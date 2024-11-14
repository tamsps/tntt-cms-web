using Microsoft.AspNetCore.Mvc;
using ThieuNhiTT.Web.Services;

namespace ThieuNhiTT.Web.Controllers
{
	public class BooksController : Controller
	{
		private readonly BookService _bookService;
		private readonly LessonService _lessonService;

		public BooksController(BookService bookService, LessonService lessonService)
		{
			_bookService = bookService;
			_lessonService = lessonService;
		}
		public IActionResult Index()
		{
			var books = _bookService.GetAllBooks();
			return View(books);
		}
		public IActionResult Detail(string bookId)
		{
			var lessons = _lessonService.GetAllLessonsByBookId(bookId);
			
			return View(lessons);
		}
	}
}
