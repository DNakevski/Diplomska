using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Models;
using MojKatalog.Queries;

namespace MojKatalog.Controllers
{
    public class KlientController : Controller
    {
        //
        // GET: /Klient/
        QKlient klient = new QKlient();
        public ActionResult Index()
        {
            return View(klient.izlistaj());
        }
        public ActionResult DodadiKlient()
        {
            return View(new Klienti());
        }
        [HttpPost]
        public ActionResult DodadiKlient(Klienti newKlient)
        {
            klient.dodadi(newKlient);
            return RedirectToAction("Index");
        }
        public ActionResult IzmeniKlient(int id)
        {
            return View(klient.vratiKlient(id));
        }
        [HttpPost]
        public ActionResult IzmeniKlient(Klienti newKlient)
        {
            klient.izmeni(newKlient);
            return RedirectToAction("Index");
        }
        public ActionResult IzbrisiKlient(int id)
        {
            klient.izbrisi(id);
            return RedirectToAction("Index");
        }
    }
}
