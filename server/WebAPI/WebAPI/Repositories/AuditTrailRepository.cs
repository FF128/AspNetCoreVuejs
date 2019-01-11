using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;

namespace WebAPI.Repositories
{
    public class AuditTrailRepository : IAuditTrailRepository
    {
        private readonly IConnectionFactory connectionFactory;
        public AuditTrailRepository(IConnectionFactory connectionFactory)
        {
            this.connectionFactory = connectionFactory;
        }
        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AuditTrail>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AuditTrail> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Insert(AuditTrail auditTrail)
        {
            throw new NotImplementedException();
        }

        public Task Update(AuditTrail auditTrail)
        {
            throw new NotImplementedException();
        }
    }
}
