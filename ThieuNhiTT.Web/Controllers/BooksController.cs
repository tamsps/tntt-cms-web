using Microsoft.AspNetCore.Mvc;

namespace ThieuNhiTT.Web.Controllers
{
	public class BooksController : Controller
	{
		public IActionResult Index()
		{

			return View();
		}
	}
}
