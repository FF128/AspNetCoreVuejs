using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetAll();
        Task<Branch> GetById(int id);
        Task<Branch> GetByCode(string code);
        Task Insert(Branch branch);
        Task Update(Branch branch);
        Task Delete(int id);
    }
}
