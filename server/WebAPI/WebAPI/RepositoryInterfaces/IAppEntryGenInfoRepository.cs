using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.ApplicantsEntrySetUp;

namespace WebAPI.RepositoryInterfaces
{
    public interface IAppEntryGenInfoRepository
    {
        Task<IEnumerable<AppEntryGenInfo>> GetAll();
        Task<AppEntryGenInfo> GetById(int id);
        Task Insert(AppEntryGenInfo genInfo);
        Task Update(AppEntryGenInfo genInfo);
        Task Delete(int id);
    }
}
