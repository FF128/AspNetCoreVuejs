using _128Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.ExtensionMethods
{
    public static class EmployeeExtension
    {
        
        public static IEnumerable<Employee> MapEmployeeDetails(this IEnumerable<Employee> employees)
        {
            Aes aes = new Aes();
            var empList = new List<Employee>();
            foreach(var employee in employees)
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
