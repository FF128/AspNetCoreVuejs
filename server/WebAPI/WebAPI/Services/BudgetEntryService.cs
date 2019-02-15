using System;
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
        private readonly IUserRepository userRepo;
        private readonly ITransUserRepository transUserRepo;
        private readonly IAuditTrailService<BudgetEntryMainHeader> mainAuditTrailService;
        private readonly IAuditTrailService<IEnumerable<BudgetEntryMaintDetails>> detailsAuditTrailService;
        private readonly IAuditTrailService<IEnumerable<BudgetEntryMaintAttachment>> attachAuditTrailService;
        private readonly IAuditTrailService<IEnumerable<BudgetEntryMaintAllocated>> allocatedhAuditTrailService;
        private readonly IAuditTrailService<BudgetEntryMaintReturnComment> commentAuditTrailService;
        private const string TRANS = "BUDGETAPPROVAL";
        private const string APPROVED = "APPROVED";
        private const string RETURNED = "RETURNED";
        private const string WAITING = "WAITING";
        private const string DECLINED = "DECLINED";

        public BudgetEntryService(IBudgetEntryRepository repo,
            ICompanyInformationRepository compInfoRepo,
            IBudgetEntryApprovalRepository beaRepo,
            ITransUserRepository transUserRepo,
            IUserRepository userRepo,
            IAuditTrailService<BudgetEntryMainHeader> mainAuditTrailService,
            IAuditTrailService<IEnumerable<BudgetEntryMaintDetails>> detailsAuditTrailService,
            IAuditTrailService<IEnumerable<BudgetEntryMaintAttachment>> attachAuditTrailService,
            IAuditTrailService<IEnumerable<BudgetEntryMaintAllocated>> allocatedhAuditTrailService,
            IAuditTrailService<BudgetEntryMaintReturnComment> commentAuditTrailService)
        {
            this.repo = repo;
            this.compInfoRepo = compInfoRepo;
            this.beaRepo = beaRepo;
            this.transUserRepo = transUserRepo;
            this.userRepo = userRepo;
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
                var transApprovingList = new List<TransApprovingBudget>();

                var budgetEntryDetails = await GetBudgetEntryMaintDetailsByTransNo(transNo);
                foreach (var item in budgetEntryDetails)
                {

                    //var transUserDepartments = await transUserRepo.GetTransUserDepartmentsByDepCode(item.DepCode, TRANS, compInfoRepo.GetCompanyCode());
                    //foreach (var deparment in transUserDepartments)
                    //{

                    //}


                    //var transUserPayLoc = await transUserRepo.GetTransUserPayLocByLocId((int)item.LocId, TRANS, compInfoRepo.GetCompanyCode());
                    //foreach (var payLoc in transUserPayLoc)
                    //{

                    //}
                    var transUsers = await transUserRepo.GetAllByTrans(compInfoRepo.GetCompanyCode(), TRANS);
                    foreach(var transUser in transUsers)
                    {

                        transApprovingList.Add(new TransApprovingBudget
                        {
                            BudgetDetailsID = item.ID,
                            Approver = transUser.EmpCode,
                            TransactionNo = item.TransactionNo,
                            CompanyCode = compInfoRepo.GetCompanyCode(),
                            CreatedBy = userRepo.GetEmpCode(),
                            Status = WAITING
                        });
                    }

                    if (transApprovingList.Any())
                        await repo.InsertTransApproving(transApprovingList);

                    transApprovingList = new List<TransApprovingBudget>();
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
            var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
            var budgetEntry = await repo.GetByTransNo(transNo);
            if (budgetEntry == null)
                throw new Exception($"Transaction ({transNo}) doesn't exist to the database");

            if (budgetEntry.Status == "OPEN")
            {
                throw new Exception($"Transaction ({transNo}) was already processed");
            }

            if (await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB))
            {
                return new GetBudgetEntryApprovalDetailsDto
                {
                    BudgetEntryMainHeader = await repo.GetByTransNo(transNo),
                    BudgetEntryMaintAttachments = await repo.GetBudgetEntryMaintAttachmentsByTransNo(transNo),
                    BudgetEntryMaintDetails = await repo.GetBudgetEntryMaintDetailsByTransNo(transNo, companyInfo.HRISDB)
                };
            }
            throw new Exception("HRIS Database not found");
        }

        public async Task<CustomMessage> AcceptEntry(string transNo)
        {
            var detail = await beaRepo.GetByTransNo(transNo, await GetHRISDB());
            if (detail == null)
            {
                return CustomMessageHandler.Error($"Unable to find any data using this transaction no. {transNo}");
            }

            await beaRepo.UpdateBudgetEntryDetailsStatusApproved(transNo, APPROVED);

            await detailsAuditTrailService.Save(new List<BudgetEntryMaintDetails>(), 
                "UPDATE", $"Update status of this transaction no. {detail.TransactionNo} to approved");

            await repo.UpdateStatus(transNo, "OPEN");
            await mainAuditTrailService.Save(new BudgetEntryMainHeader(),
                "UPDATE", $"Update status of this transaction no. {detail.TransactionNo} to open");

            var bemaList = new List<BudgetEntryMaintAllocated>();
            foreach (var bed in await beaRepo.GetAllByTransNo(transNo, await GetHRISDB()))
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
            var updateTransApprovingStatus = new UpdateTransApprovingStatusDto
            {
                TransNo = transNo, CompanyCode = compInfoRepo.GetCompanyCode(),
                IsDone = true, Status = APPROVED
            };
            await repo.UpdateTransApprovingStatus(updateTransApprovingStatus);
            return CustomMessageHandler.RecordUpdated();
        }

        public async Task<CustomMessage> DeclineEntry(string transNo)
        {
            if (await beaRepo.GetByTransNo(transNo, await GetHRISDB()) == null)
            {
                return CustomMessageHandler.Error($"Unable to find any data using this transaction no. {transNo}");
            }

            await beaRepo.UpdateBudgetEntryDetailsStatusDeclined(transNo, "DECLINED");
            await detailsAuditTrailService.Save(new List<BudgetEntryMaintDetails>(),
               "UPDATE", $"Update status of this transaction no. {transNo} to declined");

            await repo.UpdateStatus(transNo, "OPEN");
            await mainAuditTrailService.Save(new BudgetEntryMainHeader(),
               "UPDATE", $"Update status of this transaction no. {transNo} to open");

            var updateTransApprovingStatus = new UpdateTransApprovingStatusDto
            {
                TransNo = transNo,
                CompanyCode = compInfoRepo.GetCompanyCode(),
                IsDone = false,
                Status = DECLINED
            };
            await repo.UpdateTransApprovingStatus(updateTransApprovingStatus);
            return CustomMessageHandler.RecordUpdated();
        }

        public async Task<CustomMessage> ReturnEntry(BudgetEntryMaintReturnComment comment)
        {
            if (await beaRepo.GetByTransNo(comment.TransactionNo, await GetHRISDB()) == null)
            {
                return CustomMessageHandler.Error($"Unable to find any data using this transaction no. {comment.TransactionNo}");
            }

            await beaRepo.UpdateBudgetEntryDetailsStatusReturned(comment, RETURNED);
            await detailsAuditTrailService.Save(new List<BudgetEntryMaintDetails>(),
              "UPDATE", $"Update status of this transaction no. {comment.TransactionNo} to returned");

            //await repo.UpdateStatus(comment.TransactionNo, RETURNED);
            //await mainAuditTrailService.Save(new BudgetEntryMainHeader(),
            //  "UPDATE", $"Update status of this transaction no. {comment.TransactionNo} to returned");
            var updateTransApprovingStatus = new UpdateTransApprovingStatusDto
            {
                TransNo = comment.TransactionNo,
                CompanyCode = compInfoRepo.GetCompanyCode(),
                IsDone = false,
                Status = RETURNED
            };
            await repo.UpdateTransApprovingStatus(updateTransApprovingStatus);

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

                await repo.UpdateStatus(dto.TransactionNo, WAITING);
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

                var updateTransApprovingStatus = new UpdateTransApprovingStatusDto
                {
                    TransNo = dto.TransactionNo,
                    CompanyCode = compInfoRepo.GetCompanyCode(),
                    IsDone = false,
                    Status = WAITING
                };
                await repo.UpdateTransApprovingStatus(updateTransApprovingStatus);

                return CustomMessageHandler.Success("Budget entry has been successfully submitted");
            }
            return CustomMessageHandler.Error($"Data doesn't exist");
        }

        public async Task<IEnumerable<GetAvailableBudgetEntriesDto>> GetBudgetEntriesByStatus(string status)
        {
            var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
            
            if(await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB))
            {
                return await repo.GetBudgetEntriesByStatus(status, companyInfo.HRISDB);
            }

            throw new Exception("HRIS Database not found");
        }

        public async Task<IEnumerable<GetBudgetEntryMaintDetails>> GetBudgetEntryMaintDetailsByTransNo(string transNo)
        {
            var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());

            if (await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB))
            {
                return await repo.GetBudgetEntryMaintDetailsByTransNo(transNo, companyInfo.HRISDB);
            }

            throw new Exception("HRIS Database not found");
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
    }
}
