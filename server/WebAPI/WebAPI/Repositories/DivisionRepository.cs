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
using WebAPI.Dtos.Division;
using WebAPI.Dtos;

namespace WebAPI.Repositories
{
    public class DivisionRepository : IDivisionRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public DivisionRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DivisionSetUp_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFileSetUp(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDivision_DeleteByCode",
                    new { Code = code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromHRISFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDivision_DeleteFromHRIS",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromPayrollFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDivision_DeleteFromPayroll",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromTKSFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDivision_DeleteFromTKS",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Division>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Division>("sp_DivisionSetUp_View");
            }
        }

        public async Task<Division> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Division>("sp_DivisionSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsDivision_ViewByCodeFromHRIS",
                       new { Code = code, DBName = hrisDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsDivision_ViewByCodeFromPayroll",
                       new { Code = code, DBName = payrollDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromTKS(string code, string tskDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsDivision_ViewByCodeFromTKS",
                       new { Code = code, DBName = tskDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Division> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Division>("sp_DivisionSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Division dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DivisionSetUp_Insert",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertFileSetup(DivInsertFileSetupDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDivision_Insert",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToHRISFileSetUp(DivInsertToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDivision_InsertToHRIS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToPayrollFileSetUp(DivInsertToPayrollFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDivision_InsertToPayroll",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToTKSFileSetUp(DivInsertToTKSFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDivision_InsertToTKS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Division dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DivisionSetUp_Update",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateFileSetup(DivUpdateFileSetupDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDivision_Update",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToHRISFileSetUp(DivUpdateToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDivision_UpdateToHRIS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToPayrollFileSetUp(DivUpdateToPayrollFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDivision_UpdateToPayroll",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToTKSFileSetUp(DivUpdateToTKSFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDivision_UpdateToTKS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }
    }
}