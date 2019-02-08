using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using WebAPI.Dtos;
using WebAPI.Dtos.BudgetEntry;
using WebAPI.Helpers;
using WebAPI.Models.BudgetEntryModel;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Controllers
{
    [Route("api/budget-entry")]
    [ApiController]
    public class BudgetEntryController : ControllerBase
    {
        private readonly IBudgetEntryService service;
        private readonly IBudgetEntryApprovalRepository beaRepo;
        private readonly IReturnedBudgetEntryRepository rbeRepo;
        private readonly IBudgetEntryRepository repo;
        private readonly IPaginationRepository<PaginationBudgetEntryDetailsDto> paginationRepo;
        public BudgetEntryController(IBudgetEntryService service,
            IBudgetEntryApprovalRepository beaRepo,
            IReturnedBudgetEntryRepository rbeRepo,
            IBudgetEntryRepository repo,
            IPaginationRepository<PaginationBudgetEntryDetailsDto> paginationRepo)
        {
            this.service = service;
            this.beaRepo = beaRepo;
            this.rbeRepo = rbeRepo;
            this.repo = repo;
            this.paginationRepo = paginationRepo;
        }
        [HttpPost]
        public async Task<IActionResult> Insert([FromForm] BudgetEntryAndEntryDetailsAttachmentDto dto)
        {
            try
            {
                // Deserialize Object
                var budgetEntry = JsonConvert.DeserializeObject<BudgetEntryDto>(dto.BudgetEntry);
                var budgetEntryDetails = JsonConvert.DeserializeObject<IEnumerable<BudgetEntryDetailsDto>>(dto.BudgetEntryDetails);

                var result = await service.Insert(new BudgetEntryAndEntryDetailsDto
                {
                    BudgetEntryDto = budgetEntry,
                    BudgetEntryDetails = budgetEntryDetails,
                    Attachments = dto.files
                });
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPost("return-budget")]
        public async Task<IActionResult> ReturnBudgetEntry([FromForm] InsertReturnBudgetEntryDto dto)
        {
            try
            {
                // Deserialize Object
                var budgetEntry = JsonConvert.DeserializeObject<BudgetEntryDto>(dto.BudgetEntry);
                var budgetEntryDetails = JsonConvert.DeserializeObject<IEnumerable<BudgetEntryDetailsDto>>(dto.BudgetEntryDetails);

                var result = await service.InsertReturnBudget(new InsertReturnBudgetEntryWithCommentDto
                {
                    BudgetEntryDto = budgetEntry,
                    BudgetEntryDetails = budgetEntryDetails,
                    Attachments = dto.files,
                    Comment = dto.Comment,
                    TransactionNo = dto.TransactionNo
                });
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("waiting")]
        public async Task<IActionResult> GetBudgetEntriesWithStatusWaiting()
        {
            try
            {
                return Ok(await beaRepo.GetBudgetEntriesWithStatusWaiting());
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("returned")]
        public async Task<IActionResult> GetBudgetEntriesWithStatusReturned()
        {
            try
            {
                return Ok(await rbeRepo.GetBudgetEntryWithStatusReturned());
            }
            catch (Exception ex)    
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("{transNo}")]
        public async Task<IActionResult> GetBudgetEntryByTransNo(string transNo)
        {
            try
            {
                var result = await service.GetBudgetEntryApprovalDetails(transNo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("main/{transNo}")]
        public async Task<IActionResult> GetBudgetEntryMain(string transNo)
        {
            try
            {
                var result = await repo.GetByTransNo(transNo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("main")]
        public async Task<IActionResult> GetBudgetEntryMain()
        {
            try
            {
                var result = await repo.GetAll();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("details/{transNo}")]
        public async Task<IActionResult> GetBudgetEntryDetails(string transNo)
        {
            try
            {
                var result = await repo.GetBudgetEntryMaintDetailsByTransNo(transNo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("details")]
        public async Task<IActionResult> GetBudgetEntryDetails()
        {
            try
            {
                var result = await repo.GetBudgetEntryMaintDetails();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("details/pageNum/{pageNum}/pageSize/{pageSize}/query/{query}")]
        public async Task<IActionResult> GetBudgetEntryDetailsPagination(int pageNum, int pageSize, string query)
        {
            try
            {
                var items = await paginationRepo.Pagination("sp_BudgetEntryMaintDetails_Pagination", pageNum, pageSize, query);

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("attachment/{transNo}")]
        public async Task<IActionResult> GetBudgetEntryAttachments(string transNo)
        {
            try
            {
                var result = await repo.GetBudgetEntryMaintAttachmentsByTransNo(transNo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        //[HttpGet("{transNo}/{fileName}/{folderName}")]
        //public IActionResult ViewAttachment(string transNo, string fileName, string folderName)
        //{
        //    var file = Path.Combine(Directory.GetCurrentDirectory(),
        //                    "Files", folderName, fileName);
        //    //IFileProvider fileProvider = new ;
        //    //var fileInfo = fileProvider.GetFileInfo(file);
        //    FileInfo f = new FileInfo(file);
        //    string contentType;
        //    var contentTypeResult = new FileExtensionContentTypeProvider().TryGetContentType(f.Extension, out contentType);
        //    return PhysicalFile(file, contentType);
        //}
        [HttpPost("accept")]
        public async Task<IActionResult> AcceptEntry(BudgetEntryMainHeader budgetEntryMainHeader)
        {
            try
            {
                var result = await service.AcceptEntry(budgetEntryMainHeader.TransactionNo);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPost("decline")]
        public async Task<IActionResult> DeclineEntry(BudgetEntryMainHeader budgetEntryMainHeader)
        {
            try
            {
                var result = await service.DeclineEntry(budgetEntryMainHeader.TransactionNo);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPost("return")]
        public async Task<IActionResult> ReturnEntry(BudgetEntryMaintReturnComment comment)
        {
            try
            {
               var result = await service.ReturnEntry(comment);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }

        [HttpGet("comments/{transNo}")]
        public async Task<IActionResult> GetBudgetEntryMaintReturnCommentsByTransNo(string transNo)
        {
            try
            {
                return Ok(await rbeRepo.GetBudgetEntryMaintReturnCommentsByTransNo(transNo));
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPut("remarks")]
        public async Task<IActionResult> UpdateRemarks(UpdateRemarksBudgetEntryDetailDto dto)
        {
            try
            {
                await beaRepo.UpdateRemarks(dto);

                return Ok(CustomMessageHandler.RecordUpdated());
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
    

        [HttpGet("budget-history")]
        public async Task<IActionResult> GetBudgetHistories()
        {
            try
            {
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("budget-history/{transNo}")]
        public async Task<IActionResult> GetBudgetHistoriesByTransNo(string transNo)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
    }
}