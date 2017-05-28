using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Situator.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Situator.Controllers
{
    [Route("api/Upload")]
    public class UploadController : Controller
    {
        private IHostingEnvironment _environment;
        public UploadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [RequestSizeLimit(valueCountLimit: 2147483647)]
        [HttpPost]
        public async Task<IActionResult> Video(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.ContentRootPath, "uploads");
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }
            return new NoContentResult();
        }

    }
}
