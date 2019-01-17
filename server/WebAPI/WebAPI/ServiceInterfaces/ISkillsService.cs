using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface ISkillsService
    {
        Task<CustomMessage> Insert(Skills skill);
        Task<CustomMessage> Update(Skills skill);
        Task<CustomMessage> Delete(int id);
    }
}
