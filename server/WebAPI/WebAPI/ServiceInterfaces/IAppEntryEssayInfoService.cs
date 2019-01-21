using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models.ApplicantsEntrySetUp;

namespace WebAPI.ServiceInterfaces
{
    public interface IAppEntryEssayInfoService
    {
        Task<CustomMessage> Insert(AppEntryEssayInfo info);
        Task<CustomMessage> Update(AppEntryEssayInfo info);
        Task<CustomMessage> Delete(int id);
    }
}
