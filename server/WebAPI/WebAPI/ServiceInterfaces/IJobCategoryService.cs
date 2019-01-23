using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Helpers;

namespace WebAPI.ServiceInterfaces
{
    public interface IJobCategoryService
    {
        Task<CustomMessage> Insert(JobCategoryDto dto);
        Task<CustomMessage> Update(JobCategoryDto dto);
        Task<CustomMessage> Delete(string jobCatCode);
        Task<IEnumerable<GetJobCategoryDto>> GetAll();
    }
}
