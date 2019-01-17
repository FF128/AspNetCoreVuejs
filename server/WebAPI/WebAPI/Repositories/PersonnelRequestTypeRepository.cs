using Dapper;
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
    public class PersonnelRequestTypeRepository : IPersonnelRequestTypeRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public PersonnelRequestTypeRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PersonnelReqType_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<PersonnelRequestType>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<PersonnelRequestType>("sp_PersonnelReqType_View");
            }
        }

        public async Task<PersonnelRequestType> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<PersonnelRequestType>("sp_PersonnelReqType_ViewByCode",
                        new { Code = code },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<PersonnelRequestType> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<PersonnelRequestType>("sp_PersonnelReqType_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(PersonnelRequestType prt)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PersonnelReqType_Insert",
                    prt, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(PersonnelRequestType prt)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_PersonnelReqType_Update",
                    prt, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
