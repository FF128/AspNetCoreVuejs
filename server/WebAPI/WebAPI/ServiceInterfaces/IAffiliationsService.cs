using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IAffiliationsService
    {
        Task<CustomMessage> Insert(Affiliations aff);
        Task<CustomMessage> Update(Affiliations aff);
        Task<CustomMessage> Delete(int id);
    }
}
