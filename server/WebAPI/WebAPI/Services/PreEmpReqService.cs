using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class PreEmpReqService : IPreEmpReqService
    {
        private readonly IPreEmpReqRepository repo;
        private readonly IAuditTrailService<PreEmpReq> auditTrailService;
        private readonly ICompanyInfoService comInfoService;
        private readonly IMapper mapper;
        public PreEmpReqService(IPreEmpReqRepository repo,
             IAuditTrailService<PreEmpReq> auditTrailService,
             ICompanyInfoService comInfoService,
             IMapper mapper)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.comInfoService = comInfoService;
            this.mapper = mapper;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var preEmpReq = await repo.GetById(id);
            if (preEmpReq != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new PreEmpReq(), preEmpReq, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<IEnumerable<PreEmpReqEmpStatusDesignationDto>> GetAll()
        {
           return mapper.Map<IEnumerable<PreEmpReqEmpStatusDesignationDto>>(await repo.GetAll());
        }

        public async Task<CustomMessage> Insert(PreEmpReq pre)
        {
            if (String.IsNullOrEmpty(pre.PreEmpReqCode) || String.IsNullOrWhiteSpace(pre.PreEmpReqCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(pre.PreEmpReqCode)) == null)
            {
                pre.CompanyCode = comInfoService.CompanyCode;
                await repo.Insert(pre);

                await auditTrailService.Save(new PreEmpReq(), pre, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(PreEmpReq pre)
        {
            var preEmpReq = await repo.GetByCode(pre.PreEmpReqCode);
            if (preEmpReq != null)
            {
                await repo.Update(pre);

                //Audit Trail
                await auditTrailService.Save(preEmpReq, pre, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
