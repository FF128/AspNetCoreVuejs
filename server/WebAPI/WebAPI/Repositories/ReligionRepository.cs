﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Dtos.Religion;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class ReligionRepository : IReligionRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public ReligionRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ReligionSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsReligion_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Religion>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Religion>("sp_ReligionSetUp_View");
            }
        }

        public async Task<Religion> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Religion>("sp_ReligionSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsReligion_ViewByCodeFromHRIS",
                       new { Code = code, DBName = hrisDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Religion> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Religion>("sp_ReligionSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Religion rel)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ReligionSetUp_Insert",
                    rel, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertFileSetup(Religion rel)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsReligion_Insert",
                    rel, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToHRISFileSetUp(ReligionInsertToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                var result = await conn.ExecuteAsync("sp_tbl_fsReligion_InsertToHRIS",
                     dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Religion rel)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ReligionSetUp_Update",
                    rel, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
