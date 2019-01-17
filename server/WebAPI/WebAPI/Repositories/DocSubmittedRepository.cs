using Dapper;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WebAPI.Repositories
{
    public class DocSubmittedRepository : IDocSubmittedRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public DocSubmittedRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DocSubmittedSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<DocSubmitted>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<DocSubmitted>("sp_DocSubmittedSetUp_View");
            }
        }

        public async Task<DocSubmitted> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<DocSubmitted>("sp_DocSubmittedSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<DocSubmitted> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<DocSubmitted>("sp_DocSubmittedSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(DocSubmitted ds)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DocSubmittedSetUp_Insert",
                    ds, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task Update(DocSubmitted ds)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DocSubmittedSetUp_Update",
                    ds, commandType: CommandType.StoredProcedure);
            }
        }
    }
}