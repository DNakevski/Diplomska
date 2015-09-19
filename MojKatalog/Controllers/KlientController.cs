using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Models;
using MojKatalog.Queries;
using MojKatalog.Models.ViewModels;
using MojKatalog.Filters;

namespace MojKatalog.Controllers
{
    [CustomAuthorize(Roles = "Admin,Poedinec,Kompanija")]
    public class KlientController : Controller
    {
        //
        // GET: /Klient/
        QKlient klient = new QKlient();
        public ActionResult Index()
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            List<Klienti> model = new List<Klienti>();

            if (user.UserType == Helpers.Enumerations.LogedUserTypeEnum.Poedinec)
            {
                model = klient.IzlistajKlientiZaPoedinec(user.Id);
            }
            else if(user.UserType == Helpers.Enumerations.LogedUserTypeEnum.Kompanija)
            {
                model = klient.IzlistajKlientiZaKompanija(user.Id);
            }

            return View(model);
        }

        public ActionResult DodadiKlient()
        {
            return View(new Klienti());
        }

        [HttpPost]
        public ActionResult DodadiKlient(Klienti newKlient)
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            if (user.UserType == Helpers.Enumerations.LogedUserTypeEnum.Poedinec)
            {
                klient.DodadiKlientZaPoedinec(newKlient, user.Id);
            }
            else if (user.UserType == Helpers.Enumerations.LogedUserTypeEnum.Kompanija)
            {
                klient.DodadiKlientZaKompanija(newKlient, user.Id);
            }

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
