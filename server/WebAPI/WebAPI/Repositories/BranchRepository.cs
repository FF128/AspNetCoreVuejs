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
    public class BranchRepository : IBranchRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public BranchRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_BranchSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Branch>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Branch>("sp_BranchSetUp_View");
            }
        }

        public async Task<Branch> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Branch>("sp_BranchSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Branch> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Branch>("sp_BranchSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Branch branch)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_BranchSetUp_Insert",
                    branch, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Branch branch)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_BranchSetUp_Update",
                    branch, commandType: CommandType.StoredProcedure);
            }
        }
    }
}