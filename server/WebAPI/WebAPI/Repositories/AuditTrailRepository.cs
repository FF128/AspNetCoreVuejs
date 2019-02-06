using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class AuditTrailRepository : IAuditTrailRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public AuditTrailRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        //public Task Delete(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<IEnumerable<AuditTrail>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<AuditTrail>("sp_AuditTrail_View");
            }
        }

        public Task<AuditTrail> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Insert(AuditTrail auditTrail)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AuditTrail_Insert", auditTrail,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<dynamic>> Pagination(int pageNumber, int pageSize, string query)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync("sp_AuditTrail_Pagination", 
                        new { PageNumber = pageNumber, PageSize= pageSize, Query = query },
                        commandType: CommandType.StoredProcedure);
            }
        }
    }
}
