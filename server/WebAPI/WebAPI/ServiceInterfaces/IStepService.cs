using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IStepService
    {
        Task<CustomMessage> Insert(Step step);
        Task<CustomMessage> Update(Step step);
        Task<CustomMessage> Delete(int id);
    }
}
