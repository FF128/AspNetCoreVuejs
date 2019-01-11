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
    public class AffiliationsRepository : IAffiliationsRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public AffiliationsRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Affiliations_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Affiliations>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Affiliations>("sp_Affiliations_View");
            }
        }

        public async Task<Affiliations> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Affiliations>("sp_Affiliations_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Affiliations affiliations)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Affiliations_Insert",
                    affiliations, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Affiliations affiliations)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Affiliations_Update",
                    affiliations, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
