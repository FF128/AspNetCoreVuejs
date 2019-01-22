using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models.ApplicantsEntrySetUp;

namespace WebAPI.ServiceInterfaces
{
    public interface IAppEntryAttachReqService
    {
        Task<CustomMessage> Insert(AppEntryAttachReq attachReq);
        Task<CustomMessage> Update(AppEntryAttachReq attachReq);
        Task<CustomMessage> Delete(int id);
    }
}
