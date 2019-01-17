using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface ICourseDegreeService
    {
        Task<CustomMessage> Insert(CourseDegree cd);
        Task<CustomMessage> Update(CourseDegree cd);
        Task<CustomMessage> Delete(int id);
    }
}
