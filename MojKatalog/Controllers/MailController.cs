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
        QPoraki poraki = new QPoraki();
        QKlient klienti = new QKlient();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IspratiMail()
        {
            ViewPoraki poraka = new ViewPoraki();
            poraka = poraki.InicijalizirajViewPoraki(1);
            return View(poraka);
        }

        [HttpPost]
        public ActionResult IspratiMail(ViewPoraki vporaka, int[] selectedKlients)
        {
            Poraki novaPoraka = new Poraki();
            novaPoraka.Subject = vporaka.Subject;
            novaPoraka.Body = vporaka.Body;
            novaPoraka.Klienti = klienti.ListaNaKlientiSporedId(selectedKlients);
            poraki.IspratiPoraka(novaPoraka);
            return RedirectToAction("Index");
        }
        public ActionResult PrebarajKlienti(string searchString)
        {
            ViewBag.SearchString = searchString;
            List<ViewKlienti> klienti = poraki.PrebarajKontakti(1, searchString);
            return PartialView("_UpdateModalKlienti",klienti);
        }

    }
}
