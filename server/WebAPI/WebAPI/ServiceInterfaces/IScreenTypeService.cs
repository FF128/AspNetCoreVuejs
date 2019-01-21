using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IScreenTypeService
    {
        Task<CustomMessage> Insert(ScreenType st);
        Task<CustomMessage> Update(ScreenType st);
        Task<CustomMessage> Delete(int id);
    }
}
