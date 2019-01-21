using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IOverallRatingsService
    {
        Task<CustomMessage> Insert(OverallRatings rate);
        Task<CustomMessage> Update(OverallRatings rate);
        Task<CustomMessage> Delete(int id);
    }
}
