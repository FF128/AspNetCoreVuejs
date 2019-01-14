using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetAll();
        Task<Branch> GetById(int id);
        Task Insert(Branch branch);
        Task Update(Branch branch);
        Task Delete(int id);
    }
}
