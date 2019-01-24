using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IEvalEmailFormatRepository
    {
        Task<EvalEmailFormat> GetByTransType(string transType);
        Task Insert(EvalEmailFormat emailFormat);
        Task Update(EvalEmailFormat emailFormat);
        Task<EvalEmailFormat> GetById(int id);
    }
}
