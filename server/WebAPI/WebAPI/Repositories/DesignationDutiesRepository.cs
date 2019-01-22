using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Dtos;
using WebAPI.RepositoryInterfaces;
using Dapper;
using AutoMapper;
using WebAPI.Models;
using System.Data;

namespace WebAPI.Repositories
{
    public class DesignationDutiesRepository : IDesignationDutiesRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public DesignationDutiesRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task DeleteByCode(string code)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DesignationDutiesReqSetUp_Delete",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteDesignationDutiesJobReqsByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DesignationDutiesReqJobReq_Delete",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteDesignationDutiesResponsibilitiesByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DesignationDutiesReqResponsibilities_Delete",
                    new { code },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<DesignationDuties>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                     await conn.QueryAsync<DesignationDuties>("sp_DesignationDutiesReqSetUp_View",
                         commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<DesignationDuties> GetByCode(string code)
        {
            using(var conn = connectionFactory.Connection)
            {
               return
                    await conn.QueryFirstOrDefaultAsync<DesignationDuties>("sp_DesignationDutiesReqSetUp_ViewByCode",
                        new { code },
                        commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<DesignationDutiesJobReq>> GetDesignationDutiesJobReqs(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                     await conn.QueryAsync<DesignationDutiesJobReq>("sp_DesignationDutiesReqJobReq_ViewByCode",
                         new { code },
                         commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<DesignationDutiesResponsibilities>> GetDesignationDutiesResponsibilities(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                     await conn.QueryAsync<DesignationDutiesResponsibilities>("sp_DesignationDutiesReqResponsibilities_ViewByCode ",
                         new { code },
                         commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task Insert(DesignationDuties data)
        {
            using(var conn = connectionFactory.Connection)
            {
               // var data = mapper.Map<DesignationDuties>(dto);

                await conn.ExecuteAsync("sp_DesignationDutiesReqSetUp_Insert", data,
                   commandType: System.Data.CommandType.StoredProcedure);

                //await conn.ExecuteAsync("sp_DesignationDutiesReqResponsibilities_Insert", dto.DesignationDutiesResponsibilities,
                //    commandType: System.Data.CommandType.StoredProcedure);

                //await conn.ExecuteAsync("sp_DesignationDutiesReqJobReq_Insert", dto.DesignationDutiesJobReqs,
                //   commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task InsertDutiesReq(IEnumerable<DesignationDutiesResponsibilities> data)
        {
            using (var conn = connectionFactory.Connection)
            {

                await conn.ExecuteAsync("sp_DesignationDutiesReqResponsibilities_Insert", data,
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task InsertJobReq(IEnumerable<DesignationDutiesJobReq> data)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_DesignationDutiesReqJobReq_Insert", data,
                   commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
