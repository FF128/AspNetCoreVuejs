using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IEmailFormatRepository
    {
        Task<EmailFormat> GetByTransType(string transType);
        Task Insert(EmailFormat emailFormat);
        Task Update(EmailFormat emailFormat);
        Task<EmailFormat> GetById(int id);
    }
}
