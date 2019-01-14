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
    public class SectionRepository : ISectionRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public SectionRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SectionSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Section>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Section>("sp_SectionSetUp_View");
            }
        }

        public async Task<Section> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Section>("sp_SectionSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Section sec)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SectionSetUp_Insert",
                    sec, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task Update(Section sec)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SectionSetUp_Update",
                    sec, commandType: CommandType.StoredProcedure);
            }
        }
    }
}