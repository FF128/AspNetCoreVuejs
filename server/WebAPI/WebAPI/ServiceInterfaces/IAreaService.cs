using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IAreaService
    {
        Task<CustomMessage> Insert(Area area);
        Task<CustomMessage> Update(Area area);
        Task<CustomMessage> Delete(int id);
        Task<CustomMessage> DeleteByCode(string code);
    }
}
