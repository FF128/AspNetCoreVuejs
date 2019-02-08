using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models.PRModel;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class PRRepository : IPRRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public PRRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<int> Insert(PRFHeaderMaint pr)
        {
            using(var conn = connectionFactory.Connection)
            {
                conn.Open();
                using(var trans = conn.BeginTransaction())
                {
                    try
                    {
                        var identity = await conn.QueryFirstOrDefaultAsync<int>("sp_PRFHeaderMaint_Insert", pr,
                                            trans, commandType: CommandType.StoredProcedure);
                        trans.Commit();
                        return identity;
                    }
                    catch
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }

        public async Task InsertDetails(IEnumerable<PRFDetailsMaint> details)
        {
            using (var conn = connectionFactory.Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.ExecuteAsync("sp_PRFDetailsMaint_Insert", details,
                            trans, commandType: CommandType.StoredProcedure);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }
        }

        public async Task UpdatePRFNo(int identity, string prfNo)
        {
            using (var conn = connectionFactory.Connection)
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        await conn.ExecuteAsync("sp_PRFHeaderMaint_UpdatePRFNo", new { PRFNo = prfNo, ID = identity },
                            trans, commandType: CommandType.StoredProcedure);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                    }
                }
            }
        }
    }
}
