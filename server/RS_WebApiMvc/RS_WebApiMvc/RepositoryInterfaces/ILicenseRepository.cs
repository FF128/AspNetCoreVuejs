using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface ILicenseRepository
    {
        Task<IEnumerable<License>> GetAll();
        Task<License> GetById(int id);
        Task Insert(License lic);
        Task Update(License lic);
        Task Delete(int id);
    }
}
