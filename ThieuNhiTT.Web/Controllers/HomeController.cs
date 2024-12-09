using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using ThieuNhiTT.Web.Models;
using ThieuNhiTT.Web.Services;

namespace ThieuNhiTT.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
				private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
				private readonly IBookService _bookService;
				private readonly ILessonService _lessonService;




		public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment webHost, IBookService bookService, ILessonService lessonService)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHost;
						_bookService = bookService;
						_lessonService = lessonService;
		}

    public async Task<IActionResult> Index()
    {
			string relativePath = _configuration["ThangTienJsonFilePath"];
			var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, relativePath);
			var books = _bookService.GetAllBooks(filePath);
			foreach (var book in books)
			{
				var lessons = _lessonService.GetAllLessonsByBookId(book.BookId.ToString(), string.Empty).ToList();
				book.Lessons = lessons;
			}
			return View(books);
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