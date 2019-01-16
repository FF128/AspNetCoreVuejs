using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IProjectCodeService
    {
        Task<CustomMessage> Insert(ProjectCodeModel proj);
        Task<CustomMessage> Update(ProjectCodeModel proj);
        Task<CustomMessage> Delete(int id);
    }
}
