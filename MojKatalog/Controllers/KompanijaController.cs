using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Queries;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;

namespace MojKatalog.Controllers
{
    public class KompanijaController : Controller
    {
        QKompanija _qkompanija = new QKompanija();
        public ActionResult Index()
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            Kompanii model = _qkompanija.GetKompanija(user.Id);
            return View(model);
        }
        public ActionResult IzmeniKompanijaInfo()
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            Kompanii model = _qkompanija.GetKompanija(user.Id);
            return View(model);
        }

        [HttpPost]
        public ActionResult IzmeniKompanijaInfo(Kompanii newKompanija)
        {
          
            _qkompanija.SetKompanija(newKompanija);
            return View(newKompanija);
        }

    }
}