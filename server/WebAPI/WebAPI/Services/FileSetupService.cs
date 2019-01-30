using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class FileSetupService : IFileSetupService
    {
        public Task<Tuple<bool, string>> CheckIfExists(string databaseName, string tableName, string code)
        {
            throw new NotImplementedException();
        }

        public (bool hasError, string message) Validate(CompanyInfoDto dto, bool payrollDB, bool tksDB, bool hrisDB)
        {
            if(dto == null)
            {
                return (false, "Company Information doesn't exist");
            }
            if (!payrollDB)
            {
                return (false, "Payroll Database doesn't exist!");
            }
            if (!tksDB)
            {
                return (false, "Timekeeping Database doesn't exist!");
            }
            if (!hrisDB)
            {
                return (false, "HRIS Database doesn't exist!");
            }

            return (true, null);
        }
    }
}
