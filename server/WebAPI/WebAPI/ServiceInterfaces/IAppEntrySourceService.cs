using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models.ApplicantsEntrySetUp;

namespace WebAPI.ServiceInterfaces
{
    public interface IAppEntrySourceService
    {
        Task<CustomMessage> Insert(AppEntrySource source);
        Task<CustomMessage> Update(AppEntrySource source);
        Task<CustomMessage> Delete(int id);
    }
}
