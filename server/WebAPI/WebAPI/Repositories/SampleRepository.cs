using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class SampleRepository : ISampleRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public SampleRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public async Task<IEnumerable<SampleModel>> GetAll()
        {
            using (var conn = connectionFactory.Connection)
            {
                var sampleModel = await conn.QueryAsync<SampleModel>("Members_GetAll");
;
                return sampleModel;
            }
        }
    }
}
