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
    public class BlogManagerController : Controller
    {
        //accessing data from product manager dal
        private BlogManagerDAL productContext = new BlogManagerDAL();
        public ActionResult ProductManagerMain() {
            //checking role for product manager
            if ((HttpContext.Session.GetString("Role") == null) || 
                (HttpContext.Session.GetString("Role") != "Product Manager"))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        //if view products, display products
        public ActionResult ViewProducts()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                   (HttpContext.Session.GetString("Role") != "Product Manager"))
            {
                return RedirectToAction("Index", "Home");
            }

            //get products from product list to display on page
            List<ProductManager> productList = productContext.GetAllProducts();
            return View(productList);
        }       
        
        //create product
        public ActionResult CreateProduct()
        {
            if ((HttpContext.Session.GetString("Role") == null) ||
                      (HttpContext.Session.GetString("Role") != "Product Manager"))
            {
                return RedirectToAction("ViewProducts", "ProductManager");
            }

            return View();
        }



        ////To get image for product and store into database
        //[HttpPost]
        //public async Task<IActionResult> CreateProduct(ProductManager product)
        //{
        //    //check if image path is null or already filled in
        //    if (product.photoUpload != null && product.photoUpload.Length > 0)
        //    {
        //        try
        //        {
        //            //getting extension path of image
        //            string fileExt = Path.GetExtension(product.photoUpload.FileName);

        //            //making image file name to be able to store into database
        //            string uploadedFile = product.blogName + fileExt;

        //            //save path into wwwroot folder into products so that it can be added to object
        //            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products", uploadedFile);


        //            using (var fileSteam = new FileStream(savePath, FileMode.Create))
        //            {
        //                await product.photoUpload.CopyToAsync(fileSteam);
        //            }

        //            //adding the product image product into product object
        //            product.blogImage = uploadedFile;
        //            ViewData["Message"] = "File uploaded successfully!";
        //        }
        //        catch (IOException)
        //        {
        //            ViewData["Message"] = "File uploading fail!";
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewData["Message"] = ex.Message;
        //        }

        //    }

        //    //if valid then add item to product manager object into database
        //    if (ModelState.IsValid)
        //    {
        //        product.blogID = productContext.Add(product);

        //        return RedirectToAction("ViewProducts", "ProductManager");
        //    }
        //    else
        //    {
        //        return View(product);
        //    }
        //}

        //for edit product
        //public ActionResult EditProduct(int id)
        //{

        //    if ((HttpContext.Session.GetString("Role") == null) ||
        //           (HttpContext.Session.GetString("Role") != "Product Manager"))
        //    {
        //        return RedirectToAction("Index", "Home");
        //    }

        //    //get product details to display in edit mode to show what object the user has selected
        //    ProductManager product = productContext.GetProductDetails(id);

        //    if (product == null)
        //    {
        //        return RedirectToAction("ViewProducts", "ProductManager");
        //    }

        //    ViewData["Conditions"] = GetCondition(product);

        //    return View(product);
        //}

        ////to update photo into database
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditProduct(ProductManager product, int id)
        //{
        //    if (product.photoUpload != null && product.photoUpload.Length > 0)
        //    {
        //        try
        //        {
        //            string fileExt = Path.GetExtension(product.photoUpload.FileName);

        //            string uploadedFile = product.blogName + fileExt;

        //            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products", uploadedFile);

        //            using (var fileSteam = new FileStream(savePath, FileMode.Create))
        //            {
        //                await product.photoUpload.CopyToAsync(fileSteam);
        //            }

        //            product.blogImage = uploadedFile;
        //            ViewData["Message"] = "File uploaded successfully!";
        //        }
        //        catch (IOException)
        //        {
        //            ViewData["Message"] = "File uploading fail!";
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewData["Message"] = ex.Message;
        //        }
            
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        productContext.UpdateProduct(product, id);
        //        return RedirectToAction("ViewProducts", "ProductManager");
        //    }
        //    else
        //    {
        //        ViewData["Conditions"] = GetCondition(product);
        //        return View(product);
        //    }
        //}
    }
}
