using Microsoft.AspNetCore.Mvc;
using ThieuNhiTT.Web.Services;

namespace ThieuNhiTT.Web.Controllers
{
		public class BooksController : Controller
		{
				private readonly BookService _bookService;
				public BooksController(BookService bookService)
				{
						_bookService = bookService;
				}

				public IActionResult Index()
				{
						var books = _bookService.GetAllBooks();
						return View(books);
				}
		}
}
