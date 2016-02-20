using MojKatalog.Areas.Portfolio.Queries;
using MojKatalog.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MojKatalog.Controllers
{
    public class HomeController : Controller
    {
        private QKatalog _katalogDb;
        QPortfolio _portfolioDb = new QPortfolio();

        public HomeController()
        {
            _katalogDb = new QKatalog();
        }

        public ActionResult Index()
        {
            var model = _katalogDb.GetAllPublishedWebSites();
            return View(model);
        }

        public ActionResult PrikaziSajt(int Id)
        {
            var model = _portfolioDb.IzmeniPortfolioGet(Id);
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
