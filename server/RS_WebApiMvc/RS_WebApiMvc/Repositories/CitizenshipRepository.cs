using Dapper;
using RS_WebApiMvc.Data;
using RS_WebApiMvc.Models;
using RS_WebApiMvc.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RS_WebApiMvc.Repositories
{
    public class CitizenshipRepository : ICitizenshipRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public CitizenshipRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Citizenship_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Citizenship>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Citizenship>("sp_Citizenship_View");
            }
        }

        public async Task<Citizenship> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Citizenship>("sp_Citizenship_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Citizenship cit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Citizenship_Insert",
                    cit, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Citizenship cit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Citizenship_Update",
                    cit, commandType: CommandType.StoredProcedure);
            }
        }
    }
}