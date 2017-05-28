using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Situator.Attributes;
using Situator.Models;
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

        private readonly SituatorContext _context;

        private IHostingEnvironment _environment;
        public UploadController(IHostingEnvironment environment, SituatorContext context)
        {
            _environment = environment;
            _context = context;
        }

        [RequestSizeLimit(valueCountLimit: 2147483647)]
        [HttpPost]
        public async Task<IActionResult> Video(ICollection<IFormFile> files, int Id)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            string filepath = "";
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    filepath = Path.Combine(uploads, file.FileName);
                    using (var fileStream = new FileStream(filepath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }

                _context.Nodes.Where(i => i.Id == Id).First().VideoUrl = "uploads/" + file.FileName;
            }

            _context.SaveChanges();

            return new NoContentResult();
        }

    }
}
