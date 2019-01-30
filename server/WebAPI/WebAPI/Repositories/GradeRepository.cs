using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Dtos.GradeDto;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public GradeRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Grade_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsGrade_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Grade>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Grade>("sp_Grade_View");
            }
        }

        public async Task<Grade> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Grade>("sp_Grade_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsGrade_ViewByCodeFromHRIS",
                       new { Code = code, DBName = hrisDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsGrade_ViewByCodeFromPayroll",
                       new { Code = code, DBName = payrollDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromTKS(string code, string tskDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsGrade_ViewByCodeFromTKS",
                       new { Code = code, DBName = tskDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Grade> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Grade>("sp_Grade_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Grade grade)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Grade_Insert",
                    grade, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertFileSetup(Grade grade)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsGrade_Insert",
                    grade, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToHRISFileSetUp(GradeInsertToFileSetupDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                var result = await conn.ExecuteAsync("sp_tbl_fsGrade_InsertToHRIS",
                     dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToPayrollFileSetUp(GradeInsertToFileSetupDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                var result = await conn.ExecuteAsync("sp_tbl_fsGrade_InsertToPayroll",
                     dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToTSKFileSetUp(GradeInsertToFileSetupDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                var result = await conn.ExecuteAsync("sp_tbl_fsGrade_InsertToTKS",
                     dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Grade grade)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Grade_Update",
                    grade, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
