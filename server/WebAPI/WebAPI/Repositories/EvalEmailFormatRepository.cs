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
    public class EvalEmailFormatRepository : IEvalEmailFormatRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public EvalEmailFormatRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task<EvalEmailFormat> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QuerySingleOrDefaultAsync<EvalEmailFormat>("sp_EvalEmailFormatSetup_ViewById",
                        new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<EvalEmailFormat> GetByTransType(string transType)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QuerySingleOrDefaultAsync<EvalEmailFormat>("sp_EvalEmailFormatSetup_ViewByTransType",
                        new { transType }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(EvalEmailFormat emailFormat)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_EvalEmailFormatSetup_Insert",
                    emailFormat, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(EvalEmailFormat emailFormat)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_EvalEmailFormatSetup_Update",
                    emailFormat, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
