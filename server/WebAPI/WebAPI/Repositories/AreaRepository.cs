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
using WebAPI.Dtos;
using WebAPI.Dtos.AreaDto;

namespace WebAPI.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public AreaRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AreaSetUp_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFileSetUp(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsArea_DeleteByCode",
                    new { Code = code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromHRISFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsArea_DeleteFromHRIS",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromPayrollFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsArea_DeleteFromPayroll",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromTKSFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsArea_DeleteFromTKS",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Area>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Area>("sp_AreaSetUp_View");
            }
        }

        public async Task<Area> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Area>("sp_AreaSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB)
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsArea_ViewByCodeFromHRIS",
                       new { Code = code, DBName = hrisDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsArea_ViewByCodeFromPayroll",
                       new { Code = code, DBName = payrollDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromTKS(string code, string tskDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsArea_ViewByCodeFromTKS",
                       new { Code = code, DBName = tskDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Area> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Area>("sp_AreaSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Area area)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AreaSetUp_Insert",
                    area, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertFileSetup(Area area)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsArea_Insert",
                    area, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToHRISFileSetUp(AreaInsertToFileSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
               var result = await conn.ExecuteAsync("sp_tbl_fsArea_InsertToHRIS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToPayrollFileSetUp(AreaInsertToFileSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                var result = await conn.ExecuteAsync("sp_tbl_fsArea_InsertToPayroll",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToTSKFileSetUp(AreaInsertToFileSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
               var result = await conn.ExecuteAsync("sp_tbl_fsArea_InsertToTKS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Area area)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_AreaSetUp_Update",
                    area, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateFileSetup(Area area)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsArea_Update",
                    area, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToHRISFileSetUp(AreaUpdateToFileSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsArea_UpdateToHRIS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToPayrollFileSetUp(AreaUpdateToFileSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsArea_UpdateToPayroll",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToTKSFileSetUp(AreaUpdateToFileSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsArea_UpdateToTKS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }
    }
}