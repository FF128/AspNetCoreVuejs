using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface ISectionRepository
    {
        Task<IEnumerable<Section>> GetAll();
        Task<Section> GetById(int id);
        Task Insert(Section sec);
        Task Update(Section sec);
        Task Delete(int id);
    }
}
