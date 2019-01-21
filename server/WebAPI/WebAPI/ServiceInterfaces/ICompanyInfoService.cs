using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface ICompanyInfoService
    {
        Task<CustomMessage> InsertOrUpdate(CompanyInformation info);
        string CompanyCode { get; }
    }
}
