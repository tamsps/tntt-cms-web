using Microsoft.AspNetCore.Mvc;
using ThieuNhiTT.Web.Services;

namespace ThieuNhiTT.Web.Controllers
{
	public class LessonController : Controller
	{

		private readonly IBookService _bookService;
		private readonly ILessonService _lessonService;

		public LessonController(IBookService bookService, ILessonService lessonService)
		{
			_bookService = bookService;
			_lessonService = lessonService;
		}
		public IActionResult Index(string lessonId, string lessonFilePath)
		{
			var lesson = _lessonService.GetLessonById(lessonId, lessonFilePath);

			return View(lesson);
		}
	} 
}
