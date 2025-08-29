using MethodDecorator.Fody.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ThieuNhiTT.Web.Fody
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor)]
	public class LogAttribute : Attribute, IMethodDecorator
	{
		public void Init(object instance, MethodBase method, object[] args)
		{
			Console.WriteLine($"Calling: {method.Name}");
		}

		public void OnEntry()
		{
			Console.WriteLine("OnEntry");
		}

		public void OnExit()
		{
			Console.WriteLine("OnExit");
		}

		public void OnException(Exception exception)
		{
			Console.WriteLine($"Exception: {exception.Message}");
			RedirectToActionResult redirect = new RedirectToActionResult("Error", "Home", null);
		}
	}

}
