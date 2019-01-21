using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models.ApplicantsEntrySetUp;
using WebAPI.RepositoryInterfaces;
using Dapper;
using System.Data;

namespace WebAPI.Repositories
{
    public class AppEntryTextCertRepository : IAppEntryTextCertRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public AppEntryTextCertRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<AppEntryTextCert> GetTextCert()
        {
            using(var conn = connectionFactory.Connection)
            {
                return 
                    await conn.QueryFirstOrDefaultAsync<AppEntryTextCert>("sp_AppEntrySetUpTextCert_View", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(AppEntryTextCert text)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpTextCert_Insert", text,commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(AppEntryTextCert text)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AppEntrySetUpTextCert_Update", text, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
