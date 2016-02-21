using MojKatalog.Filters;
using MojKatalog.Models.ViewModels;
using MojKatalog.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MojKatalog.Controllers
{
    [CustomAuthorize(Roles = "Admin,Poedinec,Kompanija")]
    public class WebSiteSettingsController : Controller
    {
        QKatalog _model;
        QWebSiteSettings modelw;
        public WebSiteSettingsController()
        {
         modelw = new QWebSiteSettings();
            _model = new QKatalog();
        }
        // GET: WebSiteSettings
        public ActionResult Index(string status = "")
        {
            ViewBag.Status = status;
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            return View(modelw.KataloziWebSiteList(user.Id, user.UserType));
        }
        public ActionResult KreirajWebSite(int id, string tip)
        {
            return RedirectToAction("Dodadi"+tip, tip, new { area = tip, id=id });
        }
        public ActionResult IzmeniWebSite(int id, string tip)
        {
            return RedirectToAction("Izmeni" + tip, tip, new { area = tip, id = id});
        }
        public ActionResult IzbrisiWebSite(int id)
        {
            if(modelw.DeleteWebSajt(id))
                return RedirectToAction("Index", new { status = "deleted" });
            else
                return RedirectToAction("Index", new { status = "error" });
        }

        [HttpPost]
        public JsonResult PublishSite(int siteId)
        {
            string status = "published";
            if (!modelw.PublishWebSite(siteId))
                status = "error";
            return Json(new { status = status });
        }
        [HttpPost]
        public JsonResult UnpublishSite(int siteId)
        {
            string status = "unpublished";
            if (!modelw.UnPublishWebSite(siteId))
                status = "error";

            return Json(new { status = status });
        }
    }
}