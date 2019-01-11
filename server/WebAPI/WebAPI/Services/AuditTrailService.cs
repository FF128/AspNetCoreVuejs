using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class AuditTrailService: IAuditTrailService
    {
        private readonly IAuditTrailRepository repo;
        public AuditTrailService(IAuditTrailRepository repo)
        {
            this.repo = repo;
        }


    }
}
