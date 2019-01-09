using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class CompanyInfoService : ICompanyInfoService
    {
        public byte[] ConvertFileToByte(IFormFile file)
        {
            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    return fileBytes;
                    //string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                }
            }
            return null;
        }
    }
}
