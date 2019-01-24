using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface ISMSFormatService
    {
        Task<CustomMessage> Insert(SMSFormat smsFormat);
        Task<CustomMessage> Update(SMSFormat smsFormat);
    }
}
