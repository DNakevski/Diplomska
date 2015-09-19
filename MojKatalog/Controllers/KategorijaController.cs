using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Models;
using MojKatalog.Queries;
using Newtonsoft.Json;
using MojKatalog.Models.ViewModels;
using MojKatalog.Filters;

namespace MojKatalog.Controllers
{

    [CustomAuthorize(Roles = "Admin,Poedinec,Kompanija")]
    public class KategorijaController : Controller
    {
        //
        // GET: /Kategorija/
        QKategorija model = new QKategorija();
        QKatalog katalog = new QKatalog();
        public ActionResult Index()
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            KataloziIdINaziv[] m = katalog.KataloziIdINaziv(user.Id, user.UserType);
            
            ViewBag.DistinctKatalozi = m;
            KataloziIdINaziv [] lista = m;
            return View();
        }

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
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            return Json(model.IzlistajKatalozi(user.Id, user.UserType), JsonRequestBehavior.AllowGet);
        }

    }
}
