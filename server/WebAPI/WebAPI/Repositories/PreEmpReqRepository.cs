using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Dtos;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class PreEmpReqRepository : IPreEmpReqRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public PreEmpReqRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PreEmpReqSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PreEmpReqEmpStatusDesignationDto>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<PreEmpReqEmpStatusDesignationDto>("sp_PreEmpReqSetUp_View");
            }
        }

        public async Task<PreEmpReq> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<PreEmpReq>("sp_PreEmpReqSetUp_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<PreEmpReq> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<PreEmpReq>("sp_PreEmpReqSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(PreEmpReq rate)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PreEmpReqSetUp_Insert",
                    rate, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(PreEmpReq rate)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PreEmpReqSetUp_Update",
                    rate, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
