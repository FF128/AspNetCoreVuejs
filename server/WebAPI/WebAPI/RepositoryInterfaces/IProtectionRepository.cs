using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IProtectionRepository
    {
        Task<Protection> GetData(string mode);
    }
}
