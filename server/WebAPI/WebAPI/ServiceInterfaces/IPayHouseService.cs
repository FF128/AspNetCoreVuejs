using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IPayHouseService
    {
        Task<CustomMessage> Insert(PayHouse ph);
        Task<CustomMessage> Update(PayHouse ph);
        Task<CustomMessage> Delete(int id);
    }
}
