using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IRatingsService
    {
        Task<CustomMessage> Insert(Ratings rate);
        Task<CustomMessage> Update(Ratings rate);
        Task<CustomMessage> Delete(int id);
    }
}
