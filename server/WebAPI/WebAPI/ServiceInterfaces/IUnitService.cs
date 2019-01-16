using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IUnitService
    {
        Task<CustomMessage> Insert(Unit unit);
        Task<CustomMessage> Update(Unit unit);
        Task<CustomMessage> Delete(int id);
    }
}
