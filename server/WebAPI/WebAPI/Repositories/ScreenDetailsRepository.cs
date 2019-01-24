using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using Dapper;
using System.Data;
using WebAPI.Dtos;

namespace WebAPI.Repositories
{
    public class ScreenDetailsRepository : IScreenDetailsRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public ScreenDetailsRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task Delete(int id)
        {
            using(var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ScreenDetailsSetup_Delete", 
                    new { id }, 
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteScreenDetailsSub(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ScreenDetailsSubSetup_Delete",
                    new { id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DeleteScreenDetailsSubById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ScreenDetailsSubSetup_DeleteById",
                    new { id },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ScreenDetails>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
              return 
                    await conn.QueryAsync<ScreenDetails>("sp_ScreenDetailsSetup_View",
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ScreenDetails> GetByCode(string code)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                      await conn.QueryFirstOrDefaultAsync<ScreenDetails>("sp_ScreenDetailsSetup_ViewByCode",
                          new { code },
                          commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<ScreenDetails> GetById(int id)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                      await conn.QueryFirstOrDefaultAsync<ScreenDetails>("sp_ScreenDetailsSetup_ViewById",
                          new { id },
                          commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<GetScreenDetailSubDto>> GetSreenDetailsSubByDetailId(int screenDetailsId)
        {
            //using (var conn = connectionFactory.Connection)
            //{
            //    return
            //          await conn.QueryAsync<ScreenDetailsSub>("sp_ScreenDetailsSubSetup_ViewByScreenDetailId",
            //              new { Id = screenDetailsId },
            //              commandType: CommandType.StoredProcedure);
            //}
            using (var conn = connectionFactory.Connection)
            {
                return
                      await conn.QueryAsync<GetScreenDetailSubDto>("sp_ScreenDetailsSubSetup_ViewByScreenDetailId",
                          new { Id = screenDetailsId },
                          commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Insert(ScreenDetails sd)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ScreenDetailsSetup_Insert",
                    sd, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task InsertScreenDetailsSub(ScreenDetailsSub screenDetailsSub)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ScreenDetailsSubSetup_Insert",
                    screenDetailsSub, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task Update(ScreenDetails sd)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ScreenDetailsSetup_Update",
                    sd, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateScreenDetailsSub(ScreenDetailsSub screenDetailsSub)
        {
            using (var conn = connectionFactory.Connection)
            {
                await conn.ExecuteAsync("sp_ScreenDetailsSubSetup_Update",
                    screenDetailsSub, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
