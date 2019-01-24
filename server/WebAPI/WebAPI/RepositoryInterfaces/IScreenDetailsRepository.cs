using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IScreenDetailsRepository
    {
        Task<IEnumerable<ScreenDetails>> GetAll();
        Task<ScreenDetails> GetById(int id);
        Task<ScreenDetails> GetByCode(string code);
        Task Insert(ScreenDetails sd);
        Task Update(ScreenDetails sd);
        Task Delete(int id);
        // Screen Details Sub
        Task<IEnumerable<GetScreenDetailSubDto>> GetSreenDetailsSubByDetailId(int screenDetailsId);
        Task UpdateScreenDetailsSub(ScreenDetailsSub screenDetailsSub);
        Task InsertScreenDetailsSub(ScreenDetailsSub screenDetailsSub);
        Task DeleteScreenDetailsSub(int screenDetailId);
        Task DeleteScreenDetailsSubById(int id);
    }
}
