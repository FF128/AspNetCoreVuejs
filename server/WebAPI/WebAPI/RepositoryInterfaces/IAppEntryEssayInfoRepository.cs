using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.ApplicantsEntrySetUp;

namespace WebAPI.RepositoryInterfaces
{
    public interface IAppEntryEssayInfoRepository
    {
        Task<IEnumerable<AppEntryEssayInfo>> GetAll();
        Task<AppEntryEssayInfo> GetById(int id);
        Task Insert(AppEntryEssayInfo info);
        Task Update(AppEntryEssayInfo info);
        Task Delete(int id);
    }
}
