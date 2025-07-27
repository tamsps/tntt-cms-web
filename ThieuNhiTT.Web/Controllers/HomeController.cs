using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using ThieuNhiTT.Web.Fody;
using ThieuNhiTT.Web.Models;
using ThieuNhiTT.Web.Services;

namespace ThieuNhiTT.Web.Controllers
{
	[TypeFilter(typeof(CustomExceptionFilter))]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
				private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _webHostEnvironment;
				private readonly IBookService _bookService;
				private readonly ILessonService _lessonService;
				private readonly INewsService _newService;
				private readonly IEmailSender _emailService;
				//private readonly IEmailService _emailService;
		private readonly EmailSenderOptions _emailOptions;
				private readonly EmailReceiver _emailReceiver;



		public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IWebHostEnvironment webHost, IBookService bookService, ILessonService lessonService, INewsService newsService, IEmailSender emailService, IOptions<EmailReceiver> emailOptions)
        {
            _logger = logger;
            _configuration = configuration;
            _webHostEnvironment = webHost;
						_bookService = bookService;
						_lessonService = lessonService;
					  _newService = newsService;
						_emailService = emailService;
						_emailReceiver = emailOptions.Value;
		}

    public async Task<IActionResult> Index()
    {
			_logger.LogInformation("Home page requested");
			
			string relativePath = _configuration["ThangTienJsonFilePath"];
			var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, relativePath);
			
			_logger.LogDebug("Loading books from {FilePath}", filePath);
			var books = _bookService.GetAllBooks(filePath);
			
			foreach (var book in books)
			{
				var lessons = _lessonService.GetAllLessonsByBookId(book.BookId.ToString(), string.Empty).ToList();
				book.Lessons = lessons;
			}
			
			relativePath = _configuration["NewsFilePath"];
			var newsFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, relativePath);
			
			_logger.LogDebug("Loading news from {NewsFilePath}", newsFilePath);
			var newss = _newService.GetNewsHome(newsFilePath);
			
			if(newss != null)
			{
				var newsMain = newss.FirstOrDefault(d=>d.IsShowMainHome);
				var newsOther = newss.Where(d=>d.IsShowSmallHome).Take(4).ToList();
				ViewData["NewsMain"] = newsMain;
				ViewData["NewsOther"] = newsOther;
				
				_logger.LogInformation("Loaded {NewsCount} news items for home page", newss.Count());
			}

			_logger.LogInformation("Home page loaded successfully with {BookCount} books", books.Count());
			return View(books);
		}

    public IActionResult AboutUs()
    {
        return View();
    }

		public IActionResult NewsDetail(string newsId)
		{
			_logger.LogInformation("News detail requested for {NewsId}", newsId);
			
			string relativePath = _configuration["NewsFilePath"];
			var filePath = Path.Combine(_webHostEnvironment.ContentRootPath, relativePath);
			
			_logger.LogDebug("Loading news from {FilePath}", filePath);
			var newsList = _newService.GetAllNews(filePath);

			var news = _newService.GetNewsById(newsId, filePath);
			ViewData["NewsList"] = newsList;

			if (news != null)
			{
				_logger.LogInformation("News detail loaded successfully for {NewsId}: {NewsTitle}", newsId, news.NewsTitle);
			}
			else
			{
				_logger.LogWarning("News not found for {NewsId}", newsId);
			}

			return View(news);
		}

		public IActionResult ContactUs()
		{
			return View();
		}
		[HttpPost]
		public IActionResult SendMessage(IFormCollection form)
		{
			var name = form["name"];
			var email = form["email"];
			var message = form["msg"];
			var toEmail = _emailReceiver.Email;
			var contactMsg  = $"Name: {name} \nEmail: {email} \nMessage: {message}";

			_logger.LogInformation("Contact message received from {Name} ({Email})", name, email);
			
			try
			{
				_emailService.SendEmailAsync(toEmail, "Phụ Huynh Liên Hệ", contactMsg);
				_logger.LogInformation("Contact message sent successfully to {ToEmail}", toEmail);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Failed to send contact message from {Name} ({Email})", name, email);
			}

			return RedirectToAction("ContactUs");
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