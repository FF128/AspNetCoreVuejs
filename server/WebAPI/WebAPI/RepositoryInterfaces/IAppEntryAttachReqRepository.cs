using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.ApplicantsEntrySetUp;

namespace WebAPI.RepositoryInterfaces
{
    public interface IAppEntryAttachReqRepository
    {
        Task<IEnumerable<AppEntryAttachReq>> GetAll();
        Task<AppEntryAttachReq> GetById(int id);
        Task Insert(AppEntryAttachReq attachReq);
        Task Update(AppEntryAttachReq attachReq);
        Task Delete(int id);
    }
}
