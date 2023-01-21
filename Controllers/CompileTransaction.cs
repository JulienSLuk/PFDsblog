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
    public class CompileTransaction : Controller
    {
        // GET: CompileTransaction
        private CustomerDAL customerContext = new CustomerDAL();
        public ActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Marketing Personnel"))
            {
                return RedirectToAction("Index", "Home");
            }
            List<Customer> customerList = customerContext.GetAllCustomer();
            return View(customerList);
  
        }

        
    }
}
