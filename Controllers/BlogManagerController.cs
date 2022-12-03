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

            return View();
        }


      

        public ActionResult ViewBlogsAdmin()
        {
            //get products from product list to display on page
            List<ProductManager> productList = productContext.GetAllProducts();
            return View(productList);
        }


        


        //create product
        public ActionResult CreateBlog()
        {
            return View();
        }

        private FeedbackDAL feedbackContext = new FeedbackDAL();
        public ActionResult ViewFeedbackAdmin(int? id)
        {
            // check if user role is marketing manager, else redirect back to master homepage

            List<FeedbackViewModel> feedbackVMList = new List<FeedbackViewModel>();
            feedbackVMList = feedbackContext.GetAllFeedbackAndResponses();

            // id == 1 means to only view unresponded feedback, else read all feedbacks
            if (id == 1)
            {
                HttpContext.Session.SetString("RespondedStatus", "All Unresponded Feedbacks (");
                feedbackVMList.RemoveAll(x => x.ResponseList.Count > 0);
            }
            else
            {
                HttpContext.Session.SetString("RespondedStatus", "All Feedbacks (");
            }

            HttpContext.Session.SetString("NoOfFeedback", feedbackVMList.Count().ToString());
            return View(feedbackVMList);
        }


        public ActionResult CreateResponse()
        {
            return View();
        }
        //To get image for product and store into database
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductManager product)
        {
            //check if image path is null or already filled in
            if (product.photoUpload != null && product.photoUpload.Length > 0)
            {
                try
                {
                    //getting extension path of image
                    string fileExt = Path.GetExtension(product.photoUpload.FileName);

                    //making image file name to be able to store into database
                    string uploadedFile = product.blogName + fileExt;

                    //save path into wwwroot folder into products so that it can be added to object
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\products", uploadedFile);


                    using (var fileSteam = new FileStream(savePath, FileMode.Create))
                    {
                        await product.photoUpload.CopyToAsync(fileSteam);
                    }

                    //adding the product image product into product object
                    product.blogImage = uploadedFile;
                    ViewData["Message"] = "File uploaded successfully!";
                }
                catch (IOException)
                {
                    ViewData["Message"] = "File uploading fail!";
                }
                catch (Exception ex)
                {
                    ViewData["Message"] = ex.Message;
                }

            }

            //if valid then add item to product manager object into database
            productContext.Add(product);

            return RedirectToAction("ViewBlogsAdmin", "BlogManager");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Response response)
        {
            // get all feedback and responses for corresponding feedback, all feedback sorted by DateTimePosted in descending order then followed by sorting FeedbackID in descending order
            List<FeedbackViewModel> feedbackList = feedbackContext.GetAllFeedbackAndResponses();
            List<Response> responseList = new List<Response>();

            // find if FeedbackID matches any of the feedback's FeedbackID in the list
            bool validFeedbackId = false;
            foreach (FeedbackViewModel fv in feedbackList)
            {
                if (fv.FeedbackID == response.FeedbackID)
                {
                    responseList = fv.ResponseList;
                    validFeedbackId = true;
                    break;
                }
            }
            feedbackContext.AddResponse(response, "Marketing Manager", "Marketing");
            //return RedirectToAction("Index", "MarketingManagerHome");

            return View();
        }


    }
}
