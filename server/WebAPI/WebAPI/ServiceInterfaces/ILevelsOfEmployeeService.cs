using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface ILevelsOfEmployeeService
    {
        Task<CustomMessage> Insert(LevelsOfEmployee loe);
        Task<CustomMessage> Update(LevelsOfEmployee loe);
        Task<CustomMessage> Delete(int id);
    }
}
