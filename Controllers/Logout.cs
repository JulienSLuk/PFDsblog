using Microsoft.AspNetCore.Mvc;

namespace WEB2022_ZZFashion.Controllers
{
	public class Logout : Controller
	{
		public IActionResult Index()
		{
			// Clear all key-values pairs stored in session state
			HttpContext.Session.Clear();
			// Call the Index action of Home controller
			return RedirectToAction("Index", "Home");
		}
	}
}
