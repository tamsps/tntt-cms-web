using Microsoft.AspNetCore.Mvc;
using ThieuNhiTT.Web.Fody;
using ThieuNhiTT.Web.Models;
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
		[Log]
		public IActionResult Index(string lessonId, string filePath)
		{
			var lesson = _lessonService.GetLessonById(lessonId, filePath);
			if (lesson == null)
			{
				return RedirectToAction("Error", "Home");
			}
			return View(lesson);
		}
	} 
}
