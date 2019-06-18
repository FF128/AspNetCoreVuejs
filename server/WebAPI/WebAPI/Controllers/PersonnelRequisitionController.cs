﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Dtos.PRDto;
using WebAPI.Helpers;
using WebAPI.Models.PRModel;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Controllers
{
    [Route("api/pr")]
    [ApiController]
    public class PersonnelRequisitionController : ControllerBase
    {
        private readonly IPRRepository repo;
        private readonly IBudgetEntryRepository budgetEntryRepo;
        private readonly IPRService service;
        private readonly IBudgetEntryService budgetEntryService;
        private readonly IPaginationRepository<PaginationPRFMaintDetailsDto> paginationRepo;
        private readonly IPaginationRepository<PaginationPRFHeaderMaintDto> prfHeaderPaginationRepo;
        private readonly IPaginationService<GetExtendPRFDetails> prfExtendService;
        public PersonnelRequisitionController(IBudgetEntryRepository budgetEntryRepo,
            IPRRepository repo,
            IPRService service,
            IBudgetEntryService budgetEntryService,
            IPaginationRepository<PaginationPRFMaintDetailsDto> paginationRepo,
            IPaginationRepository<PaginationPRFHeaderMaintDto> prfHeaderPaginationRepo,
            IPaginationService<GetExtendPRFDetails> prfExtendService)
        {
            this.budgetEntryRepo = budgetEntryRepo;
            this.repo = repo;
            this.service = service;
            this.budgetEntryService = budgetEntryService;
            this.paginationRepo = paginationRepo;
            this.prfHeaderPaginationRepo = prfHeaderPaginationRepo;
            this.prfExtendService = prfExtendService;
        }
        [HttpGet("budget-entries")] 
        public async Task<IActionResult> GetBudgetEntries()
        {
            try
            {
                var result = await budgetEntryService.GetBudgetEntriesByStatus("OPEN");
                return Ok(result);

            }catch(Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert(PRFHeaderDetailsDto dto)
        {
            try
            {
                var result = await service.Insert(dto);

                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("header/pageNum/{pageNum}/pageSize/{pageSize}/query/{query}")]
        public async Task<IActionResult> GetPRFHeaderMaintPagination(int pageNum, int pageSize, string query)
        {
            try
            {
                var items = await prfHeaderPaginationRepo.Pagination("sp_PRFHeaderMaint_Pagination", pageNum, pageSize, query);

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("details/{prfNo}")]
        public async Task<IActionResult> GetBudgetEntryDetails(string prfNo)
        {
            try
            {
                var result = await service.GetPREntryMaintDetailsByPRFNo(prfNo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("details/pageNum/{pageNum}/pageSize/{pageSize}/query/{query}")]
        public async Task<IActionResult> GetPRFHeaderMaintDetailsPagination(int pageNum, int pageSize, string query)
        {
            try
            {
                var items = await paginationRepo.Pagination("sp_PRFHeaderMaintDetails_Pagination", pageNum, pageSize, query);

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPost("not-budgeted")]
        public async Task<IActionResult> InsertNotBudgeted([FromForm]InsertPREntryDetailsAttachmentDto dto)
        {
            try
            {
                // Deserialize Object
                var header = JsonConvert.DeserializeObject<PRFHeaderMaintDto>(dto.Header);
                var details = JsonConvert.DeserializeObject<IEnumerable<PRFDetailsMaintDto>>(dto.Details);

                var result = await service.InsertNotBudgeted(new PRFHeaderDetailsDto
                {
                    Header = header,
                    Details = details,
                    Attachments = dto.Files
                });
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("waiting")]
        public async Task<IActionResult> GetPREntryWithStatusWaiting()
        {
            try
            {
                return Ok(await repo.ViewPRFEntryByStatus("WAITING"));
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("returned")]
        public async Task<IActionResult> GetPREntryWithStatusReturned()
        {
            try
            {
                return Ok(await repo.ViewPRFEntryByStatus("RETURNED"));
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("comments/{prfNo}")]
        public async Task<IActionResult> GetPRFHeaderMaintReturnCommentsByPRFNo(string prfNo)
        {
            try
            {
                return Ok(await repo.GetPRFHeaderMaintReturnCommentsByPRFNo(prfNo));
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("{prfNo}")]
        public async Task<IActionResult> GetBudgetEntryByTransNo(string prfNo)
        {
            try
            {
                var result = await service.GetPREntryApprovalDetails(prfNo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPost("accept")]
        public async Task<IActionResult> AcceptPREntry(PRFHeaderMaint header)
        {
            try
            {
                var result = await service.AcceptEntry(header.PRFNo);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPost("decline")]
        public async Task<IActionResult> DeclinePREntry(PRFHeaderMaint header)
        {
            try
            {
                var result = await service.DeclineEntry(header.PRFNo);

                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPost("return")]
        public async Task<IActionResult> ReturnPREntry(PRFMainReturnCommentDto comment)
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
        [HttpPost("return-prf")]
        public async Task<IActionResult> InsertReturnPRF(InsertReturnPRFDto dto)
        {
            try
            {
                var result = await service.InsertReturnPRF(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }

        #region PRF EXTEND
        [HttpGet("extend/pageNum/{pageNum}/pageSize/{pageSize}/query/{query?}")]
        public async Task<IActionResult> GetPRFExtendDetails(int pageNum, int pageSize, string query = null)
        {
            try
            {
                var items = await 
                    prfExtendService.PaginationByCompanyCode("sp_PRFExtend_ViewTransactionsPagination", pageNum, pageSize, query);

                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPost("extend")]
        public async Task<IActionResult> InsertPRFExtend(PRFExtend prfExtend)
        {
            try
            {
                var result = await service.InsertPRFExtend(prfExtend);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPost("extend/approved/{id}")]
        public async Task<IActionResult> PRFExtendApproved(int id)
        {
            try
            {
                var result = await service.PRFExtendApproved(id);
                
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPost("extend/declined/{id}")]
        public async Task<IActionResult> PRFExtendDeclined(int id)
        {
            try
            {
                var result = await service.PRFExtendDeclined(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("extend/{prfNo}")]
        public async Task<IActionResult> GetPRFExtendDetailsByPRFNo(string prfNo)
        {
            try
            {
                var result = await service.GetPRFExtendByPRFNo(prfNo);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("extend/waiting")]
        public async Task<IActionResult> GetPRFExtendByStatus()
        {
            try
            {
                var result = await service.GetPRFExtendByStatus("WAITING");
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        #endregion
    }
}