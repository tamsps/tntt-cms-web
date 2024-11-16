using Microsoft.AspNetCore.Mvc;
using ThieuNhiTT.Web.Services;

namespace ThieuNhiTT.Web.Controllers
{
	public class LessonController : Controller
	{

		private readonly BookService _bookService;
		private readonly LessonService _lessonService;

		public LessonController(BookService bookService, LessonService lessonService)
		{
			_bookService = bookService;
			_lessonService = lessonService;
		}
		public IActionResult Index(string bookId, string lessonId)
		{
			var lesson = _lessonService.GetLessonById(bookId, lessonId);

			return View(lesson);
		}
	} 
}
