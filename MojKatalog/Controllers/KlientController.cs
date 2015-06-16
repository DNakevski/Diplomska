using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Models;
using MojKatalog.Queries;
using MojKatalog.Models.ViewModels;

namespace MojKatalog.Controllers
{
    public class KlientController : Controller
    {
        //
        // GET: /Klient/
        QKlient klient = new QKlient();
        public ActionResult Index()
        {
            var poedinec = (LogiranPoedinecViewModel)Session["Poedinec"];
            var kompanija = (LogiranaKompanijaViewModel)Session["Kompanija"];
            List<Klienti> model = new List<Klienti>();

            //if(poedinec != null)
            //{
            //    model = klient.IzlistajKlientiZaPoedinec(poedinec.IdPoedinec);
            //}
            //else
            //{
            //    model = klient.IzlistajKlientiZaKompanija(kompanija.IdKompanija);
            //}

            //ovoj treba da se odstrani
            model = klient.IzlistajKlientiZaKompanija(1);

            return View(model);
        }
        public ActionResult DodadiKlient()
        {
            return View(new Klienti());
        }
        [HttpPost]
        public ActionResult DodadiKlient(Klienti newKlient)
        {
            var poedinec = (LogiranPoedinecViewModel)Session["Poedinec"];
            var kompanija = (LogiranaKompanijaViewModel)Session["Kompanija"];

            //if(poedinec != null)
            //{
            //    klient.DodadiKlientZaPoedinec(newKlient, poedinec.IdPoedinec);
            //}
            //else
            //{
            //    klient.DodadiKlientZaKompanija(newKlient, kompanija.IdKompanija);
            //}

            //ovj treba da se odstrani
            klient.DodadiKlientZaKompanija(newKlient, 1);
            
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
