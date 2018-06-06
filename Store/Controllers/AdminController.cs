using Store.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Store.Controllers
{
    public class AdminController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorize(Models.Admin userModel)
        {
            var userDetails = db.Admins.Where(x => x.Username == userModel.Username && x.Password == userModel.Password).FirstOrDefault();
            if (userDetails != null)
            {
                Session["Name"] = userDetails.Username;
                return RedirectToAction("About", "Home");
            }
            return View();
        }

        public PartialViewResult HelloAdmin()
        {
            return PartialView();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


    }
}