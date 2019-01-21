using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.ApplicantsEntrySetUp;

namespace WebAPI.RepositoryInterfaces
{
    public interface IAppEntryTextCertRepository
    {
        Task Insert(AppEntryTextCert text);
        Task Update(AppEntryTextCert text);
        Task<AppEntryTextCert> GetTextCert();
    }
}
