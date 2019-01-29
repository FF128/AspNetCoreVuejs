using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IDivisionService
    {
        Task<CustomMessage> Insert(Division div);
        Task<CustomMessage> Update(Division div);
        Task<CustomMessage> Delete(int id);
        Task<CustomMessage> DeleteByCode(string code);
    }
}
