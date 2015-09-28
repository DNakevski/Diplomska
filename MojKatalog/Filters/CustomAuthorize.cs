using MojKatalog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MojKatalog.Filters
{
    /// <summary>
    /// [CustomAuthorize (Roles = "Admin,Poedinec,Kompanija")]
    /// </summary>
    public class CustomAuthorize : ActionFilterAttribute, IActionFilter
    {

        public string Roles { get; set; }
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {

            List<string> roles = Roles.Split(',').ToList();
            bool authorized = true;

            LoggedInEntity user = (LoggedInEntity)HttpContext.Current.Session["LoggedInEntity"];

            if (user == null || !roles.Contains(user.Role))
                authorized = false;


            if (!authorized)
            {
                RouteValueDictionary route = new RouteValueDictionary { {"Area", ""}, { "Controller", "Account" }, { "Action", "Login" } };
                filterContext.Result = new RedirectToRouteResult(route);
            }

            this.OnActionExecuting(filterContext);
        }
    }
}