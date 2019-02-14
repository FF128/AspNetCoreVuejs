using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Dtos.DepartmentDto;
using WebAPI.Dtos.TransUserDto;
using WebAPI.Helpers;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class TransUserService : ITransUserService
    {
        private readonly ITransUserRepository repo;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IUserRepository userRepo;
        private readonly IDepartmentRepository depRepo;
        private readonly IPayLocationRepository payLocRepo;
        private readonly IPayLocationService payLocService;
        private readonly IEmployeeService empService;
        public TransUserService(ITransUserRepository repo,
            ICompanyInformationRepository compInfoRepo,
            IUserRepository userRepo,
            IDepartmentRepository depRepo,
            IPayLocationRepository payLocRepo,
            IPayLocationService payLocService,
            IEmployeeService empService)
        {
            this.repo = repo;
            this.compInfoRepo = compInfoRepo;
            this.userRepo = userRepo;
            this.depRepo = depRepo;
            this.payLocRepo = payLocRepo;
            this.payLocService = payLocService;
            this.empService = empService;
        }

        public async Task<IEnumerable<GetTransUserDto>> GetAll(string trans)
        {
            var dataList = new List<GetTransUserDto>();
            var transUsers = await repo.GetAllByTrans(compInfoRepo.GetCompanyCode(), trans);
            foreach (var transUser in transUsers)
            {
                // Department
                var transUserDepartments = await repo.GetTransUserDepartments(transUser.ID, transUser.CompanyCode);
                var departmentList = new List<GetDeparmentDto>();
                foreach (var item in transUserDepartments)
                {
                    var department = await depRepo.GetByCode(item.DepartmentCode);

                    departmentList.Add(new GetDeparmentDto
                    {   
                        DepartmentCode = department.DepartmentCode,
                        DepartmentDesc = department.DepartmentDesc
                    });
                }
                // Payroll Location
                var transUserPayLocations = await repo.GetTransUserPaylocations(transUser.ID, transUser.CompanyCode);
                var payLocList = new List<PayLocationDto>();
                foreach (var item in transUserPayLocations)
                {
                    var payLoc = await payLocService.GetPayLocationById(item.LocId);
                    payLocList.Add(new PayLocationDto
                    {
                        LocId = payLoc.LocId,
                        LocName = payLoc.LocName
                    });
                }
                dataList.Add(new GetTransUserDto
                {
                    PayLocations = payLocList,
                    Departments = departmentList,
                    ID = transUser.ID,
                    Trans = transUser.Trans,
                    CompanyCode = transUser.CompanyCode,
                    IsActive = transUser.IsActive,
                    EmpCode = transUser.EmpCode,
                    Order = transUser.Order
                });
            }
            return dataList;

        }

        public async Task<CustomMessage> Insert(InsertTransUserDto dto)
        {
            if (String.IsNullOrEmpty(dto.TransUser.EmpCode))
                throw new Exception("Invalid employee code. Please select user");
            if (!await empService.GetByEmpCode(dto.TransUser.EmpCode))
                throw new Exception("Employee doesn't exist");
            if (await repo.GetByEmpCode(dto.TransUser.EmpCode, compInfoRepo.GetCompanyCode()))
                throw new Exception("User already exists");

            var companyCode = compInfoRepo.GetCompanyCode();
            dto.TransUser.CreatedBy = userRepo.GetEmpCode();
            dto.TransUser.IsActive = true;
            dto.TransUser.CompanyCode = companyCode;

            var identity = await repo.Insert(dto.TransUser);
            if (identity == 0)
                throw new Exception($"Invalid TransID ({identity})");

            dto.TransUserDepartments.ToList().ForEach(dep =>
            {
                dep.CompanyCode = companyCode;
                dep.TransID = identity;
            });

            dto.TransUserPaylocations.ToList().ForEach(loc =>
            {
                loc.CompanyCode = companyCode;
                loc.TransID = identity;
            });
            await repo.InserTransUserPayLoc(dto.TransUserPaylocations);
            await repo.InsertTransUserDepartment(dto.TransUserDepartments);

            return CustomMessageHandler.RecordAdded();
        }
    }
}
