using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB2022_ZZFashion.DAL;

namespace WEB2022_ZZFashion.Controllers
{
   
    public class MarketingManagerController : Controller
    {
        private StaffDAL staffContext = new StaffDAL();
        // GET: MarketingManager
        public ActionResult Index()
        {
            string staffID=HttpContext.Session.GetString("LoginID");
            string sName = staffContext.findName(staffID);
            TempData["sName"] = sName;
            return RedirectToAction("Index", "Feedback");
        }
        

        
    }
}
