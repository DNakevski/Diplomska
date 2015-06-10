using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Models.ViewModels;
using MojKatalog.Models;
using MojKatalog.Queries;
namespace MojKatalog.Controllers
{
    public class KatalogController : Controller
    {
        //
        // GET: /Katalog/
        QKatalog _model;
        public KatalogController()
        {
            _model = new QKatalog();
        }

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

            return View(_model.IzlistajKatalozi(1, Helpers.Enumerations.LogedUserTypeEnum.Kompanija));
            
        }
        [HttpGet]
        public ActionResult DodadiKatalog()
        {
            return View(new Katalozi());
        }
        [HttpPost]
        public ActionResult DodadiKatalog(Katalozi katalog)
        {
            _model.DodadiKatalog(katalog);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult IzmeniKatalog(int id)
        {
            Katalozi katalog = _model.VratiKatalog(id);
            return View(katalog);
        }
        [HttpPost]
        public ActionResult IzmeniKatalog(Katalozi katalog)
        {
            _model.IzmeniKatalog(katalog);
            return RedirectToAction("Index");
        }
        public ActionResult PregledajKatalog(int id)
        {
            return View(_model.VratiKatalog(id));
        }
        public JsonResult VratiDrvoZaKatalog(int id) 
        {
            QKategorija qKategorija = new QKategorija();
            var model = qKategorija.IzlistajSporedId(id);
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult IzbrisiKatalog(int id)
        {
            _model.IzbrisiKatalog(id);
            return RedirectToAction("Index");
        }


    }
}
