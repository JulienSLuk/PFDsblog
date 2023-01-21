using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB2022Apr_P01_T3.DAL;

namespace WEB2022Apr_P01_T3.Controllers
{
    public class AdminController : Controller
    {
        private AdminDAL aDAL = new AdminDAL();
        public IActionResult Index()
        {
            return View();
        }

       

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult AdminView()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Register(IFormCollection formData)
        {
            string username = formData["Username"].ToString();
            string password = formData["Password"].ToString();
            string confirmPassword = formData["ConfirmPassword"].ToString();

            if (!aDAL.checkUsernamePresent(username))
            {
                if (ModelState.IsValid)
                {
                    aDAL.createAccount(username, password);
                    return RedirectToAction("Login");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                TempData["UsernamePresent"] = "Username is taken!";
            }
            
            return View();
        }


        AdminDAL a = new AdminDAL();
        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        public ActionResult Login(IFormCollection formData)
        {
            // Read inputs from textboxes
            // Email address converted to lowercase
            string username = formData["Username"].ToString();
            string password = formData["Password"].ToString();
            if (a.CredentialsMatch(username, password))
            {
                HttpContext.Session.SetString("username", password);
                //return RedirectToAction("AdminView", "Username");  
                return RedirectToAction("AdminView", "Admin");

            }
            // Store an error message in TempData for display at the index view 
            TempData["Message"] = "Invalid Login Credentials!";

            // Redirect user back to the index view through an action
            return RedirectToAction("Login");
        }

        public ActionResult LogOut()
        {
            // Clear all key-values pairs stored in session state 
            HttpContext.Session.Clear();
            // Call the Index action of Home controller 
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
