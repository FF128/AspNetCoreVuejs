﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.PRDto;
using WebAPI.Helpers;
using WebAPI.Models.PRModel;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;
using WebAPI.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using System.IO;
using WebAPI.Models.BudgetEntryModel;

namespace WebAPI.Services
{
    public class PRService : IPRService
    {
        private readonly IPRRepository prRepo;
        private readonly IMapper mapper;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly ITransUserRepository transUserRepo;
        private readonly IUserRepository userRepo;
        private const string TRANS = "PRFAPPROVAL", WAITING = "WAITING", APPROVED = "APPROVED",
            RETURNED = "RETURNED", DECLINED = "DECLINED", DONE = "DONE", CLOSE = "CLOSE", OPEN = "OPEN";

        public PRService(IPRRepository prRepo,
            ICompanyInformationRepository compInfoRepo,
            ITransUserRepository transUserRepo,
            IUserRepository userRepo,
            IMapper mapper)
        {
            this.prRepo = prRepo;
            this.mapper = mapper;
            this.compInfoRepo = compInfoRepo;
            this.transUserRepo = transUserRepo;
            this.userRepo = userRepo;
        }
        
        public async Task<GetPREntryApprovalDetailsDto> GetPREntryApprovalDetails(string prfNo)
        {
            var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
            if (compInfoRepo == null)
                throw new Exception("Company Information doesn't exist");
            if (await prRepo.GetByPRFNo(prfNo) == null)
                throw new Exception($"Personnel Requisition ({prfNo}) doesn't exist");

            if(await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB))
            {
                return new GetPREntryApprovalDetailsDto
                {
                    Header = mapper.Map<PRFHeaderMaintDto>(await prRepo.GetByPRFNo(prfNo)),
                    Details = await prRepo.GetDetailsByPRFNo(prfNo, companyInfo.HRISDB),
                    Attachments = new List<PRFDetailsMaintAttachmentDto>()
                };
            }

            throw new Exception("HRIS Database doesn't exist");
        }

        public async Task<CustomMessage> Insert(PRFHeaderDetailsDto prfHeaderDetailsDto)
        {
            prfHeaderDetailsDto.Header.CompanyCode = compInfoRepo.GetCompanyCode();
            prfHeaderDetailsDto.Header.Status = WAITING;
            prfHeaderDetailsDto.Header.CompanyCode = userRepo.GetEmpCode();
            var identity = await prRepo.Insert(mapper.Map<PRFHeaderMaint>(prfHeaderDetailsDto.Header));

            if(identity == 0)
                return CustomMessageHandler.Error($"Error saving this record with identity {identity}");

            string prfNo = GenerateTransNo(identity); // Generate PRF No.

            await prRepo.UpdatePRFNo(identity, prfNo); // Update Main PRF No. after Insert

            var details = mapper.Map<IEnumerable<PRFDetailsMaint>>(prfHeaderDetailsDto.Details);

            await prRepo.InsertDetails(details.MapToPRFDetailsMaint(prfNo, prfHeaderDetailsDto.IsBudgeted)); // Insert to PRF Details

            var prfDetails = await prRepo.GetDetailsByPRFNo(prfNo, await GetHRISDB());
            var transApprovingList = new List<TransApprovingPRF>();
            foreach (var item in prfDetails)
            {
                var transUsers = await transUserRepo.GetAllByTrans(compInfoRepo.GetCompanyCode(), TRANS);
                foreach (var transUser in transUsers)
                {

                    transApprovingList.Add(new TransApprovingPRF
                    {
                        PRFDetailsID = item.ID,
                        Approver = transUser.EmpCode,
                        PRFNo = item.PRFNo,
                        CompanyCode = compInfoRepo.GetCompanyCode(),
                        CreatedBy = userRepo.GetEmpCode(),
                        Status = WAITING
                    });
                }

                if (transApprovingList.Any())
                    await prRepo.InsertTransApproving(transApprovingList);

                transApprovingList = new List<TransApprovingPRF>();
            }

            return CustomMessageHandler.Success($"Personnel Requisition Entry has been successfully submitted . Your Transaction No. is {prfNo}");
        }

