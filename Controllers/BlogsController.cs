using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEB2022Apr_P01_T3.DAL;
using WEB2022Apr_P01_T3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;


namespace WEB2022Apr_P01_T3.Controllers
{
    public class BlogsController : Controller
    {
        private BlogManagerDAL productContext = new BlogManagerDAL();
        public IActionResult Index()
        {
            return View();
        }

        //if view products, display products
        public ActionResult ViewBlogs()
        {
            //get products from product list to display on page
            List<ProductManager> productList = productContext.GetAllProducts();
            return View(productList);
        }


        public ActionResult ViewTechBlogs()
        {
            //get products from product list to display on page
            List<ProductManager> productList = productContext.GetTechProducts();
            return View(productList);
        }


        public ActionResult ViewFinSchBlogs()
        {
            //get products from product list to display on page
            List<ProductManager> productList = productContext.GetFinSchProducts();
            return View(productList);
        }
    }
}
