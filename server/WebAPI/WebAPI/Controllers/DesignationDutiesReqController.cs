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
    [Route("api/desig-duties-req")]
    [ApiController]
    public class DesignationDutiesReqController : ControllerBase
    {
        private readonly IDesignationDutiesRepository repo;
        private readonly IDesignationDutiesReqService service;
        public DesignationDutiesReqController(IDesignationDutiesRepository repo,
            IDesignationDutiesReqService service)
        {
            this.repo = repo;
            this.service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Insert(DesignationDutiesReqDto dto)
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
        [HttpPut]
        public async Task<IActionResult> Update(DesignationDutiesReqDto dto)
        {
            try
            {
                var result = await service.Update(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await service.GetAll());
            }catch(Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpDelete("{code}")]
        public async Task<IActionResult> Delete(string code)
        {
            try
            {
                var result = await service.Delete(code);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
    }
}