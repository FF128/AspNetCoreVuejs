﻿using Dapper;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebAPI.Dtos.MajorDto;
using WebAPI.Dtos;

namespace WebAPI.Repositories
{
    public class MajorRepository : IMajorRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public MajorRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_MajorSetUp_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFileSetUp(string code)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsMajor_DeleteByCode",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteFromHRISFileSetUp(DeleteSetUpDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsMajor_DeleteFromHRIS",
                    dto,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Major>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Major>("sp_MajorSetUp_View");
            }
        }

        public async Task<Major> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Major>("sp_MajorSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                   await conn.QueryFirstOrDefaultAsync("sp_tbl_fsMajor_ViewByCodeFromHRIS",
                       new { Code = code, DBName = hrisDB },
                       commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<Major> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Major>("sp_MajorSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Major major)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_MajorSetUp_Insert",
                    major, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertFileSetup(Major major)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsMajor_Insert",
                    major, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertToHRISFileSetUp(MajorInsertToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                var result = await conn.ExecuteAsync("sp_tbl_fsMajor_InsertToHRIS",
                     dto, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Major major)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_MajorSetUp_Update",
                    major, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateFileSetup(Major major)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsMajor_Update",
                    major, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateToHRISFileSetUp(MajorUpdateToHRISFSDto dto)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_tbl_fsMajor_UpdateToHRIS",
                     dto, commandType: CommandType.StoredProcedure);
            }
        }
    }
}