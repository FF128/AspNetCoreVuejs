using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IResidenceTypeService
    {
        Task<CustomMessage> Insert(ResidenceType rt);
        Task<CustomMessage> Update(ResidenceType rt);
        Task<CustomMessage> Delete(int id);
    }

}
