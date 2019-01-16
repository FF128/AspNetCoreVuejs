using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IStepRepository
    {
        Task<IEnumerable<Step>> GetAll();
        Task<Step> GetById(int id);
        Task<Step> GetByCode(string code);
        Task Insert(Step step);
        Task Update(Step step);
        Task Delete(int id);
    }
}
