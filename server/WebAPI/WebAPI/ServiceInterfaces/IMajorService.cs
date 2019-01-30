using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IMajorService
    {
        Task<CustomMessage> Insert(Major major);
        Task<CustomMessage> Update(Major major);
        Task<CustomMessage> Delete(int id);
        Task<CustomMessage> DeleteByCode(string code);

    }
}
