using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WEB2022_ZZFashion.DAL;
using WEB2022_ZZFashion.Models;

namespace WEB2022_ZZFashion.Controllers
{
    public class ProductManagerController : Controller
    {
        private ProductDAL productContext = new ProductDAL();
        // GET: ProductManager
        public ActionResult Index()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
            (HttpContext.Session.GetString("Role") != "Product Manager"))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public ActionResult AllProducts()
        {
            List<Product> productList = productContext.GetAllProduct();
            return View(productList);
        }
        public ActionResult NewProducts()
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

        public ActionResult Details(int productID)
        {
            Product product = productContext.GetDetails(productID);
            ProductViewModel productVM = MapToProductViewModel(product);
            return View(productVM);
        }
        public ProductViewModel MapToProductViewModel(Product product)
        {
            List<Product> productList = productContext.GetProductByProductID(product.ID);
            ProductViewModel productVM = new ProductViewModel
            {
                ProductID = product.ID,
                ProductTitle = product.Title,
                ProductImage = product.Image,
                ProductDesc = product.Desc,
                ProductCat = product.Cat,
                Obsolete = product.ObsoleteStatus,
                ProductList = productList
            };
            return productVM;

        }

        public ActionResult Create(int id)
        {
            Product product = new Product
            {

            };
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.ID = productContext.AddProduct(product);
                return RedirectToAction("AllProducts");
            }
            else
            {
                return View(product);
            }
        }
        public ActionResult Edit(int id)
        {
            Product product = productContext.GetDetails(id);
            product.ID = id;
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                productContext.Update(product);
                return RedirectToAction("AllProducts");
            }
            else
            {
                return View(product);
            }
        }
        // GET
        public ActionResult Delete(int id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }
            Product product = productContext.GetDetails(id);
            if(product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Product product)
        {
            productContext.Delete(product.ID);
            return RedirectToAction("AllProducts");   
        }

        

    }
}
