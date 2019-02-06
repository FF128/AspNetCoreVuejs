using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Controllers
{
    [Route("api/audit-trail")]
    [ApiController]
    public class AuditTrailController : ControllerBase
    {
        private readonly IAuditTrailRepository repo;
        private readonly IPaginationRepository paginationRepo;
        public AuditTrailController(IAuditTrailRepository repo,
            IPaginationRepository paginationRepo)
        {
            this.repo = repo;
            this.paginationRepo = paginationRepo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await repo.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }

        [HttpGet("pageNum/{pageNum}/pageSize/{pageSize}/query/{query}")]
        public async Task<IActionResult> Pagination(int pageNum, int pageSize, string query)
        {
            try
            {
                var items = await paginationRepo.Pagination("sp_AuditTrail_Pagination",pageNum, pageSize, query);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
    }
}