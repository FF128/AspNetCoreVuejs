using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IPreEmpReqService
    {
        Task<CustomMessage> Insert(PreEmpReq pre);
        Task<CustomMessage> Update(PreEmpReq pre);
        Task<CustomMessage> Delete(int id);
        Task<IEnumerable<PreEmpReqEmpStatusDesignationDto>> GetAll();
    }
}
