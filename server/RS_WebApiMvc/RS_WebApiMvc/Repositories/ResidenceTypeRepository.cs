﻿using Dapper;
using RS_WebApiMvc.Data;
using RS_WebApiMvc.Models;
using RS_WebApiMvc.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace RS_WebApiMvc.Repositories
{
    public class ResidenceTypeRepository : IResidenceTypeRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public ResidenceTypeRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ResidenceTypeSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ResidenceType>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<ResidenceType>("sp_ResidenceTypeSetUp_View");
            }
        }

        public async Task<ResidenceType> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<ResidenceType>("sp_ResidenceTypeSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(ResidenceType rt)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ResidenceTypeSetUp_Insert",
                    rt, commandType: CommandType.StoredProcedure);
            }
        }
        public async Task Update(ResidenceType rt)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ResidenceTypeSetUp_Update",
                    rt, commandType: CommandType.StoredProcedure);
            }
        }
    }
}