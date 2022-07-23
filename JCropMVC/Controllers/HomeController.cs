using JCropMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JCropMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Save([FromForm] string avatarCropped)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
            string base64 = avatarCropped;
            byte[] bytes = Convert.FromBase64String(base64.Split(',')[1]);
            //using (FileStream stream = new FileStream(path, FileMode.Create))
            //{
            //    stream.Write(bytes, 0, bytes.Length);
            //    stream.Flush();
            //}

            System.IO.File.WriteAllBytes(Path.Combine(path, $"{Guid.NewGuid()}.png"), bytes);

            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}