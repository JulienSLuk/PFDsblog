using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB2022_ZZFashion.DAL;
using WEB2022_ZZFashion.Models;

namespace WEB2022_ZZFashion.Controllers
{
    public class FeedbackController : Controller
    {
        private FeedbackDAL feedbackContext = new FeedbackDAL();
        private ResponseDAL responseContext = new ResponseDAL();
        public IActionResult Index()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Marketing Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            List <Feedback> feedbackList = feedbackContext.GetAllFeedback();
            return View(feedbackList);
        }
        public ActionResult Details(int id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Staff" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Marketing Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            Feedback feedback = feedbackContext.GetFeedback(id);
            FeedbackViewModel feedbackVM = MapToFeedbackVM(feedback);
            return View(feedbackVM);
        }
        public FeedbackViewModel MapToFeedbackVM(Feedback feedback)
        {
            List<Response> responseList =responseContext.GetStaffResponseByFeedbackID(feedback.FeedbackID);
            FeedbackViewModel feedbackVM = new FeedbackViewModel
            {
                FeedbackID = feedback.FeedbackID,
                MemberID = feedback.MemberID,
                DateTimePosted = feedback.DateTimePosted,
                Title = feedback.Title,
                Text = feedback.Text,
                ImageFileName = feedback.ImageFileName,
                ResponseList = responseList
                
            };

            return feedbackVM;
        }

    }
    
}
