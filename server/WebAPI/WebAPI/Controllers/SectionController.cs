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
    [Route("api/section")]
    [ApiController]
    public class SectionController : ControllerBase
    {
        private readonly ISectionRepository repo;
        private readonly ISectionService service;
        public SectionController(ISectionRepository repo,
            ISectionService service)
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
        public async Task<IActionResult> Insert(Section section)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = await service.Insert(section);

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
        [HttpPut]
        public async Task<IActionResult> Update(Section section)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok();
                }
                var result = await service.Update(section);

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
