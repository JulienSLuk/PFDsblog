using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WEB2022_ZZFashion.DAL;
using WEB2022_ZZFashion.Models;

namespace WEB2022_ZZFashion.Controllers
{
	public class HomeController : Controller
	{
		// TODO : create member/customer (same thing) sql database
		private CustomerDAL memberContext = new CustomerDAL();
		private StaffDAL staffContext = new StaffDAL();
		private ProductDAL productContext = new ProductDAL();
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		public IActionResult AboutUs()
		{
			return View();
		}

		public IActionResult ContactUs()
		{
			return View();
		}

		public IActionResult Forum()
		{
			return View();
		}

		public IActionResult Donate()
		{
			return View();
		}

		public IActionResult Payment()
		{
			return View();
		}


		public ActionResult ViewProduct()
        {
			List<Product> productList = productContext.GetNewProducts();
			return View(productList);
        }

		public ActionResult ViewTechBlogs()
		{
			//get products from product list to display on page
			List<Product> productList = productContext.GetTechProducts();
			return View(productList);
		}


		public ActionResult ViewFinSchBlogs()
		{
			//get products from product list to display on page
			List<Product> productList = productContext.GetFinSchProducts();
			return View(productList);
		}

		public ActionResult ViewFoodDealBlogs()
		{
			//get products from product list to display on page
			List<Product> productList = productContext.GetFoodDealProducts();
			return View(productList);
		}

		public IActionResult Login()
		{
			
			string role = HttpContext.Session.GetString("Role");
			if (role == null)
			{
				return View();
			}
			else
			{
				switch (role)
				{
					case "Sales Personnel":
						return RedirectToAction("Index", "SalesPersonnel");
					case "Marketing Personnel":
						return RedirectToAction("Index", "MarketingManager");
					case "Product Manager":
						return RedirectToAction("Index", "ProductManager");
					case "Member":
						return RedirectToAction("Index", "Member");
					default:
						return View();
				}
			}
			
		}

		[HttpPost]
		public ActionResult UserLogin(IFormCollection formData)
		{
			// Read inputs from textboxes
			// LoginID converted to lowercase
			
			string loginID = formData["txtLoginid"].ToString().ToLower();
			string password = formData["txtPassword"].ToString();
			string usertype = formData["user"].ToString();

			if (usertype == "Staff") {

				if (staffContext.CheckPassword(loginID, password))
				{
					HttpContext.Session.SetString("LoginID", loginID);
					HttpContext.Session.SetString("Role", staffContext.getRole(loginID));
					switch (HttpContext.Session.GetString("Role"))
					{
						case "Sales Personnel":
							return RedirectToAction("Index", "SalesPersonnel");
						case "Marketing Personnel":
							return RedirectToAction("Index", "MarketingManager");
						case "Product Manager":
							return RedirectToAction("Index", "ProductManager");
						default:
							return RedirectToAction("Index", "Home");
					}
				}
				else
				{
					// Store an error message in TempData for display at the login page
					TempData["ErrorMessage"] = "Invalid Login Credentials!";
					return RedirectToAction("Login");
				}
			}
			else if (usertype == "Member")
			{
				// TODO : check username password from database to create session
				if (memberContext.CheckPassword(loginID,password)) {
					HttpContext.Session.SetString("LoginID", loginID);
					HttpContext.Session.SetString("Role", "Member");
					return RedirectToAction("Index", "Member");
				}
				else
				{
					// Store an error message in TempData for display at the login page
					TempData["ErrorMessage"] = "Invalid Login Credentials!";
					return RedirectToAction("Login");
				}
			}
			else
			{

				// Store an error message in TempData for display at the login page
				TempData["ErrorMessage"] = "Invalid Login Credentials!";
				return RedirectToAction("Login");
			}
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	} 
}
