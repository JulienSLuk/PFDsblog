using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WEB2022_ZZFashion.DAL;
using WEB2022_ZZFashion.Models;

namespace WEB2022_ZZFashion.Controllers
{
    public class MemberController : Controller
    {
        private CustomerDAL customerContext = new CustomerDAL();
        private FeedbackDAL feedbackContext = new FeedbackDAL();
        private ResponseDAL responseContext = new ResponseDAL();

        // GET: Member
        public ActionResult Index()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Member" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }

            Customer currentCustomer = customerContext.GetCustomer(HttpContext.Session.GetString("LoginID"));
            List<Feedback> customerFeedbacks = feedbackContext.GetMemberFeedbacks(currentCustomer.MemberID);
            List<Response> customerResponse = responseContext.GetMemberResponse(currentCustomer.MemberID);
            TempData["UserName"] = currentCustomer.Name;
            TempData["UserBD"] = currentCustomer.DOB.Month.ToString();
            ViewBag.feedbacks = customerFeedbacks;
            ViewBag.responses = customerResponse;
            return View();
        }

        // GET: Member/Profile
        public ActionResult Profile()
        {
            // Stop accessing the action if not logged in
            // or account not in the "Member" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }

            Customer currentCustomer = customerContext.GetCustomer(HttpContext.Session.GetString("LoginID"));
            TempData["UserName"] = currentCustomer.Name;
            TempData["UserEmail"] = currentCustomer.Email;
            TempData["UserPhone"] = currentCustomer.ContactNo;
            TempData["UserAddr"] = currentCustomer.ResidentialAddr;
            TempData["UserBD"] = currentCustomer.DOB.ToString("dd/MM/yyyy");
            return View();
        }

        // GET: Member/Feedback
        public ActionResult Feedback(int id)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Member" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }

            Customer currentCustomer = customerContext.GetCustomer(HttpContext.Session.GetString("LoginID"));
            Feedback currentFeedback = feedbackContext.GetFeedback(id);
            List<Response> currentResponse = responseContext.GetResponseByFeedbackID(id);
            TempData["UserName"] = currentCustomer.Name;

            ViewBag.currentResponse = currentResponse;
            return View(currentFeedback);
        }

        // GET: Member/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Member/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Member/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Member/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Member/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Member/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // Post for updating PW 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPW(IFormCollection formData)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Member" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }

            string currentPW = formData["currentPW"].ToString();
            string newPW = formData["newPW"].ToString();
            string cNewPW = formData["cNewPW"].ToString();

            // Validation for change of password
            if (customerContext.CheckPassword(HttpContext.Session.GetString("LoginID"), currentPW))
            {

                if (newPW == "")
                {
                    TempData["nPWError"] = "Cannot be empty!";
                }
                else if (newPW.Length < 6)
                {
                    TempData["nPWError"] = "Password is too short, must consist of atleast 6 characters!";
                }
                else if (newPW.Length > 20)
                {
                    TempData["nPWError"] = "Password cannot exceed 20 characters";
                }
                else if (!newPW.Equals(cNewPW))
                {
                    TempData["cPWError"] = "Confirm New Password is not the same as new password!";
                }
                else
                {
                    Customer customer = customerContext.GetCustomer(HttpContext.Session.GetString("LoginID"));
                    customerContext.UpdatePW(customer, newPW);
                }
            }
            else
            {
                TempData["oldPWError"] = "Current Password does not match!";
            }

            return RedirectToAction("Profile");
        }

        // Post for updating Email
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmail(IFormCollection formData)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Member" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }

            string newEmail = formData["newEmail"].ToString();
            string cNewEmail = formData["cNewEmail"].ToString();

            if (!customerContext.IsEmailExist(newEmail, HttpContext.Session.GetString("LoginID")))
            {
                if (newEmail == "")
                {
                    TempData["nEmailError"] = "Cannot be empty!";
                }
                else if (!newEmail.Equals(cNewEmail))
                {
                    TempData["cEmailError"] = "Confirm email is not the same as new email!";
                }
                else
                {
                    customerContext.UpdateEmail(HttpContext.Session.GetString("LoginID"), newEmail);
                }
            }
            else
            {
                TempData["nEmailError"] = "Email is already being used by someone else!";
            }

            return RedirectToAction("Profile");
        }

        // Post for updating contact number
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditContact(IFormCollection formData)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Member" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }

            string newPhone = formData["newPhone"].ToString();
            string cNewPhone = formData["cNewPhone"].ToString();

            if (!customerContext.IsContactExist(newPhone, HttpContext.Session.GetString("LoginID")))
            {
                if (newPhone == "")
                {
                    TempData["nPhoneError"] = "Cannot be empty!";
                }
                else if (!newPhone.Equals(cNewPhone))
                {
                    TempData["cEmailError"] = "Confirm phone number is not the same as new phone number!";
                }
                else
                {
                    customerContext.UpdatePhone(HttpContext.Session.GetString("LoginID"), newPhone);
                }
            }
            else
            {
                TempData["nPhoneError"] = "Phone number is already being used by someone else!";
            }

            return RedirectToAction("Profile");
        }

        // Post for updating Address
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAddr(IFormCollection formData)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Member" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }

            string newAddr = formData["newAddr"].ToString();
            string cNewAddr = formData["cNewAddr"].ToString();

            if (newAddr == "")
            {
                TempData["nAddrError"] = "Cannot be empty!";
            }
            else if (!newAddr.Equals(cNewAddr))
            {
                TempData["cAddrError"] = "Confirm address is not the same as new address!";
            }
            else
            {
                customerContext.UpdateAddr(HttpContext.Session.GetString("LoginID"), newAddr);
            }

            return RedirectToAction("Profile");
        }

        // Post for feedback
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GiveFeedback(IFormCollection formData, Models.Feedback feedback)
        {
            // Stop accessing the action if not logged in
            // or account not in the "Member" role
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Member"))
            {
                return RedirectToAction("Index", "Home");
            }

            string title = formData["title"].ToString();
            string text = feedback.Text;
            string imageFile = formData["file"].ToString();

            // Validation
            if (title == "")
            {
                TempData["titleError"] = "Please enter a title!";
            }
            else if (text == "")
            {
                TempData["textError"] = "Cannot be empty!";
            }
            else
            {
                Feedback feedback1 = new Feedback();
                feedback1.Title = title;
                feedback1.MemberID = HttpContext.Session.GetString("LoginID");
                feedback1.Text = text;
                feedback1.ImageFileName = imageFile;
                feedbackContext.AddFeedback(feedback1);
                
            }

            return RedirectToAction("Index");
        }
    }
}
