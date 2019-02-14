using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models.TransUserModel;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class TransUserRepository : ITransUserRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public TransUserRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<TransUser>> GetAll()
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<TransUser>("sp_TransUsers_GetAll", commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<TransUser>> GetAllByTrans(string companyCode, string trans)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<TransUser>("sp_TransUsers_GetAllByTrans", 
                        new { CompanyCode = companyCode, Trans = trans },commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<bool> GetByEmpCode(string empCode, string companyCode)
        {
            using(var conn = connectionFactory.Connection)
            {
                var result = await conn.QueryFirstOrDefaultAsync("sp_TransUsers_GetByEmpCode", new { EmpCode = empCode, CompanyCode = companyCode }, 
                    commandType: CommandType.StoredProcedure);

                return result != null ? true : false;
            }
        }

        public async Task<IEnumerable<TransUserDepartment>> GetTransUserDepartments(long transID, string companyCode)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<TransUserDepartment>("sp_TransUsersDepartment_GetByTransID",
                        new { TransID = transID, CompanyCode = companyCode }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<dynamic>> GetTransUserDepartmentsByDepCode(string depCode, string trans, string companyCode)
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync("sp_TransUsers_GetTransUserDepartmentByDepCode",
                        new { DepCode = depCode, Trans = trans, CompanyCode = companyCode }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<TransUserPaylocation>> GetTransUserPaylocations(long transID, string companyCode)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<TransUserPaylocation>("sp_TransUsersPaylocation_GetByTransID",
                        new { TransID = transID, CompanyCode = companyCode }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<dynamic>> GetTransUserPayLocByLocId(int locId, string trans, string companyCode)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync("sp_TransUsers_GetTransUserPayLocByLocId",
                        new { LocId = locId, Trans = trans, CompanyCode = companyCode }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> Insert(TransUser transUser)
        {
            using(var conn = connectionFactory.Connection)
            {
                var identity = await conn.QueryFirstOrDefaultAsync<int>("sp_TransUsers_Insert", transUser, commandType: CommandType.StoredProcedure);
                return identity;
            }
        }

        public async Task InserTransUserPayLoc(IEnumerable<TransUserPaylocation> transUserPaylocations)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_TransUsersPaylocation_Insert", transUserPaylocations, commandType: CommandType.StoredProcedure);
                
            }
        }

        public async Task InsertTransUserDepartment(IEnumerable<TransUserDepartment> transUserDepartments)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_TransUsersDepartment_Insert", transUserDepartments, commandType: CommandType.StoredProcedure);

            }
        }
    }
}
