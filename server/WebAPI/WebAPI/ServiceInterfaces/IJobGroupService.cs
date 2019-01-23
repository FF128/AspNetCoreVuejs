using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IJobGroupService
    {
        Task<CustomMessage> Insert(JobGroup jobGroup);
        Task<CustomMessage> Update(JobGroup jobGroup);
        Task<CustomMessage> Delete(int id);
    }
}
