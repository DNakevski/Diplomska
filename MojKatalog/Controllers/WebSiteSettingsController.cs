using MojKatalog.Models.ViewModels;
using MojKatalog.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MojKatalog.Controllers
{
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
        public ActionResult Index()
        {
            var poedinec = (LogiranPoedinecViewModel)Session["Poedinec"];
            var kompanija = (LogiranaKompanijaViewModel)Session["Kompanija"];

            //if(poedinec != null)
            //{
            //    return View(_model.IzlistajKatalozi(poedinec.IdPoedinec, Helpers.Enumerations.LogedUserTypeEnum.Poedinec));
            //} 
            //else
            //{
            //    return View(_model.IzlistajKatalozi(kompanija.IdKompanija, Helpers.Enumerations.LogedUserTypeEnum.Kompanija));
            //}

           // List<KataloziWebSiteViewModel> kataloziWeb = ;
            //ovoj treba da se izbrishe
           return View(modelw.KataloziWebSiteList(1, Helpers.Enumerations.LogedUserTypeEnum.Kompanija));
        }
        public ActionResult KreirajWebSite(int id, string tip)
        {
            return RedirectToAction("Dodadi"+tip, tip, new { area = tip, id=id });
        }
        public ActionResult IzmeniWebSite(int id, string tip)
        {
            return RedirectToAction("Izmeni" + tip, tip, new { area = tip, id = id });
        }
        public ActionResult IzbrisiWebSite(int id, string tip)
        {
            return View();
        }
    }
}