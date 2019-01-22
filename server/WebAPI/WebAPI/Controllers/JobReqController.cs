﻿using WebAPI.Models;
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
    [Route("api/job-req")]
    [ApiController]
    public class JobReqController : ControllerBase
    {
        private readonly IJobReqRepository repo;
        private readonly IJobReqService service;
        public JobReqController(IJobReqRepository repo,
            IJobReqService service)
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
        public async Task<IActionResult> Insert(JobReq jr)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = await service.Insert(jr);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }

        }
        [HttpGet("{id}")]
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
        [HttpGet("{code}")]
        public async Task<IActionResult> GetByCode(string code)
        {
            try
            {
                var info = await repo.GetByCode(code);
                return Ok(info);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(JobReq jr)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok();
                }
                var result = await service.Update(jr);

                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
            
        }
        [HttpDelete("{id}")]
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
