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
    [Route("api/Videos")]
    public class VideosController : Controller
    {

        private static string UPLOADS_PATH;

        private readonly SituatorContext _context;

        private IHostingEnvironment _environment;

        public VideosController(IHostingEnvironment environment, SituatorContext context)
        {
            _environment = environment;
            _context = context;
            UPLOADS_PATH = Path.Combine(_environment.WebRootPath, "uploads");
        }

        [RequestSizeLimit(valueCountLimit: 2147483647)]
        [HttpPost]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files, int Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string filepath = "";
            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    filepath = Path.Combine(UPLOADS_PATH, "video-" + Id);
                    using (var fileStream = new FileStream(filepath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }

                _context.Nodes.Where(i => i.Id == Id).First().VideoUrl = "video-" + Id;
            }

            _context.SaveChanges();

            return new NoContentResult();
        }


        [HttpGet("{id}")]
        public IActionResult GetVideo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var videoPath = Path.Combine(UPLOADS_PATH, _context.Nodes.Where(i => i.Id == id).First().VideoUrl);

            return Ok(videoPath);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVideo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var videoPath = Path.Combine(UPLOADS_PATH, _context.Nodes.Where(i => i.Id == id).First().VideoUrl);
            if (System.IO.File.Exists(videoPath))
                System.IO.File.Delete(videoPath);

            var node = _context.Nodes.Where(i => i.Id == id).First();

            node.VideoUrl = "/";
            _context.SaveChanges();

            return Ok(node);
        }

    }

}
