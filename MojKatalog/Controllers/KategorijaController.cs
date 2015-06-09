using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Models;
using MojKatalog.Queries;
using Newtonsoft.Json;

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
            ViewBag.DistinctKatalozi = katalog.kataloziIdINaziv();
            KataloziIdINaziv [] lista = katalog.kataloziIdINaziv();
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
            int lastId = model.dodadi(new Kategorii()
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
            model.izmeni(id, naziv);
        }
        [HttpGet]
        public void IzbrisiKategorija(int id)
        {
            model.izbrisi(id);
        }

        [HttpGet]
        public ActionResult IzlistajKategorii()
        {
            return Json(model.izlistaj(), JsonRequestBehavior.AllowGet);
        }

    }
}
