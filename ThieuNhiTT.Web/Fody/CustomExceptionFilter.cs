using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ThieuNhiTT.Web.Fody
{
	public class CustomExceptionFilter : IExceptionFilter
	{
		private readonly ILogger<CustomExceptionFilter> _logger;

		public CustomExceptionFilter(ILogger<CustomExceptionFilter> logger)
		{
			_logger = logger;
		}

		public void OnException(ExceptionContext context)
		{
			var exception = context.Exception;
			var request = context.HttpContext.Request;

			// Log the exception with structured data
			_logger.LogError(exception, 
				"Unhandled exception occurred in {Controller}.{Action} for {Method} {Path}",
				context.RouteData.Values["controller"],
				context.RouteData.Values["action"],
				request.Method,
				request.Path);

			// Log additional context information
			_logger.LogError("Exception Details: {ExceptionType} - {ExceptionMessage}", 
				exception.GetType().Name, 
				exception.Message);

			if (request.Headers.Count > 0)
			{
				_logger.LogDebug("Request Headers: {@Headers}", 
					request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()));
			}

			// Redirect to a specific action
			context.Result = new RedirectToActionResult("Error", "Home", new { message = "An unexpected error occurred." });

			// Mark the exception as handled
			context.ExceptionHandled = true;
		}
	}
}
