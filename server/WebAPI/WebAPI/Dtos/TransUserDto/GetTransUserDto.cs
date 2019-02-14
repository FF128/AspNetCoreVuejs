using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos.DepartmentDto;
using WebAPI.Models.TransUserModel;

namespace WebAPI.Dtos.TransUserDto
{
    public class GetTransUserDto
    {
        public long ID { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string EmpCode { get; set; }
        public string Trans { get; set; }
        public int Order { get; set; }
        public string CompanyCode { get; set; }
        //public IEnumerable<TransUserDepartment>  TransUserDepartments { get; set; }
        //public IEnumerable<TransUserPaylocation> TransUserPaylocations { get; set; }
        public IEnumerable<PayLocationDto> PayLocations { get; set; }
        public IEnumerable<GetDeparmentDto> Departments { get; set; }
    }
}
