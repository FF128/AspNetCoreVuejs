﻿using System;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ScreenTypeController : ControllerBase
    {
        private readonly IScreenTypeRepository repo;
        private readonly IScreenTypeService service;
        public ScreenTypeController(IScreenTypeRepository repo,
            IScreenTypeService service)
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
        public async Task<IActionResult> Insert(ScreenType st)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var result = await service.Insert(st);

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
        public async Task<IActionResult> Update(ScreenType st)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok();
                }
                var result = await service.Update(st);

                return Ok(result);
            }
            catch (Exception ex)
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