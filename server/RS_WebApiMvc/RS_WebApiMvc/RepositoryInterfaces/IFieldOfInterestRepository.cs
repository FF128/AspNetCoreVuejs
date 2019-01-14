using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface IFieldOfInterestRepository
    {
        Task<IEnumerable<FieldOfInterest>> GetAll();
        Task<FieldOfInterest> GetById(int id);
        Task Insert(FieldOfInterest foi);
        Task Update(FieldOfInterest foi);
        Task Delete(int id);
    }
}
