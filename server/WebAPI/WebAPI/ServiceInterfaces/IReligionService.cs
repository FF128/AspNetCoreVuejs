using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IReligionService
    {
        Task<CustomMessage> Insert(Religion rel);
        Task<CustomMessage> Update(Religion rel);
        Task<CustomMessage> Delete(int id);
        Task<CustomMessage> DeleteByCode(string code);
    }
}
