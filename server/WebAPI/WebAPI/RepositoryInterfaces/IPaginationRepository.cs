using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.RepositoryInterfaces
{
    public interface IPaginationRepository<T> where T : class
    {
        Task<IEnumerable<T>> Pagination(string command, int pageNumber, int pageSize, string query);
    }
}
