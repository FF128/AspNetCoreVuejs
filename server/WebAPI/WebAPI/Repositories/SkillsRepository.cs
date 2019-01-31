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
using WebAPI.Dtos.SkillsDto;
using WebAPI.Dtos;

namespace WebAPI.Repositories
{
    public class SkillsRepository : ISkillsRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public SkillsRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SkillsSetUp_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFileSetUp(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsSkills_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromHRISFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsSkills_DeleteFromHRIS",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Skills>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Skills>("sp_SkillsSetUp_View");
            }
        }

        public async Task<Skills> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Skills>("sp_SkillsSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsSkills_ViewByCodeFromHRIS",
                       new { Code = code, DBName = hrisDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Skills> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Skills>("sp_SkillsSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Skills sk)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SkillsSetUp_Insert",
                    sk, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertFileSetup(Skills sk)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsSkills_Insert",
                    sk, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToHRISFileSetUp(SkillInsertToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                var result = await conn.ExecuteAsync("sp_tbl_fsSkills_InsertToHRIS",
                     dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Skills sk)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_SkillsSetUp_Update",
                    sk, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateFileSetup(Skills sk)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsSkills_Update",
                    sk, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToHRISFileSetUp(SkillUpdateToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                var result = await conn.ExecuteAsync("sp_tbl_fsSkills_UpdateToHRIS",
                     dto, commandType: CommandType.StoredProcedure);
            }
        }
    }
}