using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IJobLevelService
    {
        Task<CustomMessage> Insert(JobLevel jl);
        Task<CustomMessage> Update(JobLevel jl);
        Task<CustomMessage> Delete(int id);
    }
}
