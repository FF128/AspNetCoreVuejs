using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public PersonnelRequisitionController(IBudgetEntryRepository budgetEntryRepo,
            IPRRepository repo,
            IPRService service)
        {
            this.budgetEntryRepo = budgetEntryRepo;
            this.repo = repo;
            this.service = service;
        }
        [HttpGet("budget-entries")] 
        public async Task<IActionResult> GetBudgetEntries()
        {
            try
            {
                var result = await budgetEntryRepo.GetBudgetEntriesByStatus("OPEN");
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
        [HttpGet("waiting")]
        public async Task<IActionResult> GetPREntryWithStatusWaiting()
        {
            try
            {
                return Ok(await repo.ViewPREntryWithStatusWaitingDto());
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
                return Ok(await repo.ViewPREntryWithStatusReturnedDto());
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
    }
}