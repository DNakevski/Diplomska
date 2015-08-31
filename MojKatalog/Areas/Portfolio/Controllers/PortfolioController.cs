using MojKatalog.Areas.Portfolio.Queries;
using MojKatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MojKatalog.Areas.Portfolio.Controllers
{
    public class PortfolioController : Controller
    {
        QPortfolio model = new QPortfolio();
        // GET: Portfolio/Portfolio
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DodadiPortfolio(int id) 
        {
            WebSiteSettings portfolioSettings = new WebSiteSettings
            {
                IdKatalozi = id,
                FontFamily = "OpenSans-Regular",
                FontColor1 = "darkBlueDarker",
                FontColor2 = "greenLighter",
                BGPocetna = "~/Areas/Portfolio/Images/HomeBackgroundImage.jpg",
                BGZaNas = "white",
                BGFZaNas = "~/Areas/Portfolio/Images/AboutFooter.jpg",
                BGPortfolio = "white",
                BGFPortfolio = "~/Areas/Portfolio/Images/AboutFooter.jpg",
                BGContact = "lightGreyLighter",
                BGMenu = "lightGreyLighter",
                BGFooter = "lightGreyLighter"
            };
            //Insert vo tabela WebSiteSettings so default vrednosti
            model.DodadiPortfolio(portfolioSettings);
            return View();
        }
        [HttpGet]
        public ActionResult IzmeniPortfolio(int id) 
        {
            WebSiteSettings portfolioSettings = model.IzmeniPortfolioGet(id);
            //Editiranje na vrednosti vo tabela WebSiteSettings
            return View(portfolioSettings);
        }
        [HttpPost]
        public ActionResult IzmeniPortfolio(WebSiteSettings wsettings)
        {
            model.IzmeniPortfolioPost(wsettings);
            WebSiteSettings portfolioSettings = model.IzmeniPortfolioGet(wsettings.IdKatalozi);
            return View(portfolioSettings);
        }

        public ActionResult IzbrisiPortfolio()
        {
            //Brisenje na vrednosti od tabela WebSiteSettings
            return View();

        }

        public ActionResult PregledajPortfolio() 
        {
            //Pregled na website bez edit funkcionalnosti
            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}