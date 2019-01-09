﻿using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
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
                await conn.ExecuteAsync("sp_Citizenship_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<Citizenship>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<Citizenship>("sp_Citizenship_View");
            }
        }

        public async Task<Citizenship> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<Citizenship>("sp_Citizenship_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(Citizenship cit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Citizenship_Insert",
                    cit, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(Citizenship cit)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_Citizenship_Update",
                    cit, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
