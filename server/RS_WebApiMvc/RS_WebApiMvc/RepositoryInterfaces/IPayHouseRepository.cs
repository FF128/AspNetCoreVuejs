using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface IPayHouseRepository
    {
        Task<IEnumerable<PayHouse>> GetAll();
        Task<PayHouse> GetById(int id);
        Task Insert(PayHouse ph);
        Task Update(PayHouse ph);
        Task Delete(int id);
    }
}
