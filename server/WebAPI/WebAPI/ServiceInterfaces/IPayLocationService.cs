using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;

namespace WebAPI.ServiceInterfaces
{
    public interface IPayLocationService
    {
        Task<IEnumerable<PayLocationDto>> GetPayLocations();
        Task<PayLocationDto> GetPayLocationById(long locId);
    }
}
