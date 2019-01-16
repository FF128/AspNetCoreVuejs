using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IEmployeeStatusFileService
    {
        Task<CustomMessage> Insert(EmployeeStatusFile status);
        Task<CustomMessage> Update(EmployeeStatusFile status);
        Task<CustomMessage> Delete(int id);
    }
}
