using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using ThieuNhiTT.Web.Models;
using ThieuNhiTT.Web.Services;

namespace ThieuNhiTT.Web.Controllers
{
	public class BooksController : Controller
	{
		private readonly IBookService _bookService;
    private readonly ILessonService _lessonService;
    private readonly ILogger<BooksController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _webHostEnvironment;



    public BooksController(IBookService bookService, ILessonService lessonService,IConfiguration configuration, ILogger<BooksController> logger, IWebHostEnvironment webHost)
		{
			_bookService = bookService;
			_lessonService = lessonService;
			_configuration = configuration;
			_logger = logger;
			_webHostEnvironment = webHost;
		}
		public IActionResult Index(string filePath)
		{
			if(string.IsNullOrEmpty(filePath))
			{
        string relativePath = _configuration["ThangTienJsonFilePath"];
        filePath = Path.Combine(_webHostEnvironment.ContentRootPath, relativePath);

    //    foreach (var bk in Enum.GetValues(typeof(BookListEnum)))
				//{
				//	  filePath = $"Data/{bk}.json";
    //        var books = _bookService.GetAllBooks(filePath);
    //    }
      }
			var books = _bookService.GetAllBooks(filePath);
			return View(books);
		}
		public IActionResult Detail(string bookId, string lessonFilePath)
		{
			lessonFilePath = $"Data\\{bookId}.json";
			var lessons = _lessonService.GetAllLessonsByBookId(bookId,lessonFilePath).ToList(); ;
			
			return View(lessons);
		}
	}
}
