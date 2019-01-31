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
using WebAPI.Dtos.Location;
using WebAPI.Dtos;

namespace WebAPI.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public LocationRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_LocationSetUp_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFileSetUp(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsLocation_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromHRISFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsLocation_DeleteFromHRIS",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromPayrollFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsLocation_DeleteFromPayroll",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromTKSFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsLocation_DeleteFromTKS",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Location>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Location>("sp_LocationSetUp_View");
            }
        }

        public async Task<Location> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Location>("sp_LocationSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsLocation_ViewByCodeFromHRIS",
                       new { Code = code, DBName = hrisDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsLocation_ViewByCodeFromPayroll",
                       new { Code = code, DBName = payrollDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromTKS(string code, string tskDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsLocation_ViewByCodeFromTKS",
                       new { Code = code, DBName = tskDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Location> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Location>("sp_LocationSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Location loc)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_LocationSetUp_Insert",
                    loc, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertFileSetup(Location loc)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsLocation_Insert",
                    loc, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToHRISFileSetUp(LocationInsertFileSetupDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsLocation_InsertToHRIS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToPayrollFileSetUp(LocationInsertFileSetupDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsLocation_InsertToPayroll",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToTKSFileSetUp(LocationInsertFileSetupDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsLocation_InsertToTKS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Location loc)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_LocationSetUp_Update",
                    loc, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateFileSetup(Location loc)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsLocation_Update",
                    loc, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToHRISFileSetUp(LocationUpdateFileSetupDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsLocation_UpdateToHRIS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToPayrollFileSetUp(LocationUpdateFileSetupDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsLocation_UpdateToPayroll",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToTKSFileSetUp(LocationUpdateFileSetupDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsLocation_UpdateToTKS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }
    }
}