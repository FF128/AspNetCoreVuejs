using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Dtos;

namespace WebAPI.ServiceInterfaces
{
    public interface IDesignationDutiesReqService
    {
        Task<CustomMessage> Insert(DesignationDutiesReqDto dto);
        Task<IEnumerable<GetDesignationDutiesReqDto>> GetAll();

        Task<CustomMessage> Delete(string code);
    }
}
