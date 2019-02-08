﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Helpers;
using WebAPI.Models.BudgetEntryModel;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;
using WebAPI.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using System.IO;
using WebAPI.Dtos.BudgetEntry;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class BudgetEntryService : IBudgetEntryService
    {
        private readonly IBudgetEntryRepository repo;
        private readonly IBudgetEntryApprovalRepository beaRepo;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IAuditTrailService<BudgetEntryMainHeader> mainAuditTrailService;
        private readonly IAuditTrailService<IEnumerable<BudgetEntryMaintDetails>> detailsAuditTrailService;
        private readonly IAuditTrailService<IEnumerable<BudgetEntryMaintAttachment>> attachAuditTrailService;
        private readonly IAuditTrailService<IEnumerable<BudgetEntryMaintAllocated>> allocatedhAuditTrailService;
        private readonly IAuditTrailService<BudgetEntryMaintReturnComment> commentAuditTrailService;


        public BudgetEntryService(IBudgetEntryRepository repo,
            ICompanyInformationRepository compInfoRepo,
            IBudgetEntryApprovalRepository beaRepo,
            IAuditTrailService<BudgetEntryMainHeader> mainAuditTrailService,
            IAuditTrailService<IEnumerable<BudgetEntryMaintDetails>> detailsAuditTrailService,
            IAuditTrailService<IEnumerable<BudgetEntryMaintAttachment>> attachAuditTrailService,
            IAuditTrailService<IEnumerable<BudgetEntryMaintAllocated>> allocatedhAuditTrailService,
            IAuditTrailService<BudgetEntryMaintReturnComment> commentAuditTrailService)
        {
            this.repo = repo;
            this.compInfoRepo = compInfoRepo;
            this.beaRepo = beaRepo;
            this.mainAuditTrailService = mainAuditTrailService;
            this.detailsAuditTrailService = detailsAuditTrailService;
            this.attachAuditTrailService = attachAuditTrailService;
            this.allocatedhAuditTrailService = allocatedhAuditTrailService;
            this.commentAuditTrailService = commentAuditTrailService;
        }
        private string GenerateTransNo(int identity) => string.Format("BE{0}", identity.ToString("D12"));
        public async Task<CustomMessage> Insert(BudgetEntryAndEntryDetailsDto dto)
        {
            var main = dto.BudgetEntryDto.MapToBudgetEntryMainHeader(); 

            main.CompanyCode = compInfoRepo.GetCompanyCode(); // Get Company Code
            
            var identity = await repo.Insert(main); // Insert to Budget Entry Main and return @@Identity

            await mainAuditTrailService.Save(new BudgetEntryMainHeader(), main, "ADD"); // 
            if (identity != 0)
            {
                string transNo = GenerateTransNo(identity); // Generate Transaction No.

                await repo.UpdateTransNo(identity, transNo); // Update Main Transaction No. after Insert

                await mainAuditTrailService.Save(main, new BudgetEntryMainHeader { TransactionNo = transNo }, "Update");

                var details = dto.BudgetEntryDetails.MapToBudgetEntryMaintDetails(transNo);

                await repo.InsertDetails(details); // Insert to Budget Entry Details

                await detailsAuditTrailService.Save(new List<BudgetEntryMaintDetails>(), details, "ADD");

                if (dto.Attachments != null)
                {
                    var attachments = await WriteFile(dto.Attachments, transNo); // return List

                    await repo.InsertAttachments(attachments); // Insert to Budget Entry Attachments
                    await attachAuditTrailService.Save(new List<BudgetEntryMaintAttachment>(), attachments, "ADD");
                }
                return CustomMessageHandler.Success("Budget entry has been successfully submitted");
            }
            return CustomMessageHandler.Error($"Error saving this record with identity {identity}");
        }

        private async Task<IEnumerable<BudgetEntryMaintAttachment>> WriteFile(IEnumerable<IFormFile> formFiles, string transNo)
        {
            long size = formFiles.Sum(f => f.Length);
            Guid id = Guid.NewGuid();
            var attachments = new List<BudgetEntryMaintAttachment>();
            // full path to file in temp location
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"Files\\{id}");

            if (Directory.Exists(path))
            {
                return null;
            }

            DirectoryInfo di = Directory.CreateDirectory(path);
            
            foreach (var formFile in formFiles)
            {
                var filePath = Path.Combine(path, formFile.FileName);
                if (formFile.Length > 0)
                {
                    
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }

                attachments.Add(new BudgetEntryMaintAttachment
                {
                    FileName = formFile.FileName,
                    FullPath = filePath,
                    TransactionNo = transNo,
                    FolderName = id.ToString()
                });
            }
            return attachments;
        }

        public async Task<GetBudgetEntryApprovalDetailsDto> GetBudgetEntryApprovalDetails(string transNo)
        {
            return new GetBudgetEntryApprovalDetailsDto
            {
                BudgetEntryMainHeader = await repo.GetByTransNo(transNo),
                BudgetEntryMaintAttachments = await repo.GetBudgetEntryMaintAttachmentsByTransNo(transNo),
                BudgetEntryMaintDetails = await repo.GetBudgetEntryMaintDetailsByTransNo(transNo)
            };
        }

        public async Task<CustomMessage> AcceptEntry(string transNo)
        {
            var detail = await beaRepo.GetByTransNo(transNo);
            if (detail == null)
            {
                return CustomMessageHandler.Error($"Unable to find any data using this transaction no. {transNo}");
            }

            await beaRepo.UpdateBudgetEntryDetailsStatusApproved(transNo, "APPROVED");

            await detailsAuditTrailService.Save(new List<BudgetEntryMaintDetails>(), 
                "UPDATE", $"Update status of this transaction no. {detail.TransactionNo} to approved");

            await repo.UpdateStatus(transNo, "OPEN");
            await mainAuditTrailService.Save(new BudgetEntryMainHeader(),
                "UPDATE", $"Update status of this transaction no. {detail.TransactionNo} to open");

            var bemaList = new List<BudgetEntryMaintAllocated>();
            foreach (var bed in await beaRepo.GetAllByTransNo(transNo))
            {
                for(int i = 0; i < bed.Quantity; i++)
                {
                    bemaList.Add(new BudgetEntryMaintAllocated
                    {
                        TransactionNo = bed.TransactionNo,
                        BudgetDetailsID = bed.ID,
                        CreatedBy = "" // Current User
                    });
                }
                await repo.InsertAllocated(bemaList); // Save Budget Entry Allocated
                await allocatedhAuditTrailService.Save(new List<BudgetEntryMaintAllocated>(), bemaList, "ADD");

                bemaList = new List<BudgetEntryMaintAllocated>();
            }

            return CustomMessageHandler.RecordUpdated();
        }

        public async Task<CustomMessage> DeclineEntry(string transNo)
        {
            if (await beaRepo.GetByTransNo(transNo) == null)
            {
                return CustomMessageHandler.Error($"Unable to find any data using this transaction no. {transNo}");
            }

            await beaRepo.UpdateBudgetEntryDetailsStatusDeclined(transNo, "DECLINED");
            await detailsAuditTrailService.Save(new List<BudgetEntryMaintDetails>(),
               "UPDATE", $"Update status of this transaction no. {transNo} to declined");

            await repo.UpdateStatus(transNo, "OPEN");
            await mainAuditTrailService.Save(new BudgetEntryMainHeader(),
               "UPDATE", $"Update status of this transaction no. {transNo} to open");

            return CustomMessageHandler.RecordUpdated();
        }

        public async Task<CustomMessage> ReturnEntry(BudgetEntryMaintReturnComment comment)
        {
            if (await beaRepo.GetByTransNo(comment.TransactionNo) == null)
            {
                return CustomMessageHandler.Error($"Unable to find any data using this transaction no. {comment.TransactionNo}");
            }

            await beaRepo.UpdateBudgetEntryDetailsStatusReturned(comment, "RETURNED");
            await detailsAuditTrailService.Save(new List<BudgetEntryMaintDetails>(),
              "UPDATE", $"Update status of this transaction no. {comment.TransactionNo} to returned");

            await repo.UpdateStatus(comment.TransactionNo, "RETURNED");
            await mainAuditTrailService.Save(new BudgetEntryMainHeader(),
              "UPDATE", $"Update status of this transaction no. {comment.TransactionNo} to returned");

            //Insert to Budget Entry Return Comment
            await repo.InsertComment(comment);
            await commentAuditTrailService.Save(new BudgetEntryMaintReturnComment(), comment, "ADD");
            return CustomMessageHandler.RecordUpdated();
        }

        public async Task<CustomMessage> InsertReturnBudget(InsertReturnBudgetEntryWithCommentDto dto)
        {
             var main = dto.BudgetEntryDto.MapToBudgetEntryMainHeader(); 

            main.CompanyCode = compInfoRepo.GetCompanyCode(); // Get Company Code
            main.TransactionNo = dto.TransactionNo;

            // var identity = await repo.Insert(main); // Insert to Budget Entry Main and return @@Identity
            var budgetEntry = await repo.GetByTransNo(dto.TransactionNo);
            if (budgetEntry != null)
            {
                await repo.Update(main); // Update Budget Entry Main Header
                await mainAuditTrailService.Save(budgetEntry, main, "UPDATE");

                await repo.DeleteByTransNo(dto.TransactionNo); // Delete From Budget Entry Details
                await detailsAuditTrailService.Save(new List<BudgetEntryMaintDetails>(), "DELETE", $"Delete budget entry maint details with Trans No. {budgetEntry.TransactionNo}");

                var details = dto.BudgetEntryDetails.MapToBudgetEntryMaintDetails(dto.TransactionNo);

                await repo.InsertDetails(details); // Insert to Budget Entry Details
                await detailsAuditTrailService.Save(new List<BudgetEntryMaintDetails>(), details, "ADD");

                await repo.UpdateStatus(dto.TransactionNo, "WAITING");
                await mainAuditTrailService.Save(new BudgetEntryMainHeader(),
                    "UPDATE", $"Update status of this transaction no. {dto.TransactionNo} to waiting");  

                if (!String.IsNullOrEmpty(dto.Comment) || !String.IsNullOrWhiteSpace(dto.Comment))
                {
                    await repo.InsertComment(new BudgetEntryMaintReturnComment
                    {
                        Comment = dto.Comment,
                        TransactionNo = dto.TransactionNo
                    });
                }
                //if (dto.Attachments != null)
                //{
                //    var attachments = await WriteFile(dto.Attachments, transNo); // return List

                //    await repo.InsertAttachments(attachments); // Insert to Budget Entry Attachments
                //}
                return CustomMessageHandler.Success("Budget entry has been successfully submitted");
            }
            return CustomMessageHandler.Error($"Data doesn't exist");
        }
    }
}
