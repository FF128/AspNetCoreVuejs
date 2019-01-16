using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IRankService
    {
        Task<CustomMessage> Insert(Rank rank);
        Task<CustomMessage> Update(Rank rank);
        Task<CustomMessage> Delete(int id);
    }
}
