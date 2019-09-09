using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace FileUploaderAngularExample.Controllers
{
    public class MediaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private static readonly FormOptions _defaultFormOptions = new FormOptions();

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            string targetFilePath = null;

            var boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(Request.ContentType), _defaultFormOptions.MultipartBoundaryLengthLimit);
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();

            targetFilePath = Path.GetTempFileName();
            using (FileStream targetStream = System.IO.File.Create(targetFilePath))
            {
                await section.Body.CopyToAsync(targetStream);
            }

            return Json(new
            {

            });
        }
    }
}