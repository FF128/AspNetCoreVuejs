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
using WebAPI.Dtos.Department;
using WebAPI.Dtos;

namespace WebAPI.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public DepartmentRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DepartmentSetUp_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFileSetUp(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDepartment_DeleteByCode",
                    new { Code = code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromHRISFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDepartment_DeleteFromHRIS",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromPayrollFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDepartment_DeleteFromPayroll",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromTKSFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDepartment_DeleteFromTKS",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Department>("sp_DepartmentSetUp_View");
            }
        }

        public async Task<Department> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Department>("sp_DepartmentSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsDepartment_ViewByCodeFromHRIS",
                       new { Code = code, DBName = hrisDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsDepartment_ViewByCodeFromPayroll",
                       new { Code = code, DBName = payrollDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromTKS(string code, string tskDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsDepartment_ViewByCodeFromTKS",
                       new { Code = code, DBName = tskDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Department> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Department>("sp_DepartmentSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Department dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DepartmentSetUp_Insert",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertFileSetup(Department dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDepartment_Insert",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToHRISFileSetUp(DepartmentInsertToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDepartment_InsertToHRIS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToPayrollFileSetUp(DepartmentInsertToPayrollFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDepartment_InsertToPayroll",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToTKSFileSetUp(DepartmentInsertToTKSFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDepartment_InsertToTKS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Department dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DepartmentSetUp_Update",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateFileSetup(Department dep)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDepartment_Update",
                    dep, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToHRISFileSetUp(DepartmentUpdateToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDepartment_UpdateToHRIS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToPayrollFileSetUp(DepartmentUpdateToPayrollFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDepartment_UpdateToPayroll",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToTKSFileSetUp(DepartmentUpdateToTKSFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsDepartment_UpdateToTKS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }
    }
}