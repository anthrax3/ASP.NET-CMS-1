using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CMS.DAL;
using System.Diagnostics;

namespace CMS.Filters
{
    public class UserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if(HttpContext.Current.Session["Username"] == null)
            {
                RouteValueDictionary redirectTargetDictionary = new RouteValueDictionary();
                redirectTargetDictionary.Add("Account", "Login");

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Account" }));
            }

            base.OnActionExecuted(filterContext);
        }
    }

    public class AdminFilter : ActionFilterAttribute
    {
        private CMSContext db = new CMSContext();

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (HttpContext.Current.Session["Username"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Login", controller = "Account" }));
            }
            else
            {
                var username = (string)HttpContext.Current.Session["Username"];
                var user = db.Users.Where(u => u.Username.Equals(username)).FirstOrDefault();
                if (user.IsAdmin.Equals(0))
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Home" }));
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
    
    public class OwnershipFilterForUsersById : ActionFilterAttribute
    {
        private CMSContext db = new CMSContext();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                var username = HttpContext.Current.Session["Username"];
                var user = db.Users.Where(u => u.Username.Equals((string)username)).FirstOrDefault();
                if(user != null)
                {
                    foreach (var param in filterContext.ActionParameters)
                    {
                        if (param.Value.Equals(user.Id) || user.IsAdmin.Equals(1))
                        {
                            return;
                        }
                        else
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Home" }));
                        }
                    }
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Home" }));
                }
                
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Home" }));
            }

            base.OnActionExecuting(filterContext);
        }
    }

    public class OwnershipFilterForProjectsById : ActionFilterAttribute
    {
        private CMSContext db = new CMSContext();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["Username"] != null)
            {
                var username = (string)HttpContext.Current.Session["Username"];
                var user = db.Users.Where(u => u.Username.Equals(username)).FirstOrDefault();
                var id = filterContext.ActionParameters["id"];
                if(id != null)
                {
                    var proj = db.Accounts.Include("Projects").Where(p => p.Projects.Id.Equals((int)id)).FirstOrDefault();
                    if(proj != null)
                    {
                        if (proj.Users.Username.Equals(username) || !user.IsAdmin.Equals(0))
                        {
                            return;
                        }
                        else
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Home" }));
                        }
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Home" }));
                    }
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Index", controller = "Home" }));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}