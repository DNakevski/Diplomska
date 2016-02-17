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
        QKompanija _qpoedinec = new QKompanija();
        public ActionResult Index()
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            Kompanii model = _qpoedinec.GetKompanija(user.Id);
            return View(model);
        }
        public ActionResult IzmeniPoedinecInfo()
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            Kompanii model = _qpoedinec.GetKompanija(user.Id);
            return View(model);
        }
    }
}