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
    public class FieldOfInterestRepository : IFieldOfInterestRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public FieldOfInterestRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_FieldInterestSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<FieldOfInterest>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<FieldOfInterest>("sp_FieldInterestSetUp_View");
            }
        }

        public async Task<FieldOfInterest> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<FieldOfInterest>("sp_FieldInterestSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<FieldOfInterest> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<FieldOfInterest>("sp_FieldInterestSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(FieldOfInterest dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_FieldInterestSetUp_Insert",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(FieldOfInterest dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_FieldInterestSetUp_Update",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }
    }
}