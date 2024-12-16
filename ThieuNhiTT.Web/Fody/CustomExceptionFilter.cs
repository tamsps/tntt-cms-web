using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ThieuNhiTT.Web.Fody
{
	public class CustomExceptionFilter : IExceptionFilter
	{
		public void OnException(ExceptionContext context)
		{
			// Log the exception if needed
			var exception = context.Exception;
			Console.WriteLine($"Exception caught: {exception.Message}");

			// Redirect to a specific action
			context.Result = new RedirectToActionResult("Error", "Home", new { message = "An unexpected error occurred." });

			// Mark the exception as handled
			context.ExceptionHandled = true;
		}
	}
}