        public async Task<CustomMessage> AcceptEntry(string prfNo)
        {
            var detail = await prRepo.GetDetailsByPRFNo(prfNo, await GetHRISDB());
            if (detail == null)
            {
                return CustomMessageHandler.Error($"Unable to find any data using this transaction no. {prfNo}");
            }

            await prRepo.UpdatePRDetailsStatus(prfNo, DONE);

            await prRepo.UpdateStatus(prfNo, OPEN);
            var updateTransApprovingStatus = new UpdatePRFTransApprovingStatusDto
            {
                PRFNo = prfNo,
                CompanyCode = compInfoRepo.GetCompanyCode(),
                IsDone = true,
                Status = APPROVED
            };

            await prRepo.UpdateTransApprovingStatus(updateTransApprovingStatus);
            return CustomMessageHandler.RecordUpdated();
        }

        private string GenerateTransNo(int identity) => string.Format("PR{0}", identity.ToString("D12"));

        public async Task<CustomMessage> DeclineEntry(string prfNo)
        {
            var detail = await prRepo.GetDetailsByPRFNo(prfNo, await GetHRISDB());
            if (detail == null)
            {
                return CustomMessageHandler.Error($"Unable to find any data using this transaction no. {prfNo}");
            }

            await prRepo.UpdateStatus(prfNo, CLOSE);

            var updateTransApprovingStatus = new UpdatePRFTransApprovingStatusDto
            {
                PRFNo = prfNo,
                CompanyCode = compInfoRepo.GetCompanyCode(),
                IsDone = true,
                Status = DECLINED
            };

            await prRepo.UpdateTransApprovingStatus(updateTransApprovingStatus);
            return CustomMessageHandler.RecordUpdated();

        }

