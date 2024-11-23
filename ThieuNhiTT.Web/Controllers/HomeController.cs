using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using ThieuNhiTT.Web.Models;

namespace ThieuNhiTT.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
				private readonly IConfiguration _configuration;

        private readonly IWebHostEnvironment _webHostEnvironment;

		

		public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment webHost)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHost;
		}

        public async Task<IActionResult> Index()
        {
						string relativePath = _configuration["ThangTienJsonFilePath"];
						string jsonFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, relativePath);

						var jsonData = await System.IO.File.ReadAllTextAsync(jsonFilePath);

						var bookCollection = JsonConvert.DeserializeObject<BookCollection>(jsonData);

						return View(bookCollection.Books);
				}

        public IActionResult Privacy()
        {
            return View();
        }
		public IActionResult CatholicCalendar()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public async Task<IActionResult> ErrorAsync()
        {
						return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
				}

    }
}