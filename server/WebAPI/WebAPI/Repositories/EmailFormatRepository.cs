using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using Dapper;
using System.Data;

namespace WebAPI.Repositories
{
    public class EmailFormatRepository : IEmailFormatRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public EmailFormatRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task<EmailFormat> GetById(int id)
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                    await conn.QuerySingleOrDefaultAsync<EmailFormat>("sp_EmailFormatSetup_ViewById",
                        new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<EmailFormat> GetByTransType(string transType)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QuerySingleOrDefaultAsync<EmailFormat>("sp_EmailFormatSetup_ViewByTransType",
                        new { transType }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(EmailFormat emailFormat)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_EmailFormatSetup_Insert",
                    emailFormat, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(EmailFormat emailFormat)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_EmailFormatSetup_Update",
                    emailFormat, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
