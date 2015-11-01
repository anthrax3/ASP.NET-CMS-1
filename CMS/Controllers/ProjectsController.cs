using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using CMS.DAL;
using CMS.Models;
using CMS.Filters;

namespace CMS.Controllers
{
    public class ProjectsController : Controller
    {
        private CMSContext db = new CMSContext();

        [AdminFilter]
        public ActionResult Index()
        {
            return View(db.Accounts.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(string projectname)
        {
            if (projectname == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Where(p => p.ProjectName.Equals(projectname)).FirstOrDefault();
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        [UserFilter]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectName,ProjectDesc,ProjectImage")] Project project)
        {
            project.PostedOn = DateTime.Today;

            var username = (string)Session["Username"];
            var user = db.Users.Where(u => u.Username.Equals(username)).FirstOrDefault();
            Account account = new Account();
            account.Projects = project;
            account.Users = user;
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction(user.Username, "User");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        [OwnershipFilterForProjectsById]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectName,ProjectDesc,ProjectImage,PostedOn")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [OwnershipFilterForProjectsById]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            Account proj_id = db.Accounts.Include("Projects").Where(p => p.Projects.Id.Equals(id)).FirstOrDefault();
            db.Accounts.Remove(proj_id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public PartialViewResult UserProjectList(string username)
        {
            var a = db.Accounts.Where(u => u.Users.Username.Equals(username));
            return PartialView("_UserProjectList", a.ToList());
        }
    }
}
