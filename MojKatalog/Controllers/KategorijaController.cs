using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Models;
using MojKatalog.Queries;
using Newtonsoft.Json;
using MojKatalog.Models.ViewModels;

namespace MojKatalog.Controllers
{
  
    public class KategorijaController : Controller
    {
        //
        // GET: /Kategorija/
        QKategorija model = new QKategorija();
        QKatalog katalog = new QKatalog();
        public ActionResult Index()
        {
            var poedinec = (LogiranPoedinecViewModel)Session["Poedinec"];
            var kompanija = (LogiranaKompanijaViewModel)Session["Kompanija"];
            KataloziIdINaziv[] m = katalog.KataloziIdINaziv(1, Helpers.Enumerations.LogedUserTypeEnum.Kompanija);
            //if (poedinec != null)
            //{
            //    m = katalog.KataloziIdINaziv(1, Helpers.Enumerations.LogedUserTypeEnum.Poedinec);
            //}
            //else
            //{
            //    m = katalog.KataloziIdINaziv(1, Helpers.Enumerations.LogedUserTypeEnum.Kompanija);
            //}
            
            ViewBag.DistinctKatalozi = m;
            KataloziIdINaziv [] lista = m;
            return View();
        }
     /*   [HttpGet]
        public ActionResult DodadiKategorija(int id) {
           ViewBag.PanelId = id;
           return PartialView("_DodadiKategorija");
        }*/
        [HttpPost]
        public int DodadiKategorija(string naziv, int? roditel, int katalogId)
        {
            int lastId = model.DodadiKategorija(new Kategorii()
            {
                NazivNaKategorija = naziv,
                RoditelId = (roditel == 0) ? null : roditel,
                IdKatalozi=katalogId
            });
            return lastId;
        }

        [HttpPost]
        public void IzmeniKategorija(int id, string naziv)
        {
            model.IzmeniKategorija(id, naziv);
        }
        [HttpGet]
        public void IzbrisiKategorija(int id)
        {
            model.IzbrisiKategorija(id);
        }

        [HttpGet]
        public ActionResult IzlistajKategorii()
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
            return Json(model.IzlistajKatalozi(1, Helpers.Enumerations.LogedUserTypeEnum.Kompanija), JsonRequestBehavior.AllowGet);
        }

    }
}
