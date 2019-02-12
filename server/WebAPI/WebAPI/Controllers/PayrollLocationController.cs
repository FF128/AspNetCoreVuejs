using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Controllers
{
    [Route("api/payloc")]
    [ApiController]
    public class PayrollLocationController : ControllerBase
    {
        private readonly IPayLocationRepository repo;
        private readonly IPayLocationService service;
        public PayrollLocationController(IPayLocationRepository repo,
            IPayLocationService service)
        {
            this.repo = repo;
            this.service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetPayrollLocations()
        {
            try
            {
                return Ok(await service.GetPayLocations());
            }catch(Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
    }
}