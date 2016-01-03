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
    public class PoedinecController : Controller
    {
        // GET: Poedinec
        QPoedinec _qpoedinec = new QPoedinec();
        public ActionResult Index()
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            Poedinci model = _qpoedinec.GetPoedinec(user.Id);
            return View(model);
        }
    }
}