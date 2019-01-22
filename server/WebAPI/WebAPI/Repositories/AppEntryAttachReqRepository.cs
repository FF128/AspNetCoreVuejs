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
    public class AppEntryAttachReqRepository : IAppEntryAttachReqRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public AppEntryAttachReqRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpAttachment_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<AppEntryAttachReq>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<AppEntryAttachReq>("sp_AppEntrySetUpAttachment_View");
            }
        }

        public async Task<AppEntryAttachReq> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<AppEntryAttachReq>("sp_AppEntrySetUpAttachment_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(AppEntryAttachReq rate)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpAttachment_Insert",
                    rate, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(AppEntryAttachReq rate)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpAttachment_Update",
                    rate, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
