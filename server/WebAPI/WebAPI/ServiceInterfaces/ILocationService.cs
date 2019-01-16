using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface ILocationService
    {
        Task<CustomMessage> Insert(Location loc);
        Task<CustomMessage> Update(Location loc);
        Task<CustomMessage> Delete(int id);
    }
}
