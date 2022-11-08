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

        




 


    }
}
