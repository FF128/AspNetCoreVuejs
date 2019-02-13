using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Helpers;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransUserController : ControllerBase
    {
        private readonly ITransUserRepository repo;
        private readonly ITransUserService service;
        public TransUserController(ITransUserRepository repo,
            ITransUserService service)
        {
            this.repo = repo;
            this.service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Insert(InsertTransUserDto dto)
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