using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;

namespace WebAPI.ServiceInterfaces
{
    public interface IFileSetupService
    {
        Task<Tuple<bool, string>> CheckIfExists(string databaseName,string tableName, string code);
        (bool hasError, string message) Validate(CompanyInfoDto dto, bool payrollDB, bool tksDB, bool hrisDB);
    }
}
