using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class PaginationRepository<T> :IPaginationRepository<T> where T : class
    {
        private readonly IConnectionFactory connectionFactory;
        public PaginationRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task<IEnumerable<T>> Pagination(string command, int pageNumber, int pageSize, string query)
        {
            using (var conn = connectionFactory.Connection)
            {

                return
                    await conn.QueryAsync<T>(command,
                        new { PageNumber = pageNumber, PageSize = pageSize, Query = query },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<T>> Pagination(string command, int pageNumber, int pageSize, string query, string companyCode)
        {
            using (var conn = connectionFactory.Connection)
            {

                return
                    await conn.QueryAsync<T>(command,
                        new { PageNumber = pageNumber, PageSize = pageSize, Query = query, CompanyCode = companyCode },
                        commandType: CommandType.StoredProcedure);
            }
        }
    }
}
