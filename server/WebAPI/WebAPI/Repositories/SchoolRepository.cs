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
using WebAPI.Dtos.School;

namespace WebAPI.Repositories
{
    public class SchoolRepository : ISchoolRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public SchoolRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SchoolSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsSchool_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<School>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<School>("sp_SchoolSetUp_View");
            }
        }

        public async Task<School> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<School>("sp_SchoolSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsSchool_ViewByCodeFromHRIS",
                       new { Code = code, DBName = hrisDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<School> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<School>("sp_SchoolSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(School sec)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SchoolSetUp_Insert",
                    sec, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertFileSetup(School school)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsSchool_Insert",
                    school, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToHRISFileSetUp(SchoolInsertToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                var result = await conn.ExecuteAsync("sp_tbl_fsSchool_InsertToHRIS",
                     dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(School sec)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SchoolSetUp_Update",
                    sec, commandType: CommandType.StoredProcedure);
            }
        }
    }
}