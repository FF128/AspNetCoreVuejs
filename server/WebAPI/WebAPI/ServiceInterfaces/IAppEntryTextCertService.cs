using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models.ApplicantsEntrySetUp;

namespace WebAPI.ServiceInterfaces
{
    public interface IAppEntryTextCertService
    {
        Task<CustomMessage> Insert(AppEntryTextCert text);
        Task<CustomMessage> Update(AppEntryTextCert text);
    }
}
