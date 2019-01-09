using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IGradeRepository
    {
        Task<IEnumerable<Grade>> GetAll();
        Task<Grade> GetById(int id);
        Task Insert(Grade grade);
        Task Update(Grade grade);
        Task Delete(int id);
    }
}
