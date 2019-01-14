using Dapper;
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
    public class LicenseRepository : ILicenseRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public LicenseRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_LicenseSetUp_Delete",
                    new { Id = id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<License>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<License>("sp_LicenseSetUp_View");
            }
        }

        public async Task<License> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryFirstOrDefaultAsync<License>("sp_LicenseSetUp_ViewById",
                        new { Id = id },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(License lic)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_LicenseSetUp_Insert",
                    lic, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(License lic)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_LicenseSetUp_Update",
                    lic, commandType: CommandType.StoredProcedure);
            }
        }
    }
}