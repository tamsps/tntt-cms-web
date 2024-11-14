using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using ThieuNhiTT.Web.Services;

namespace ThieuNhiTT.Web.Controllers
{
	public class BooksController : Controller
	{
		private readonly BookService _bookService;
		private readonly LessonService _lessonService;

		public BooksController(BookService bookService)
		{
			_bookService = bookService;
		}
		public IActionResult Index()
		{
			var books = _bookService.GetAllBooks();
			return View(books);
		}
		public IActionResult Detail(string bookdId)
		{
			var lessons = _lessonService.GetAllLessonsByBookId(bookdId);
			var bookDetail = _bookService.GetBookById(bookdId);
			return View(bookDetail);
		}
	}
}
