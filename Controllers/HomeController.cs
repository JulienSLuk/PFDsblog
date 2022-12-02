using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WEB2022Apr_P01_T3.Models;
using WEB2022Apr_P01_T3.DAL;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace WEB2022Apr_P01_T3.Controllers
{
    public class HomeController : Controller
    {
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

        public IActionResult CreateFeedback()
        {
            return View();
        }



        private FeedbackDAL feedbackContext = new FeedbackDAL();
        public IActionResult ViewFeedback()
        {

            List<FeedbackViewModel> feedbackVMList = new List<FeedbackViewModel>();
            feedbackVMList = feedbackContext.GetAllFeedbackAndResponses();

            HttpContext.Session.SetString("NoOfFeedback", feedbackVMList.Count().ToString());
            return View(feedbackVMList);


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateFeedback(FeedbackViewModel feedbackVM)
        {

            int newFeedbackId = feedbackContext.GetAllFeedbackAndResponses().Count() + 1;

            if (feedbackVM.fileToUpload != null &&
                feedbackVM.fileToUpload.Length > 0)
            {
                try
                {
                    string fileExt = Path.GetExtension(
                    feedbackVM.fileToUpload.FileName);

                    string uploadedFile = "image-" + newFeedbackId.ToString() + fileExt;

                    string savePath = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot\\images\\feedback", uploadedFile);

                    using (var fileSteam = new FileStream(
                    savePath, FileMode.Create))
                    {
                        await feedbackVM.fileToUpload.CopyToAsync(fileSteam);
                    }
                    feedbackVM.ImageFileName = uploadedFile;
                    ViewData["Message"] = "File uploaded successfully.";
                }
                catch (IOException)
                {
                    //File IO error, could be due to access rights denied
                    ViewData["Message"] = "File uploading fail!";
                }
                catch (Exception ex) //Other type of error
                {
                    ViewData["Message"] = ex.Message;
                }
            }
            Console.WriteLine(feedbackVM.Email);
            if (ModelState.IsValid)
            {
                feedbackContext.Add(feedbackVM);
                return View();
            }

            return View(feedbackVM);
        }




        public ActionResult LogOut()
        {
            // Clear all key-values pairs stored in session state 
            HttpContext.Session.Clear();
            // Call the Index action of Home controller 
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Login(IFormCollection formData)
        {
            // Read inputs from textboxes
            // Email address converted to lowercase
            string password = formData["txtPassword"].ToString();
            if (password == "123")
            {
                return View("AdminView");
            }

            // Store an error message in TempData for display at the index view 
            TempData["Message"] = "Invalid Login Credentials!";

            // Redirect user back to the index view through an action
            return View();
        }
    }
}
