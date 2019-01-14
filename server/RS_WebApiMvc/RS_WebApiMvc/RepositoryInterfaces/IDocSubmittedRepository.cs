using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface IDocSubmittedRepository
    {
        Task<IEnumerable<DocSubmitted>> GetAll();
        Task<DocSubmitted> GetById(int id);
        Task Insert(DocSubmitted ds);
        Task Update(DocSubmitted ds);
        Task Delete(int id);
    }
}
