using CMS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS.Controllers
{
    public class HomeController : Controller
    {
        private CMSContext db = new CMSContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public PartialViewResult AjaxSearch(string q)
        {
            var data = db.Accounts.Include("Users").Include("Projects").Where(a => a.Users.Username.Contains(q) || a.Users.FirstName.Contains(q) || a.Users.LastName.Contains(q) || a.Users.Location.Contains(q) || a.Projects.ProjectName.Contains(q));
            return PartialView("_AjaxSearch", data);
        }
    }
}