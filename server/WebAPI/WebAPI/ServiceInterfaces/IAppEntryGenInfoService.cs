using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models.ApplicantsEntrySetUp;

namespace WebAPI.ServiceInterfaces
{
    public interface IAppEntryGenInfoService
    {
        Task<CustomMessage> Insert(AppEntryGenInfo gen);
        Task<CustomMessage> Update(AppEntryGenInfo gen);
        Task<CustomMessage> Delete(int id);
    }
}
