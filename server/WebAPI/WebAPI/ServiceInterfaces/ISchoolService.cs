using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface ISchoolService
    {
        Task<CustomMessage> Insert(School school);
        Task<CustomMessage> Update(School school);
        Task<CustomMessage> Delete(int id);
        Task<CustomMessage> DeleteByCode(string code);

    }
}
