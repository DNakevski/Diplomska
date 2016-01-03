using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using MojKatalog.Queries;
using MojKatalog.Helpers;
using MojKatalog.Filters;

namespace MojKatalog.Controllers
{
    [CustomAuthorize(Roles = "Admin,Poedinec,Kompanija")]
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
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            var poraki = _poraki.GetPoraki(user.Id, user.UserType);
            return View(poraki);
        }
        public ActionResult IspratiMail()
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            ViewPoraki poraka = new ViewPoraki();
            poraka = _poraki.InicijalizirajViewPoraki(user.Id, user.UserType);
            return View(poraka);
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "IspratiMail")]
        public ActionResult IspratiMail(ViewPoraki vporaka, int[] selectedKlients)
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];

            Poraki novaPoraka = new Poraki();
            novaPoraka.Subject = vporaka.Subject;
            novaPoraka.Body = vporaka.Body;
            novaPoraka.Date = DateTime.Now;
            novaPoraka.IsSent = true;
            novaPoraka.IsDeleted = false;
            novaPoraka.IsReceived = false;
            novaPoraka.Klienti = _klienti.ListaNaKlientiSporedId(selectedKlients);

            if (user.UserType == Helpers.Enumerations.LogedUserTypeEnum.Poedinec)
                novaPoraka.IdPoedinci = user.Id;
            else
                novaPoraka.IdKompanii = user.Id;
            
            _poraki.IspratiISnimiPoraka(novaPoraka);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "SocuvajPoraka")]
        public ActionResult SocuvajPoraka(ViewPoraki vporaka, int[] selectedKlients)
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];

            Poraki novaPoraka = new Poraki();
            novaPoraka.Subject = vporaka.Subject;
            novaPoraka.Body = vporaka.Body;
            novaPoraka.Date = DateTime.Now;
            novaPoraka.IsSent = false;
            novaPoraka.IsDeleted = false;
            novaPoraka.IsReceived = false;
            novaPoraka.Klienti = _klienti.ListaNaKlientiSporedId(selectedKlients);

            if (user.UserType == Helpers.Enumerations.LogedUserTypeEnum.Poedinec)
                novaPoraka.IdPoedinci = user.Id;
            else
                novaPoraka.IdKompanii = user.Id;
            
            _poraki.SocuvajPoraka(novaPoraka);
            return RedirectToAction("Index");
        }

        public ActionResult PrebarajKlienti(string searchString)
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            ViewBag.SearchString = searchString;
            List<ViewKlienti> klienti = (user.UserType == Helpers.Enumerations.LogedUserTypeEnum.Poedinec) ?
                _poraki.PrebarajKontaktiZaPoedinec(user.Id, searchString) :
                _poraki.PrebarajKontaktiZaKompanija(user.Id, searchString);

            return PartialView("_UpdateModalKlienti",klienti);
        }

        [HttpGet]
        public PartialViewResult GetIsprateniPartial()
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            var poraki = _poraki.GetIsprateniPoraki(user.Id, user.UserType);
            return PartialView("Partials/_IsprateniPorakiPartial", poraki);
        }

        [HttpGet]
        public PartialViewResult GetPrimeniPartial()
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            var poraki = _poraki.GetPrimeniPoraki(user.Id, user.UserType);
            return PartialView("Partials/_PrimeniPorakiPartial", poraki);
        }

        [HttpGet]
        public PartialViewResult GetIzbrishaniPartial()
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            var poraki = _poraki.GetIzbrishaniPoraki(user.Id, user.UserType);
            return PartialView("Partials/_IzbrisaniPorakiPartial", poraki);
        }

        [HttpGet]
        public PartialViewResult GetSocuvaniPartial()
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            var poraki = _poraki.GetSocuvaniPoraki(user.Id, user.UserType);
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
        public JsonResult DeletePrimeni(List<int> porakiIds)
        {
            string status = "Fail";

            if (_poraki.DeletePrimeniPoraki(porakiIds))
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

        [HttpGet]
        public ActionResult IzlistajPoraka(int idPoraka)
        {
            Poraki poraka = _poraki.VratiPorakaSporedId(idPoraka);
            return PartialView("Partials/_PregledajPoraka", poraka);
        }
    }
}
