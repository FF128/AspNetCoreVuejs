using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.ServiceInterfaces;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [Route("api/dr")]
    [ApiController]
    public class DRController : ControllerBase
    {
        private readonly IDutiesAndResponsibilitiesRepository repo;
        private readonly IDutiesAndResponsibilitiesService service;
        public DRController(IDutiesAndResponsibilitiesRepository repo,
            IDutiesAndResponsibilitiesService service)
        {
            this.repo = repo;
            this.service = service;
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

        [HttpPost]
        public async Task<IActionResult> Insert(DutiesAndResponsibilities dr)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = await service.Insert(dr);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var info = await repo.GetById(id);
                return Ok(info);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }

        }
        [HttpPut]
        public async Task<IActionResult> Update(DutiesAndResponsibilities dr)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok();
                }
                var result = await service.Update(dr);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
           
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await service.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }

        }
    }
}
