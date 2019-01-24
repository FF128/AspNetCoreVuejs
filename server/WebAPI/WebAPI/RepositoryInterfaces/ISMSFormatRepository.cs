using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface ISMSFormatRepository
    {
        Task<SMSFormat> GetByTransType(string transType);
        Task Insert(SMSFormat smsFormat);
        Task Update(SMSFormat smsFormat);
        Task<SMSFormat> GetById(int id);
    }
}
