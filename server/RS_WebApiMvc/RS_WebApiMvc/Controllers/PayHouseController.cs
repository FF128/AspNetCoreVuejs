using RS_WebApiMvc.Models;
using RS_WebApiMvc.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RS_WebApiMvc.Controllers
{
    public class PayHouseController : ApiController
    {
        private readonly IPayHouseRepository repo;
        public PayHouseController(IPayHouseRepository repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            try
            {
                return Ok(await repo.GetAll());
            }
            catch (Exception ex)
            {
                return Ok(ex.InnerException);
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> Insert(PayHouse ph)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                await repo.Insert(ph);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IHttpActionResult> GetById(int id)
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
        public async Task<IHttpActionResult> Update(PayHouse ph)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }
            await repo.Update(ph);

            return Ok();
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            try
            {
                await repo.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
