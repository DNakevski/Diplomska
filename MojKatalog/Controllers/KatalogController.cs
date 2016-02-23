using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Models.ViewModels;
using MojKatalog.Models;
using MojKatalog.Queries;
using MojKatalog.Filters;

namespace MojKatalog.Controllers
{
    [CustomAuthorize(Roles = "Admin,Poedinec,Kompanija")]
    public class KatalogController : Controller
    {
        //
        // GET: /Katalog/
        QKatalog _model;
        public KatalogController()
        {
            _model = new QKatalog();
        }

        public ActionResult Index(int ? id)
        {
            if (id != null)
            {
                ViewBag.RowSuccess = "row-" + id;
            }
            else
            {
                ViewBag.RowSuccess = "";
            }
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            return View(_model.IzlistajKatalozi(user.Id, user.UserType));

        }

        [HttpGet]
        public ActionResult DodadiKatalog()
        {
            return View(new Katalozi());
        }

        [HttpPost]
        public ActionResult DodadiKatalog(Katalozi katalog)
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            if (user.UserType == Helpers.Enumerations.LogedUserTypeEnum.Poedinec)
            {
                katalog.IdPoedinci = user.Id;
            }
            else if (user.UserType == Helpers.Enumerations.LogedUserTypeEnum.Kompanija)
            {
                katalog.IdKompanii = user.Id;
            }

            katalog.DataNaKreiranje = DateTime.Now;
            _model.DodadiKatalog(katalog);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult IzmeniKatalog(int id)
        {
            Katalozi katalog = _model.VratiKatalog(id);
            return View(katalog);
        }
        [HttpPost]
        public ActionResult IzmeniKatalog(Katalozi katalog)
        {
            _model.IzmeniKatalog(katalog);
            return RedirectToAction("Index", new { id =katalog.IdKatalozi});
        }
        public ActionResult PregledajKatalog(int id)
        {
            return View(_model.VratiKatalog(id));
        }
        public JsonResult VratiDrvoZaKatalog(int id)
        {
            QKategorija qKategorija = new QKategorija();
            var model = qKategorija.IzlistajSporedId(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult IzbrisiKatalog(int id)
        {
            _model.IzbrisiKatalog(id);
            return RedirectToAction("Index");
        }


    }
}
