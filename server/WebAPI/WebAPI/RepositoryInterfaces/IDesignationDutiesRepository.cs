using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IDesignationDutiesRepository
    {
        Task Insert(DesignationDuties data);
        Task InsertDutiesReq(IEnumerable<DesignationDutiesResponsibilities> data);
        Task InsertJobReq(IEnumerable<DesignationDutiesJobReq> data);

        Task Update(DesignationDuties data);
        Task UpdateDutiesReq(IEnumerable<DesignationDutiesResponsibilities> data);
        Task UpdateJobReq(IEnumerable<DesignationDutiesJobReq> data);

        Task<DesignationDuties> GetByCode(string code);
        Task<IEnumerable<DesignationDuties>> GetAll();
        Task<IEnumerable<DesignationDutiesResponsibilities>> GetDesignationDutiesResponsibilities(string code);
        Task<IEnumerable<DesignationDutiesJobReq>> GetDesignationDutiesJobReqs(string code);

        Task DeleteByCode(string code);
        Task DeleteDesignationDutiesResponsibilitiesByCode(string code);
        Task DeleteDesignationDutiesJobReqsByCode(string code);
    }
}
