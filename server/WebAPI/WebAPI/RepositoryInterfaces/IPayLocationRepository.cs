using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;

namespace WebAPI.RepositoryInterfaces
{
    public interface IPayLocationRepository
    {
        Task<IEnumerable<PayLocationDto>> GetPayLocations(string dbName);
        Task<PayLocationDto> GetPayLocationById(long locId, string dbName);
    }
}
