using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Helpers;
using WebAPI.Models.ApplicantsEntrySetUp;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Controllers
{
    [Route("api/applicants-entry")]
    [ApiController]
    public class ApplicantsEntryController : ControllerBase
    {
        private readonly IAppEntryGenInfoRepository genInfoRepo;
        private readonly IAppEntryGenInfoService genInfoService;
        private readonly IAppEntryTextCertRepository textCertRepo;
        private readonly IAppEntryTextCertService textCertService;

        private readonly IAppEntryEssayInfoRepository essayInfoRepo;
        private readonly IAppEntryEssayInfoService essayInfoService;
        public ApplicantsEntryController(IAppEntryGenInfoRepository genInfoRepo,
            IAppEntryGenInfoService genInfoService,
            IAppEntryTextCertRepository textCertRepo,
            IAppEntryTextCertService textCertService,
            IAppEntryEssayInfoRepository essayInfoRepo,
            IAppEntryEssayInfoService essayInfoService)
        {
            this.genInfoRepo = genInfoRepo;
            this.genInfoService = genInfoService;
            this.textCertRepo = textCertRepo;
            this.textCertService = textCertService;
            this.essayInfoRepo = essayInfoRepo;
            this.essayInfoService = essayInfoService;
        }
        [HttpGet("gen")]
        public async Task<IActionResult> GetAllGenInfo()
        {
            try
            {
                return Ok(await genInfoRepo.GetAll());
            }
            catch(Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
          
        }
        [HttpPost("gen")]
        public async Task<IActionResult> InsertGenInfo(AppEntryGenInfo info)
        {
            try
            {
                var result = await genInfoService.Insert(info);
                return Ok(result);
            }catch(Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPut("gen")]
        public async Task<IActionResult> UpdateGenInfo(AppEntryGenInfo info)
        {
            try
            {
                var result = await genInfoService.Update(info);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpDelete("gen/{id}")]
        public async Task<IActionResult> DeleteGenInfo(int id)
        {
            try
            {
                var result = await genInfoService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpGet("text")]
        public async Task<IActionResult> GetTextCert()
        {
            try
            {
                return Ok(await textCertRepo.GetTextCert());

            }catch(Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPost("text")]
        public async Task<IActionResult> InsertTextCert(AppEntryTextCert info)
        {
            try
            {
                var result = await textCertService.Insert(info);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPut("text")]
        public async Task<IActionResult> UpdateTextCert(AppEntryTextCert info)
        {
            try
            {
                var result = await textCertService.Update(info);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }

        [HttpGet("essay")]
        public async Task<IActionResult> GetAllEssayQuestions()
        {
            try
            {
                return Ok(await essayInfoRepo.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }

        }
        [HttpPost("essay")]
        public async Task<IActionResult> InsertEssayQuestion(AppEntryEssayInfo info)
        {
            try
            {
                var result = await essayInfoService.Insert(info);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpPut("essay")]
        public async Task<IActionResult> UpdateEssayQuestion(AppEntryEssayInfo info)
        {
            try
            {
                var result = await essayInfoService.Update(info);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
        [HttpDelete("essay/{id}")]
        public async Task<IActionResult> DeleteEssayQuestion(int id)
        {
            try
            {
                var result = await essayInfoService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(CustomMessageHandler.Error(ex.Message));
            }
        }
    }
}