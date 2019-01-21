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
    public class AppEntryEssayInfoRepository : IAppEntryEssayInfoRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public AppEntryEssayInfoRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpEssayInfo_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<AppEntryEssayInfo>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<AppEntryEssayInfo>("sp_AppEntrySetUpEssayInfo_View");
            }
        }

        public async Task<AppEntryEssayInfo> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<AppEntryEssayInfo>("sp_AppEntrySetUpEssayInfo_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(AppEntryEssayInfo rate)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpEssayInfo_Insert",
                    rate, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(AppEntryEssayInfo rate)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpEssayInfo_Update",
                    rate, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
