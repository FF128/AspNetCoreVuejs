using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IAuditTrailRepository
    {
        Task<IEnumerable<AuditTrail>> GetAll();
        Task<IEnumerable<dynamic>> Pagination(int pageNum, int pageSize, string query);
        Task<AuditTrail> GetById(int id);
        Task Insert(AuditTrail auditTrail);
       // Task Update(AuditTrail auditTrail);
        //Task Delete(int id);
    }
}
