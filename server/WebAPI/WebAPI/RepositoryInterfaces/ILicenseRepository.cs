using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
{
    public interface ILicenseRepository
    {
        Task<IEnumerable<License>> GetAll();
        Task<License> GetById(int id);
        Task<License> GetByCode(string code);
        Task Insert(License lic);
        Task Update(License lic);
        Task Delete(int id);
    }
}
