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
        public AuditTrailController(IAuditTrailRepository repo)
        {
            this.repo = repo;
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
    }
}