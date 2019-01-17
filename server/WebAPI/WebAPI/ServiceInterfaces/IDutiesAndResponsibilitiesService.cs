using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IDutiesAndResponsibilitiesService
    {
        Task<CustomMessage> Insert(DutiesAndResponsibilities dar);
        Task<CustomMessage> Update(DutiesAndResponsibilities dar);
        Task<CustomMessage> Delete(int id);
    }

}
