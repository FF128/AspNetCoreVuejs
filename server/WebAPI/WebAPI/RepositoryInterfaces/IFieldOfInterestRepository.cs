using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
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
