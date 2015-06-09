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
        QKatalog model = new QKatalog();
        public ActionResult Index()
        {
            List<ViewKataloziKategorii> kataloziIkategorii = model.izlistaj();
            return View(model.izlistaj());
        }
        [HttpGet]
        public ActionResult DodadiKatalog()
        {
            return View(new Katalozi());
        }
        [HttpPost]
        public ActionResult DodadiKatalog(Katalozi katalog)
        {
            model.dodadi(katalog);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult IzmeniKatalog(int id)
        {
            Katalozi katalog = model.vratiKatalog(id);
            return View(katalog);
        }
        [HttpPost]
        public ActionResult IzmeniKatalog(Katalozi katalog)
        {
            model.izmeni(katalog);
            return RedirectToAction("Index");
        }
        public ActionResult PregledajKatalog(int id)
        {
            return View(model.vratiKatalog(id));
        }
        public JsonResult VratiDrvoZaKatalog(int id) 
        {
            QKategorija qKategorija = new QKategorija();
            var model = qKategorija.izlistajSporedId(id);
            return Json(model,JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult IzbrisiKatalog(int id)
        {
            model.izbrisi(id);
            return RedirectToAction("Index");
        }


    }
}
