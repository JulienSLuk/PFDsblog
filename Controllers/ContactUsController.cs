using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WEB2022Apr_P01_T3.Models;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WEB2022Apr_P01_T3.Controllers
{
    public class ContactUsController : Controller
    {
        // GET: ContactUsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ContactUsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ContactUsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContactUsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            //storing of form data into variables to be used to create a Contact object to store data into restDB
            string type = collection["Type"];
            string name = collection["Name"];
            string email = collection["Email"];
            string message = collection["Message"];

            var c = new Contact
            {
                Type = type,
                Name = name,
                Email = email,
                Message = message
            };

            //serializing form data into json object
            string jsonString = JsonSerializer.Serialize(c);

            //adding restclient to gain access to restdb
            var client = new RestClient("https://contactform-0263.restdb.io");
            var request = new RestRequest("rest/contact");
            request.AddHeader("cache-control", "no-cache");

            // key to add data into restDB
            request.AddHeader("x-apikey", "c8440da549bdf8cbe0f792f33c201b93c00cf");

            //content-type
            request.AddHeader("content-type", "application/json");

            // adding jsonString to be stored into restDB
            request.AddParameter("application/json", jsonString, ParameterType.RequestBody);

            // Method: POST
            request.Method = Method.Post;
            RestResponse response = client.Execute(request);

            //if success, go to home page otherwise remain at the same page and show error message
            if (response.StatusCode == HttpStatusCode.Created)
            {
                TempData["contactFormStatus"] = "Feedback Sent!";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["contactFormStatus"] = "Feedback Sent Unsuccessful";
                return RedirectToAction("Index", "ContactUs");
            }
        }

        // GET: ContactUsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ContactUsController/Edit/5
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

        // GET: ContactUsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ContactUsController/Delete/5
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
    }
}
