using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IPersonnelRequestTypeService
    {
        Task<CustomMessage> Insert(PersonnelRequestType prt);
        Task<CustomMessage> Update(PersonnelRequestType prt);
        Task<CustomMessage> Delete(int id);
    }
}
