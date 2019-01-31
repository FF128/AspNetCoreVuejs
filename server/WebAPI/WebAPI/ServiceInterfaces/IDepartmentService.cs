using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IDepartmentService
    {
        Task<CustomMessage> Insert(Department dep);
        Task<CustomMessage> Update(Department dep);
        Task<CustomMessage> Delete(int id);
        Task<CustomMessage> DeleteByCode(string code);
    }
}
