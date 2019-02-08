using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace WebAPI.Controllers
{
    [Route("file")]
    [ApiController]
    public class ManageFileController : ControllerBase
    {
        [HttpGet("{transNo}/{fileName}/{folderName}")]
        public IActionResult ViewAttachment(string transNo, string fileName, string folderName)
        {
            var file = Path.Combine(Directory.GetCurrentDirectory(),
                            "Files", folderName, fileName);
            //IFileProvider fileProvider = new ;
            //var fileInfo = fileProvider.GetFileInfo(file);
            FileInfo f = new FileInfo(file);
            string contentType;
            var contentTypeResult = new FileExtensionContentTypeProvider().TryGetContentType(f.Extension, out contentType);
            return PhysicalFile(file, contentType);
        }
    }
}