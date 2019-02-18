using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class PaginationService<T> : IPaginationService<T> where T : class
    {
        private readonly IPaginationRepository<T> paginationRepo;
        private readonly ICompanyInformationRepository compInfoRepo;
        
        public PaginationService(IPaginationRepository<T> paginationRepo,
            ICompanyInformationRepository compInfoRepo)
        {
            this.paginationRepo = paginationRepo;
            this.compInfoRepo = compInfoRepo;
        }
        public async Task<IEnumerable<T>> PaginationByCompanyCode(string command, int pageNumber, int pageSize, string query)
        {
            return await paginationRepo.Pagination(command, pageNumber, pageSize, query, compInfoRepo.GetCompanyCode());
        }
    }
}
