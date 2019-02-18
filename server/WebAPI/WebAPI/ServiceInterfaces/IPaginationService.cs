using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ServiceInterfaces
{
    public interface IPaginationService<T> where T: class
    {
        Task<IEnumerable<T>> PaginationByCompanyCode(string command, int pageNumber, int pageSize, string query);

    }
}
