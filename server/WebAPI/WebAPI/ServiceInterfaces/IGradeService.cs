using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IGradeService
    {
        Task<CustomMessage> Insert(Grade grade);
        Task<CustomMessage> Update(Grade grade);
        Task<CustomMessage> Delete(int id);
    }
}
