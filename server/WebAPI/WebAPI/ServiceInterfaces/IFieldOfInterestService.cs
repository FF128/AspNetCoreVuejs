using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IFieldOfInterestService
    {
        Task<CustomMessage> Insert(FieldOfInterest foi);
        Task<CustomMessage> Update(FieldOfInterest foi);
        Task<CustomMessage> Delete(int id);
    }
}
