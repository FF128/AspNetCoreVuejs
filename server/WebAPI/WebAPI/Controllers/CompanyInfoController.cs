using System;
using System.Collections.Generic;
using System.IO;
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
    public class CompanyInfoController : ControllerBase
    {
        private readonly ICompanyInformationRepository repo;
        public CompanyInfoController(ICompanyInformationRepository repo)
        {
            this.repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Insert([FromForm]CompanyInformation info)
        {
            try
            {
               
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await repo.Insert(info);

                return Ok();
            }
            catch(Exception ex)
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
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
        [HttpPut]
        public async Task<IActionResult> Update(CompanyInformation info)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            await repo.Update(info);

            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await repo.Delete(id);
            return Ok();
        }
    }
}