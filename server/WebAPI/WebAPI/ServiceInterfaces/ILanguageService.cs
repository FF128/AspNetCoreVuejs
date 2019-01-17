using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface ILanguageService
    {
        Task<CustomMessage> Insert(Language lang);
        Task<CustomMessage> Update(Language lang);
        Task<CustomMessage> Delete(int id);
    }
}
