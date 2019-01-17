using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface ILicenseService
    {
        Task<CustomMessage> Insert(License lic);
        Task<CustomMessage> Update(License lic);
        Task<CustomMessage> Delete(int id);
    }
}
