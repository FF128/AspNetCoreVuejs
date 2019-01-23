using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IJobCategoryRepository
    {
        Task<IEnumerable<JobCategory>> GetAll();
        Task<JobCategory> GetById(int id);
        Task<JobCategory> GetByCode(string code);
        Task<IEnumerable<JobCategoryDetail>> GetCategoryDetailsByCode(string code);
        Task Insert(JobCategory jobGroup);
        Task Update(JobCategory jobGroup);
        Task InsertJobCatDetail(IEnumerable<JobCategoryDetail> jobCategoryDetails);
        Task Delete(string code);
        Task DeleteJobCatDetail(string code);
    }
}
