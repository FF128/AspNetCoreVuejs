using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface IJobReqRepository
    {
        Task<IEnumerable<JobReq>> GetAll();
        Task<JobReq> GetById(int id);
        Task Insert(JobReq jr);
        Task Update(JobReq jr);
        Task Delete(int id);
    }
}
