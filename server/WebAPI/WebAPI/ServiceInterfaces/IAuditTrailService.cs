using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IAuditTrailService<T> where T : class
    {
        string GetHost { get; }
        string GetIpAddress { get; }
        Task Save(T oldObj, T newObj, string trans);
      //  Task Edit(T oldObj, T newObj);
    }
}
