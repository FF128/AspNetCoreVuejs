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
using WebAPI.Dtos.Unit;
using WebAPI.Dtos;

namespace WebAPI.Repositories
{
    public class UnitRepository : IUnitRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public UnitRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_UnitSetUp_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFileSetUp(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsUnit_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromHRISFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsUnit_DeleteFromHRIS",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromPayrollFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsUnit_DeleteFromPayroll",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Unit>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Unit>("sp_UnitSetUp_View");
            }
        }

        public async Task<Unit> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Unit>("sp_UnitSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsUnit_ViewByCodeFromHRIS",
                       new { Code = code, DBName = hrisDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsUnit_ViewByCodeFromPayroll",
                       new { Code = code, DBName = payrollDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Unit> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Unit>("sp_UnitSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Unit unit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_UnitSetUp_Insert",
                    unit, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertFileSetup(Unit unit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsUnit_Insert",
                    unit, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToHRISFileSetUp(UnitInsertToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsUnit_InsertToHRIS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToPayrollFileSetUp(UnitInsertToPayrollFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsUnit_InsertToPayroll",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Unit unit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_UnitSetUp_Update",
                    unit, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateFileSetup(Unit unit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsUnit_Update",
                    unit, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToHRISFileSetUp(UnitUpdateToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsUnit_UpdateToHRIS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToPayrollFileSetUp(UnitUpdateToPayrollFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsUnit_UpdateToPayroll",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }
    }
}