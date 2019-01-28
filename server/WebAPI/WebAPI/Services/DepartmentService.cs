using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository repo;
        private readonly IAuditTrailService<Department> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public DepartmentService(IDepartmentRepository repo,
             IAuditTrailService<Department> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var Department = await repo.GetById(id);
            if (Department != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Department(), Department, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Department dep)
        {
            if (String.IsNullOrEmpty(dep.DepartmentCode) || String.IsNullOrWhiteSpace(dep.DepartmentDesc))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(dep.DepartmentCode)) == null)
            {
                dep.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(dep);

                await auditTrailService.Save(new Department(), dep, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Department dep)
        {
            var departmentData = await repo.GetByCode(dep.DepartmentCode);
            if (departmentData != null)
            {
                await repo.Update(dep);

                //Audit Trail
                await auditTrailService.Save(departmentData, dep, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
