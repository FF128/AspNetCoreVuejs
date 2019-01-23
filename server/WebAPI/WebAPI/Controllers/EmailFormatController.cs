using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Controllers
{
    [Route("api/email-format")]
    [ApiController]
    public class EmailFormatController : ControllerBase
    {
        private readonly IEmailFormatRepository repo;
        private readonly IEmailFormatService service;
        public EmailFormatController(IEmailFormatRepository repo,
            IEmailFormatService service)
        {
            this.repo = repo;
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(EmailFormat emailFormat)
        {
            try
            {
                var result = await service.Insert(emailFormat);
                return Ok(result);

            }catch(Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("{transType}")]
        public async Task<IActionResult> GetEmailFormat(string transType)
        {
            try
            {
                return Ok(await repo.GetByTransType(transType));

            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
    }
}