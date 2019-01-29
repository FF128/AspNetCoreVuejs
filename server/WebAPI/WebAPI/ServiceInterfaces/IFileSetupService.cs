using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ServiceInterfaces
{
    public interface IFileSetupService
    {
        Task<bool> CheckIfExists(string databaseName,string tableName, string code);
    }
}
