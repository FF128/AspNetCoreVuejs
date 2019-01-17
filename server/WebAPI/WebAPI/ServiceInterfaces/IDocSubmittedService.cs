using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IDocSubmittedService
    {
        Task<CustomMessage> Insert(DocSubmitted dc);
        Task<CustomMessage> Update(DocSubmitted dc);
        Task<CustomMessage> Delete(int id);
    }
}
