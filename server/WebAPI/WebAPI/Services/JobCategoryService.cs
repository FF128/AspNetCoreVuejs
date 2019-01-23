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
    public class JobCategoryService : IJobCategoryService
    {
        private readonly IJobCategoryRepository repo;
        private readonly IDesignationFileRepository designationFileRepo;
        private readonly IJobGroupRepository jobGroupRepo;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IMapper mapper;
        public JobCategoryService(IJobCategoryRepository repo,
            IDesignationFileRepository designationFileRepo,
            IJobGroupRepository jobGroupRepo,
            ICompanyInformationRepository compInfoRepo,
            IMapper mapper)
        {
            this.repo = repo;
            this.designationFileRepo = designationFileRepo;
            this.jobGroupRepo = jobGroupRepo;
            this.compInfoRepo = compInfoRepo;
            this.mapper = mapper;
        }
        public async Task<CustomMessage> Delete(string jobCatCode)
        {
            if ((await repo.GetByCode(jobCatCode)) != null)
            {
                await repo.Delete(jobCatCode);
                await repo.DeleteJobCatDetail(jobCatCode);

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist!");
        }

        public async Task<IEnumerable<GetJobCategoryDto>> GetAll()
        {
            var data = await repo.GetAll();
            var dataList = new List<GetJobCategoryDto>();
            List<DesignationFile> designationFiles = new List<DesignationFile>();
            foreach (var item in data.ToList())
            {
                var jobCatDetails = await repo.GetCategoryDetailsByCode(item.JobCatCode);
                foreach (var jobCatDetail in jobCatDetails)
                {
                    var designationFile = await designationFileRepo.GetByCode(jobCatDetail.DesignationFileCode);
                    if (designationFile != null)
                    {
                        designationFiles.Add(designationFile);
                    }
                }
                
                // Add to List
                dataList.Add(new GetJobCategoryDto
                {
                    DesignationFiles = designationFiles,
                    JobCatCode = item.JobCatCode,
                    JobCatDesc = item.JobCatDesc,
                    JobGroupDesc = (await jobGroupRepo.GetByCode(item.JobGroupCode))?.JobGroupDesc,
                    JobGroupCode = item.JobGroupCode
                });

                // Instantiate new List
                designationFiles = new List<DesignationFile>();
            }
            return dataList;
        }

        public async Task<CustomMessage> Insert(JobCategoryDto dto)
        {
            var jobCatData = await repo.GetByCode(dto.JobCatCode);
            if(jobCatData == null)
            {
                dto.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(mapper.Map<JobCategory>(dto));

                dto.JobCategoryDetails.ToList().ForEach(x => x.JobCatCode = dto.JobCatCode );

                await repo.InsertJobCatDetail(dto.JobCategoryDetails);

                return CustomMessageHandler.RecordAdded();
            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(JobCategoryDto dto)
        {
            var jobCatData = await repo.GetByCode(dto.JobCatCode);
            if (jobCatData != null)
            {
                var data = mapper.Map<JobCategory>(dto);

                await repo.Update(data);
                // Delete 
                await repo.DeleteJobCatDetail(dto.JobCatCode);

                dto.JobCategoryDetails.ToList().ForEach(x => x.JobCatCode = data.JobCatCode);
                await repo.InsertJobCatDetail(dto.JobCategoryDetails);
                

                return CustomMessageHandler.RecordAdded();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }
    }
}
