using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.PRModel;

namespace WebAPI.RepositoryInterfaces
{
    public interface IPRRepository
    {
        Task<int> Insert(PRFHeaderMaint pr);
        Task InsertDetails(IEnumerable<PRFDetailsMaint> details);
        Task UpdatePRFNo(int identity, string prfNo);
    }
}
