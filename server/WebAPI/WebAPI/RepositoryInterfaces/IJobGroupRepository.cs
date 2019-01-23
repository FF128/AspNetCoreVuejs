using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IJobGroupRepository
    {
        Task<IEnumerable<JobGroup>> GetAll();
        Task<JobGroup> GetById(int id);
        Task<JobGroup> GetByCode(string code);
        Task Insert(JobGroup jobGroup);
        Task Update(JobGroup jobGroup);
        Task Delete(int id);
    }
}
