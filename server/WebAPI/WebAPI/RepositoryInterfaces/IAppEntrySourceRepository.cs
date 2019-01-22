using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.ApplicantsEntrySetUp;

namespace WebAPI.RepositoryInterfaces
{
    public interface IAppEntrySourceRepository
    {
        Task<IEnumerable<AppEntrySource>> GetAll();
        Task<AppEntrySource> GetById(int id);
        Task Insert(AppEntrySource source);
        Task Update(AppEntrySource source);
        Task Delete(int id);
    }
}
