using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Dtos;
using WebAPI.Dtos.CitizenshipDto;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class CitizenshipRepository : ICitizenshipRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public CitizenshipRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsCitizenship_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsCitizenship_DeleteByCode",
                    new { Code = code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Citizenship>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Citizenship>("sp_tbl_fsCitizenship_View");
            }
        }

        public async Task<Citizenship> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Citizenship>("sp_tbl_fsCitizenship_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Citizenship> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Citizenship>("sp_tbl_fsCitizenship_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertFileSetup(Citizenship cit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsCitizenship_Insert",
                    cit, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task Update(Citizenship cit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_CitizenshipSetUp_Update",
                    cit, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task InsertToPayrollFileSetUp(CitizenshipInsertToFileSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsCitizenship_InsertToPayroll",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task InsertToHRISFileSetUp(CitizenshipInsertToFileSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsCitizenship_InsertToHRIS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync("sp_tbl_fsCitizenship_ViewByCodeFromPayroll",
                        new { Code = code, DBName = payrollDB },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync("sp_tbl_fsCitizenship_ViewByCodeFromHRIS",
                        new { Code = code, DBName = hrisDB },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Citizenship cit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_CitizenshipSetUp_Insert",
                    cit, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToPayrollFileSetUp(CitizenshipUpdateToFileSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsCitizenship_UpdateToPayroll",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToHRISFileSetUp(CitizenshipUpdateToFileSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsCitizenship_UpdateToHRIS",
                    dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateFileSetup(Citizenship cit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsCitizenship_Update",
                    cit, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
