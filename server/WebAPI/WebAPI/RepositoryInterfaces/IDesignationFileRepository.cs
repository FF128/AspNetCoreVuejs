using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IDesignationFileRepository
    {
        Task<IEnumerable<DesignationFile>> GetAll();
        Task<DesignationFile> GetById(int id);
        Task Insert(DesignationFile designationFile);
        Task Update(DesignationFile designationFile);
        Task Delete(int id);
    }
}
