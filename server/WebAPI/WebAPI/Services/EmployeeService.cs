using _128Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.ExtensionMethods;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repo;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IProtectionRepository protectionRepo;
        public EmployeeService(IEmployeeRepository repo,
            ICompanyInformationRepository compInfoRepo,
            IProtectionRepository protectionRepo)
        {
            this.repo = repo;
            this.compInfoRepo = compInfoRepo;
            this.protectionRepo = protectionRepo;
        }
        public async Task<IEnumerable<Employee>> GetAllEmployees(int pageSize, int pageNum, string query)
        {
            var companyInfo = await compInfoRepo.GetByCompanyCode(compInfoRepo.GetCompanyCode());
            if (companyInfo == null)
                throw new Exception("Company Information doesn't exist");
            if (!await compInfoRepo.CheckDBIfExists(companyInfo.HRISDB))
                throw new Exception("HRIS Database doesn't exist");

            return await GetDecryptedData(await repo.GetAllEmployees(pageSize, pageNum, query, companyInfo.HRISDB));
        }

        private async Task<IEnumerable<Employee>> GetDecryptedData(IEnumerable<Employee> employees)
        {
            Aes aes = new Aes();
            aes.AesCredentials = new EncryptionDetails();
            var data = await protectionRepo.GetData("128Techbl3$$1ng$");
            
            aes.AesCredentials.KeySize = data.KeySize;
            aes.AesCredentials.Salt = data.Salt;
            aes.AesCredentials.SecretKey = data.SKey;
            aes.AesCredentials.Algorithm = aes.GetAlgo(data.SKey, data.Salt, data.KeySize);
            var empList = new List<Employee>();
            foreach (var employee in employees)
            {
                var newEmployee = new Employee
                {
                    TotalRecords = employee.TotalRecords,
                    EmpCode = employee.EmpCode,
                    FName = aes.AesDecrypt(employee.FName),
                    LName = aes.AesDecrypt(employee.LName),
                    MName = aes.AesDecrypt(employee.MName)
                };

                empList.Add(newEmployee);
            }
            return empList;
        }
    }
}
