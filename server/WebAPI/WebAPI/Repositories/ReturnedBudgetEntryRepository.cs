using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models.BudgetEntryModel;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class ReturnedBudgetEntryRepository : IReturnedBudgetEntryRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public ReturnedBudgetEntryRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<BudgetEntryMaintReturnComment>> GetBudgetEntryMaintReturnCommentsByTransNo(string transNo)
        {
            using (var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<BudgetEntryMaintReturnComment>("sp_BudgetEntryMaintReturnComment_ViewByTransNo",
                        new { TransNo = transNo },
                        commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<IEnumerable<ViewBudgetEntryWithStatusReturnedDto>> GetBudgetEntryWithStatusReturned()
        {
            using(var conn = connectionFactory.Connection)
            {
                return
                    await conn.QueryAsync<ViewBudgetEntryWithStatusReturnedDto>("sp_BudgetEntryMainHeader_ViewWithStatusReturned",
                        commandType: CommandType.StoredProcedure);
            }
        }
    }
}
