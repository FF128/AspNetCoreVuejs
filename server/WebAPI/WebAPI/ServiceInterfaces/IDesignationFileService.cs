using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IDesignationFileService
    {
        Task<CustomMessage> Insert(DesignationFile df);
        Task<CustomMessage> Update(DesignationFile df);
        Task<CustomMessage> Delete(int id);
    }
}
