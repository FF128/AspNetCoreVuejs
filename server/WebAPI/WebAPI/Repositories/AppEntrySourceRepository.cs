using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models.ApplicantsEntrySetUp;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class AppEntrySourceRepository : IAppEntrySourceRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public AppEntrySourceRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpSourceInfo_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<AppEntrySource>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<AppEntrySource>("sp_AppEntrySetUpSourceInfo_View");
            }
        }

        public async Task<AppEntrySource> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<AppEntrySource>("sp_AppEntrySetUpSourceInfo_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(AppEntrySource source)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpSourceInfo_Insert",
                    source, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(AppEntrySource source)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpSourceInfo_Update",
                    source, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