        public async Task<CustomMessage> InsertNotBudgeted(PRFHeaderDetailsDto dto)
        {
            dto.Header.CompanyCode = compInfoRepo.GetCompanyCode();
            dto.Header.Status = WAITING;
            var identity = await prRepo.Insert(mapper.Map<PRFHeaderMaint>(dto.Header));

            if (identity == 0)
                return CustomMessageHandler.Error($"Error saving this record with identity {identity}");

            string prfNo = GenerateTransNo(identity); // Generate PRF No.

            await prRepo.UpdatePRFNo(identity, prfNo); // Update Main PRF No. after Insert

            var details = mapper.Map<IEnumerable<PRFDetailsMaint>>(dto.Details);

            await prRepo.InsertDetails(details.MapToPRFDetailsMaint(prfNo, dto.IsBudgeted)); // Insert to PRF Details

            if (dto.Attachments != null)
            {
                var attachments = await WriteFile(dto.Attachments, prfNo); // return List

                await prRepo.InsertAttachments(attachments); // Insert to Budget Entry Attachments
            }

            return CustomMessageHandler.Success($"Personnel Requisition Entry has been successfully submitted . Your Transaction No. is {prfNo}");
        }
        private async Task<IEnumerable<PRFDetailsMaintAttachmentDto>> WriteFile(IEnumerable<IFormFile> formFiles, string prfNo)
        {
            long size = formFiles.Sum(f => f.Length);
            Guid id = Guid.NewGuid();
            var attachments = new List<PRFDetailsMaintAttachmentDto>();
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

                attachments.Add(new PRFDetailsMaintAttachmentDto
                {
                    FileName = formFile.FileName,
                    FullPath = filePath,
                    PRFNo = prfNo,
                    FolderName = id.ToString()
                });
            }
            return attachments;
        }
        private async Task<string> GetHRISDB()
        {
            var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());

            if (await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB))
            {
                return companyInfo.HRISDB;
            }

            throw new Exception("HRIS Database not found");
        }

        public async Task<CustomMessage> ReturnEntry(PRFMainReturnCommentDto comment)
        {
            if (await prRepo.GetByPRFNo(comment.PRFNo) == null)
            {
                return CustomMessageHandler.Error($"Unable to find any data using this PRF no. {comment.PRFNo}");
            }

            //await repo.UpdateStatus(comment.TransactionNo, RETURNED);
            //await mainAuditTrailService.Save(new BudgetEntryMainHeader(),
            //  "UPDATE", $"Update status of this transaction no. {comment.TransactionNo} to returned");
            var updateTransApprovingStatus = new UpdatePRFTransApprovingStatusDto
            {
                PRFNo = comment.PRFNo,
                CompanyCode = compInfoRepo.GetCompanyCode(),
                IsDone = true,
                Status = RETURNED
            };

            await prRepo.UpdateTransApprovingStatus(updateTransApprovingStatus);

            //Insert to Budget Entry Return Comment

            comment.CommentedBy = userRepo.GetEmpCode(); // Get Current User
            await prRepo.InsertComment(comment);
            //await commentAuditTrailService.Save(new BudgetEntryMaintReturnComment(), comment, "ADD");
            return CustomMessageHandler.RecordUpdated();
        }

        public async Task<CustomMessage> InsertReturnPRF(InsertReturnPRFDto dto)
        {

            //await prRepo.Insert(dto.Header);
            dto.Header.CompanyCode = compInfoRepo.GetCompanyCode();
            dto.Header.EditedBy = userRepo.GetEmpCode();
            await prRepo.Update(dto.Header); // Update PR HEader 
            
            foreach (var item in dto.Details)
            {
                //var prfDetails = await prRepo.GetDetailsByPRFNo(dto.Header.PRFNo, await GetHRISDB());
                var prfDetail = await prRepo.GetDetailById(item.ID);
                if(prfDetail != null)
                {
                    await prRepo.UpdateDetails(item);
                }
                else
                {
                    item.PRFNo = dto.Header.PRFNo;
                    await prRepo.InsertDetails(item);
                }
            }

            await prRepo.InsertComment(new PRFMainReturnCommentDto
            {
                Comment = dto.Comment,
                CommentedBy = userRepo.GetEmpCode(),
                PRFNo = dto.Header.PRFNo
            });

            var updateTransApprovingStatus = new UpdatePRFTransApprovingStatusDto
            {
                PRFNo = dto.Header.PRFNo,
                CompanyCode = compInfoRepo.GetCompanyCode(),
                IsDone = false,
                Status = WAITING
            };

            await prRepo.UpdateTransApprovingStatus(updateTransApprovingStatus);

            return CustomMessageHandler.RecordUpdated();
        }

        public async Task<IEnumerable<GetPREntryDetails>> GetPREntryMaintDetailsByPRFNo(string prfNo)
        {
            var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
            if (compInfoRepo == null)
                throw new Exception("Company Information doesn't exist");
            if (await prRepo.GetByPRFNo(prfNo) == null)
                throw new Exception($"Personnel Requisition ({prfNo}) doesn't exist");

            if (await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB))
            {
                return await prRepo.GetDetailsByPRFNo(prfNo, companyInfo.HRISDB);
            }

            throw new Exception("HRIS Database doesn't exist");
        }

        public async Task<CustomMessage> PRFExtendApproved(int id)
        {
            if (await prRepo.GetPRFExtendById(id, compInfoRepo.GetCompanyCode()) == null)
                throw new Exception("PRF Extend data doesn't exist");

            await prRepo.PRFExtendApproveDeclined(id, APPROVED, userRepo.GetEmpCode());

            return CustomMessageHandler.Success("Successfully Approved");
        }

        public async Task<CustomMessage> PRFExtendDeclined(int id)
        {
            if (await prRepo.GetPRFExtendById(id, compInfoRepo.GetCompanyCode()) == null)
                throw new Exception("PRF Extend data doesn't exist");

            await prRepo.PRFExtendApproveDeclined(id, DECLINED, userRepo.GetEmpCode());

            return CustomMessageHandler.Success("Successfully Approved");
        }

        public async Task<CustomMessage> InsertPRFExtend(PRFExtend prfExtend)
        {
            var header = await prRepo.GetByPRFNo(prfExtend.PRFNo);
            if (header == null)
                throw new Exception($"Data doesn't exist using this PRF No. {prfExtend.PRFNo}");

            prfExtend.OldDateTo = header.DurationTo;
            prfExtend.OldDateFrom = header.DurationFrom;
            prfExtend.OldDateRequired = header.DateRequired;
            prfExtend.CreatedBy = userRepo.GetEmpCode(); // Current User
            prfExtend.CompanyCode = compInfoRepo.GetCompanyCode(); // Current Company
            prfExtend.Status = WAITING;

            await prRepo.InsertPRFExtend(prfExtend);

            return CustomMessageHandler.Success("Personnel Requisition Entry has been successfully Extend, please wait for approver to approve your request.");
        }

        public async Task<bool> GetPRFExtendByPRFNo(string prfNo)
        {
            var details = await prRepo.GetExtendPRFDetailsByPRFNo(prfNo, compInfoRepo.GetCompanyCode());

            if (details.ExtendStatus == WAITING)
                return false;
            if (details.Status == DECLINED)
                return false;
            if (details.Status == RETURNED)
                return false;
            if (details.Status == APPROVED)
                return true;

            return false;
        }

        public async Task<IEnumerable<PRFExtend>> GetPRFExtendByStatus(string status)
        {
            return await prRepo.GetPRFExtendByStatus(status, compInfoRepo.GetCompanyCode());
        }
    }
}
