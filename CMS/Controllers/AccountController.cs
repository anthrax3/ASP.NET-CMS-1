using CMS.DAL;
using CMS.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using CMS.Filters;

namespace CMS.Controllers
{
    public class AccountController : Controller
    {
        private CMSContext db = new CMSContext();

        public ActionResult Register()
        {
            if(Session["Username"] != null)
            {
                return RedirectToAction((string)Session["Username"], "User");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "Id,Username,Password,Email,FirstName,LastName,About,Avatar,Location,Phone")] User user)
        {
            user.Password = Crypto.SHA256(user.Password);
            user.RegisteredOn = DateTime.Now;
            user.IsAdmin = 0;

            if (db.Users.Where(u => u.Username.Equals(user.Username)).FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "That username is taken!");
                return View();
            }
            else if (db.Users.Where(u => u.Email.Equals(user.Email)).FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Email is in use!");
                return View();
            }
            else if (db.Users.Where(u => u.Phone.Equals(user.Phone)).FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Phone is in use!");
                return View();
            }

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }

            return View(user);
        }

        public ActionResult Login()
        {
            if (Session["Username"] != null)
            {
                return RedirectToAction((string)Session["Username"], "User");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if(user.Username == null || user.Password == null)
            {
                return View();
            }
            else if (!ModelState.IsValidField(user.Username))
            {
                ModelState.AddModelError("", "Username is invalid!");
                return View();
            }
            else if (!ModelState.IsValidField(user.Password))
            {
                ModelState.AddModelError("", "Password is invalid!");
            }
            else if(ModelState.IsValidField(user.Username) && ModelState.IsValidField(user.Password) && LoginCheck(user))
            {
                Session["Username"] = user.Username;
                return RedirectToAction(user.Username, "User");
            }
            else
            {
                return View();
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }

        public bool LoginCheck(User u)
        {
            if(u.Username != null && u.Password != null)
            {
                var pass = Crypto.SHA256(u.Password);
                var v = db.Users.Where(a => a.Username.Equals(u.Username) && a.Password.Equals(pass)).FirstOrDefault();
                if (v != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}