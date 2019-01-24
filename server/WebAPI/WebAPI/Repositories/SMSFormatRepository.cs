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
    public class SMSFormatRepository : ISMSFormatRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public SMSFormatRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task<SMSFormat> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QuerySingleOrDefaultAsync<SMSFormat>("sp_SMSFormatSetup_ViewById",
                        new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<SMSFormat> GetByTransType(string transType)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QuerySingleOrDefaultAsync<SMSFormat>("sp_SMSFormatSetup_ViewByTransType",
                        new { transType }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(SMSFormat emailFormat)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SMSFormatSetup_Insert",
                    emailFormat, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(SMSFormat emailFormat)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SMSFormatSetup_Update",
                    emailFormat, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
