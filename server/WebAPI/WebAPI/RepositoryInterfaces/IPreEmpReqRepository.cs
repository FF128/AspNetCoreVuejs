using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IPreEmpReqRepository
    {
        Task<IEnumerable<PreEmpReqEmpStatusDesignationDto>> GetAll();
        Task<PreEmpReq> GetById(int id);
        Task<PreEmpReq> GetByCode(string code);
        Task Insert(PreEmpReq pre);
        Task Update(PreEmpReq pre);
        Task Delete(int id);
    }
}
