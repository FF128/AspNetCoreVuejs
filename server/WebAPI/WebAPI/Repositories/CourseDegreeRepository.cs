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
    public class CourseDegreeRepository : ICourseDegreeRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public CourseDegreeRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_CourseDegreeSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<CourseDegree>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<CourseDegree>("sp_CourseDegreeSetUp_View");
            }
        }

        public async Task<CourseDegree> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<CourseDegree>("sp_CourseDegreeSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<CourseDegree> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<CourseDegree>("sp_CourseDegreeSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(CourseDegree dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_CourseDegreeSetUp_Insert",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(CourseDegree dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_CourseDegreeSetUp_Update",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }
    }
}