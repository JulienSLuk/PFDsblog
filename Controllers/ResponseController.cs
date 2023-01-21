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
    public class ResponseController : Controller
    {
        private ResponseDAL responseContext = new ResponseDAL();
        // GET: ResponseController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ResponseController/Details/5
        public ActionResult Details(int feedbackID)
        {
            return View();
        }
        //Get feedback ID for create
        

        // GET: ResponseController/Create
        public ActionResult Create(int id)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Marketing Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }

            String staffID = HttpContext.Session.GetString("LoginID");
            Response response = new Response();
            response.DateTimePosted = DateTime.Today;
            response.StaffID = staffID;
            response.FeedbackID = id;
            return View(response);
        }
        // POST: ResponseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Response response)
        {
            if (ModelState.IsValid)
            {
                response.ResponseID = responseContext.AddResponse(response);
                return RedirectToAction("Details", "Feedback", new { id = response.FeedbackID });
            }
            else
            {
                return View(response);
            }
        }

        // GET: ResponseController/Edit/5
        public ActionResult Edit(int id)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Marketing Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            Response response = responseContext.GetResponseDetails(id);
            return View(response);
        }
       

        // POST: ResponseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Response response)
        {  
            if (ModelState.IsValid)
            {
                int feedbackID = response.FeedbackID;
                responseContext.Update(response);
                return RedirectToAction("Details","Feedback", new { id = feedbackID });
            }
            else
            {
                return View(response);
            }
        }
        
        // GET: ResponseController/Delete/5
        public ActionResult Delete(int id)
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Marketing Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            int feedbackID = responseContext.GetFeedbackIDByResponseID(id);
            responseContext.DeleteResponse(id);
            return RedirectToAction("Details","Feedback", new { id=feedbackID});
            
        }

        
    }
}
