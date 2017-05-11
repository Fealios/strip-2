using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using YouStrip.Models;
using YouStrip.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Text;
// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace YouStrip.Controllers
{

    [Route("api/[controller]")]
    public class YouStripController : Controller
    {
        private IHostingEnvironment _environment;
        public YouStripController(IHostingEnvironment environment)
        {
            _environment = environment;
        }
        public IActionResult GetMessages()
        {
            var allMessages = Message.GetMessages();
            return View(allMessages);
        }

        [HttpGet("[action]/{name}")]
        public IActionResult StripRequest(string name)
        {
            Request newRequest = new Request(name);
            var downloadLink = newRequest.SendRequest();
            return Json(downloadLink);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(ICollection<IFormFile> files)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "Songs");
            var request = this.HttpContext.Request;
            var newSong = request.Form.Files[0];
            using (var fileStream = new FileStream(Path.Combine(uploads, newSong.FileName), FileMode.Create))
            {
                await newSong.CopyToAsync(fileStream);
            }

            return RedirectToAction("Index");
        }

        [HttpGet("[action]")]
        public IActionResult GetFileNames()
        {
            DirectoryInfo dir = new DirectoryInfo("wwwroot/Songs");
            FileInfo[] files = dir.GetFiles();
            List<string> names = new List<string>();
            foreach(FileInfo f in files)
            {
                names.Add(f.Name);
            }
            return Json(names);
        }
    }
}
