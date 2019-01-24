using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IEvalEmailFormatService
    {
        Task<CustomMessage> Insert(EvalEmailFormat evalEmailFormat);
        Task<CustomMessage> Update(EvalEmailFormat evalEmailFormat);
    }
}
