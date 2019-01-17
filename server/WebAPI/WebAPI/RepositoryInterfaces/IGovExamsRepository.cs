using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
{
    public interface IGovExamsRepository
    {
        Task<IEnumerable<GovExams>> GetAll();
        Task<GovExams> GetById(int id);
        Task<GovExams> GetByCode(string code);
        Task Insert(GovExams ge);
        Task Update(GovExams ge);
        Task Delete(int id);
    }
}
