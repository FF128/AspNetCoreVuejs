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
using WebAPI.Dtos.CourseDto;
using WebAPI.Dtos;

namespace WebAPI.Repositories
{
    public class CourseDegreeRepository : ICourseDegreeRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public CourseDegreeRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_CourseDegreeSetUp_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFileSetUp(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsCourseDegree_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromHRISFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsCourseDegree_DeleteFromHRIS",
                    dto,
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

        public async Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsCourseDegree_ViewByCodeFromHRIS",
                       new { Code = code, DBName = hrisDB },
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

        public async Task InsertFileSetup(CourseDegree cd)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsCourseDegree_Insert",
                    cd, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToHRISFileSetUp(CourseInsertToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                var result = await conn.ExecuteAsync("sp_tbl_fsCourseDegree_InsertToHRIS",
                     dto, commandType: CommandType.StoredProcedure);
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

        public async Task UpdateFileSetup(CourseDegree cd)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsCourseDegree_Update",
                    cd, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToHRISFileSetUp(CourseUpdateToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                var result = await conn.ExecuteAsync("sp_tbl_fsCourseDegree_UpdateToHRIS",
                     dto, commandType: CommandType.StoredProcedure);
            }
        }
    }
}