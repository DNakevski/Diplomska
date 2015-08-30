using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using MojKatalog.Queries;

namespace MojKatalog.Controllers
{
    public class MailController : Controller
    {
        //
        // GET: /Mail/
        dbKatalogEntities context = new dbKatalogEntities();
        QPoraki _poraki;
        QKlient _klienti;

        public MailController()
        {
            _poraki = new QPoraki(context);
            _klienti = new QKlient(context);
        }


        public ActionResult Index()
        {
            //Treba da se napravi porakite da se zimaat spored Poedinec/Kompanija
            var poraki = _poraki.GetPoraki();
            return View(poraki);
        }
        public ActionResult IspratiMail()
        {
            ViewPoraki poraka = new ViewPoraki();
            poraka = _poraki.InicijalizirajViewPoraki(1);
            return View(poraka);
        }

        [HttpPost]
        public ActionResult IspratiMail(ViewPoraki vporaka, int[] selectedKlients)
        {

            Poraki novaPoraka = new Poraki();
            novaPoraka.Subject = vporaka.Subject;
            novaPoraka.Body = vporaka.Body;
            novaPoraka.Date = DateTime.Now;
            novaPoraka.Klienti = _klienti.ListaNaKlientiSporedId(selectedKlients);
            _poraki.IspratiISnimiPoraka(novaPoraka);
            return RedirectToAction("Index");
        }
        public ActionResult PrebarajKlienti(string searchString)
        {
            ViewBag.SearchString = searchString;
            List<ViewKlienti> klienti = _poraki.PrebarajKontakti(1, searchString);
            return PartialView("_UpdateModalKlienti",klienti);
        }

    }
}
