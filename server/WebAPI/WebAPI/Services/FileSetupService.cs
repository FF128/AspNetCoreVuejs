using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class FileSetupService : IFileSetupService
    {
        public Task<bool> CheckIfExists(string databaseName, string tableName, string code)
        {
            throw new NotImplementedException();
        }
    }
}
