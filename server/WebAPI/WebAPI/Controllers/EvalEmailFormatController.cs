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
    [Route("api/eval-email-format")]
    [ApiController]
    public class EvalEmailFormatController : ControllerBase
    {
        private readonly IEvalEmailFormatRepository repo;
        private readonly IEvalEmailFormatService service;
        public EvalEmailFormatController(IEvalEmailFormatRepository repo,
            IEvalEmailFormatService service)
        {
            this.repo = repo;
            this.service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(EvalEmailFormat emailFormat)
        {
            try
            {
                var result = await service.Insert(emailFormat);
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("{transType}")]
        public async Task<IActionResult> GetEvalEmailFormat(string transType)
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