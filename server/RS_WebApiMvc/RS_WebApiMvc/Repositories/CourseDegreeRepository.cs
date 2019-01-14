using Dapper;
using RS_WebApiMvc.Data;
using RS_WebApiMvc.Models;
using RS_WebApiMvc.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RS_WebApiMvc.Repositories
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