using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface ICitizenshipService
    {
        Task<CustomMessage> Insert(Citizenship cit);
        Task<CustomMessage> Update(Citizenship cit);
        Task<CustomMessage> Delete(int id);
    }
}
