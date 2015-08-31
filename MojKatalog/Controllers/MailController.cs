using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using MojKatalog.Queries;
using MojKatalog.Helpers;

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
        [MultipleButton(Name = "action", Argument = "IspratiMail")]
        public ActionResult IspratiMail(ViewPoraki vporaka, int[] selectedKlients)
        {

            Poraki novaPoraka = new Poraki();
            novaPoraka.Subject = vporaka.Subject;
            novaPoraka.Body = vporaka.Body;
            novaPoraka.Date = DateTime.Now;
            novaPoraka.IsSent = true;
            novaPoraka.IsDeleted = false;
            novaPoraka.IdPoedinci = 1;
            novaPoraka.Klienti = _klienti.ListaNaKlientiSporedId(selectedKlients);
            _poraki.IspratiISnimiPoraka(novaPoraka);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "SocuvajPoraka")]
        public ActionResult SocuvajPoraka(ViewPoraki vporaka, int[] selectedKlients)
        {

            Poraki novaPoraka = new Poraki();
            novaPoraka.Subject = vporaka.Subject;
            novaPoraka.Body = vporaka.Body;
            novaPoraka.Date = DateTime.Now;
            novaPoraka.IsSent = false;
            novaPoraka.IsDeleted = false;
            novaPoraka.IdPoedinci = 1;
            novaPoraka.Klienti = _klienti.ListaNaKlientiSporedId(selectedKlients);
            _poraki.SocuvajPoraka(novaPoraka);
            return RedirectToAction("Index");
        }

        public ActionResult PrebarajKlienti(string searchString)
        {
            ViewBag.SearchString = searchString;
            List<ViewKlienti> klienti = _poraki.PrebarajKontakti(1, searchString);
            return PartialView("_UpdateModalKlienti",klienti);
        }

        [HttpGet]
        public PartialViewResult GetIsprateniPartial()
        {
            var poraki = _poraki.GetIsprateniPoraki();
            return PartialView("Partials/_IsprateniPorakiPartial", poraki);
        }

        [HttpGet]
        public PartialViewResult GetIzbrishaniPartial()
        {
            var poraki = _poraki.GetIzbrishaniPoraki();
            return PartialView("Partials/_IzbrisaniPorakiPartial", poraki);
        }

        [HttpGet]
        public PartialViewResult GetSocuvaniPartial()
        {
            var poraki = _poraki.GetSocuvaniPoraki();
            return PartialView("Partials/_SocuvaniPorakiPartial", poraki);
        }

        [HttpPost]
        public JsonResult DeleteIsprateni(List<int> porakiIds)
        {
            string status = "Fail";

            if (_poraki.DeleteIsprateniPoraki(porakiIds))
                status = "Success";

            return Json(new { Status = status });
        }

        [HttpPost]
        public JsonResult DeleteIzbrishani(List<int> porakiIds)
        {
            string status = "Fail";

            if (_poraki.DeleteIzbrishaniPoraki(porakiIds))
                status = "Success";

            return Json(new { Status = status });
        }

        [HttpPost]
        public JsonResult DeleteSocuvani(List<int> porakiIds)
        {
            string status = "Fail";

            if (_poraki.DeleteSocuvaniPoraki(porakiIds))
                status = "Success";

            return Json(new { Status = status });
        }

    }
}
