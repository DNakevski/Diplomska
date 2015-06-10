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
            return View(klient.IzlistajKlienti());
        }
        public ActionResult DodadiKlient()
        {
            return View(new Klienti());
        }
        [HttpPost]
        public ActionResult DodadiKlient(Klienti newKlient)
        {
            klient.DodadiKlient(newKlient);
            return RedirectToAction("Index");
        }
        public ActionResult IzmeniKlient(int id)
        {
            return View(klient.VratiKlient(id));
        }
        [HttpPost]
        public ActionResult IzmeniKlient(Klienti newKlient)
        {
            klient.IzmeniKlient(newKlient);
            return RedirectToAction("Index");
        }
        public ActionResult IzbrisiKlient(int id)
        {
            klient.IzbrisiKlient(id);
            return RedirectToAction("Index");
        }
    }
}
