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
    public class DesignationDutiesReqService : IDesignationDutiesReqService
    {
        private readonly IDesignationDutiesRepository repo;
        private readonly IDutiesAndResponsibilitiesRepository responsibilitiesRepo;
        private readonly IJobLevelRepository jobLevelRepo;
        private readonly IJobReqRepository jobReqRepo;
        private readonly IMapper mapper;
        public DesignationDutiesReqService(IDesignationDutiesRepository repo,
            IDutiesAndResponsibilitiesRepository responsibilitiesRepo,
            IJobLevelRepository jobLevelRepo,
            IJobReqRepository jobReqRepo,
            IMapper mapper)
        {
            this.repo = repo;
            this.responsibilitiesRepo = responsibilitiesRepo;
            this.jobLevelRepo = jobLevelRepo;
            this.jobReqRepo = jobReqRepo;
            this.mapper = mapper;
        }

        public async Task<CustomMessage> Delete(string code)
        {
            if((await repo.GetByCode(code)) != null)
            {
                await repo.DeleteByCode(code);
                await repo.DeleteDesignationDutiesJobReqsByCode(code);
                await repo.DeleteDesignationDutiesResponsibilitiesByCode(code);

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data not found!");
        }

        public async Task<IEnumerable<GetDesignationDutiesReqDto>> GetAll()
        {
            var data = await repo.GetAll();
            var dataList = new List<GetDesignationDutiesReqDto>();
            List<string> strDuties = new List<string>();
            List<string> strJobLevel = new List<string>();
            List<DutiesAndResponsibilities> dutiesAndResponsibilities = new List<DutiesAndResponsibilities>();
            List<JobReq> jobReqList = new List<JobReq>();
            foreach (var item in data.ToList())
            {
                var duties = await repo.GetDesignationDutiesResponsibilities(item.DesignationCode);
                foreach (var duty in duties)
                {
                    var dutiesRes = await responsibilitiesRepo.GetByCode(duty.DutiesResponsibilitiesCode);
                    if(dutiesRes != null)
                    {
                        strDuties.Add(dutiesRes.DutiesResponsibilitiesDesc);
                        dutiesAndResponsibilities.Add(dutiesRes);
                       // strDuties = string.Join(Environment.NewLine, strDuties);
                    }
                }
                var jobReqs = await repo.GetDesignationDutiesJobReqs(item.DesignationCode);
                foreach (var jobReq in jobReqs)
                {
                    var jobReqData = await jobReqRepo.GetByCode(jobReq.JobReqCode);
                    if(jobReqData != null)
                    {
                        strJobLevel.Add(jobReqData.JobReqDesc);
                        jobReqList.Add(jobReqData);
                       // strJobLevel = string.Join(Environment.NewLine, strJobLevel);
                    }
                }
                dataList.Add(new GetDesignationDutiesReqDto {
                    DesignationCode = item.DesignationCode, DutiesRes = strDuties, JobReq = strJobLevel,
                    DutiesAndResponsibilities = dutiesAndResponsibilities, JobReqs = jobReqList
                });

                // Instantiate new List
                strDuties = new List<string>(); strJobLevel = new List<string>();
                dutiesAndResponsibilities = new List<DutiesAndResponsibilities>();
                jobReqList = new List<JobReq>();
            }
            return dataList;
        }

        public async Task<CustomMessage> Insert(DesignationDutiesReqDto dto)
        {
            if((await repo.GetByCode(dto.DesignationCode)) == null)
            {
                var data = mapper.Map<DesignationDuties>(dto);

                await repo.Insert(data);
                //list.Where(w => w.Name == "height").ToList().ForEach(s => s.Value = 30);
                dto.DesignationDutiesResponsibilities.ToList().ForEach(x => x.DesignationCode = data.DesignationCode);
                await repo.InsertDutiesReq(dto.DesignationDutiesResponsibilities);

                dto.DesignationDutiesJobReqs.ToList().ForEach(x => x.DesignationCode = data.DesignationCode);
                await repo.InsertJobReq(dto.DesignationDutiesJobReqs);

                return CustomMessageHandler.RecordAdded();
            }
            return CustomMessageHandler.Error("Code is already used"); 
        }

        public async Task<CustomMessage> Update(DesignationDutiesReqDto dto)
        {
            if ((await repo.GetByCode(dto.DesignationCode)) != null)
            {
                var data = mapper.Map<DesignationDuties>(dto);

                await repo.Update(data);
                //list.Where(w => w.Name == "height").ToList().ForEach(s => s.Value = 30);
                dto.DesignationDutiesResponsibilities.ToList().ForEach(x => x.DesignationCode = data.DesignationCode);
                await repo.UpdateDutiesReq(dto.DesignationDutiesResponsibilities);

                dto.DesignationDutiesJobReqs.ToList().ForEach(x => x.DesignationCode = data.DesignationCode);
                await repo.UpdateJobReq(dto.DesignationDutiesJobReqs);

                return CustomMessageHandler.RecordAdded();
            }
            return CustomMessageHandler.Error("Code is already used");
        }
    }
}
