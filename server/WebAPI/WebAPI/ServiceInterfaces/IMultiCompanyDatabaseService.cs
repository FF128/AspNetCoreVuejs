using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IMultiCompanyDatabaseService
    {
        Task<CustomMessage> Insert(MultiCompanyDatabase multiComp);
        Task<CustomMessage> Update(MultiCompanyDatabase multiComp);
        Task<CustomMessage> Delete(string code);
    }
}
