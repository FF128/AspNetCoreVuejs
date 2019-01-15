using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebAPI.RepositoryInterfaces
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<Language>> GetAll();
        Task<Language> GetById(int id);
        Task Insert(Language lang);
        Task Update(Language lang);
        Task Delete(int id);
    }
}