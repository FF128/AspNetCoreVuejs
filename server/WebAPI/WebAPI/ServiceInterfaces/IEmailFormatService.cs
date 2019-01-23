using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IEmailFormatService
    {
        Task<CustomMessage> Insert(EmailFormat emailFormat);
        Task<CustomMessage> Update(EmailFormat emailFormat);

    }
}
