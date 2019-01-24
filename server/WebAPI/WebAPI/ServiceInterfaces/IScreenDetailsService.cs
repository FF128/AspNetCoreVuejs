using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IScreenDetailsService
    {
        Task<CustomMessage> Insert(ScreenDetails sd);
        Task<CustomMessage> Update(ScreenDetails sd);
        Task<CustomMessage> Delete(int id);

        Task<CustomMessage> InsertScreenDetailSub(AddScreenDetailSubDto dto);
    }
}
