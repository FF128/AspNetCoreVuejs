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
    public class AppEntryGenInfoRepository : IAppEntryGenInfoRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public AppEntryGenInfoRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpGenInfo_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<AppEntryGenInfo>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<AppEntryGenInfo>("sp_AppEntrySetUpGenInfo_View");
            }
        }

        public async Task<AppEntryGenInfo> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<AppEntryGenInfo>("sp_AppEntrySetUpGenInfo_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(AppEntryGenInfo rate)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpGenInfo_Insert",
                    rate, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(AppEntryGenInfo rate)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpGenInfo_Update",
                    rate, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
