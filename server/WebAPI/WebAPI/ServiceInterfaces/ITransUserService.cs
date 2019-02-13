using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Helpers;

namespace WebAPI.ServiceInterfaces
{
    public interface ITransUserService
    {
        Task<CustomMessage> Insert(InsertTransUserDto dto);
    }
}
