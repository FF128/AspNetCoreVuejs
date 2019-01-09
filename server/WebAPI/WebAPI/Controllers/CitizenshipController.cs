using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitizenshipController : ControllerBase
    {
        private readonly ICitizenshipRepository repo;
        public CitizenshipController(ICitizenshipRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await repo.GetAll());                
            }catch(Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Citizenship cit)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await repo.Insert(cit);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
                return BadRequest(ex.Message);
            }

        }
        [HttpPut]
        public async Task<IActionResult> Update(Citizenship cit)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            await repo.Update(cit);

            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await repo.Delete(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}