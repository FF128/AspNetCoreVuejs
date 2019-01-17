using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IGovExamsService 
    {
        Task<CustomMessage> Insert(GovExams ge);
        Task<CustomMessage> Update(GovExams ge);
        Task<CustomMessage> Delete(int id);
    }
}
