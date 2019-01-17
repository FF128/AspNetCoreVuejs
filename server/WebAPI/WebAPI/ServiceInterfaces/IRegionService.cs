using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IRegionService
    {
        Task<CustomMessage> Insert(Region reg);
        Task<CustomMessage> Update(Region reg);
        Task<CustomMessage> Delete(int id);
    }
}
