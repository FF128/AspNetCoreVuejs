using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos.PRDto;
using WebAPI.Helpers;
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
    }
}