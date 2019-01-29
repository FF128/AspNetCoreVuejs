using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IBranchService
    {
        Task<CustomMessage> Insert(Branch branch);
        Task<CustomMessage> Update(Branch branch);
        Task<CustomMessage> Delete(int id);
        Task<CustomMessage> DeleteByCode(string code);
    }
}
