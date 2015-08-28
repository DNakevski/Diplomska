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
            WebSiteSettings portfolioSettings = new WebSiteSettings();
            portfolioSettings.IdKatalozi = id;
            portfolioSettings.FontFamily = "OpenSans-Regular";
            portfolioSettings.FontColor1 = "darkBlueDarker";
            portfolioSettings.FontColor2 = "greenLighter";
            portfolioSettings.BGPocetna = "/Areas/Portfolio/Images/HomeBackgroundImage.jpg";
            portfolioSettings.BGZaNas = "#FFFFFF";
            portfolioSettings.BGFZaNas = "/Areas/Portfolio/Images/AboutFooter.jpg";
            portfolioSettings.BGPortfolio = "#FFFFFF";
            portfolioSettings.BGFPortfolio = "/Areas/Portfolio/Images/AboutFooter.jpg";
            portfolioSettings.BGContact = "#ECF0F1";
            portfolioSettings.BGMenu = "#f7f9f7";
            portfolioSettings.BGFooter = "#ECF0F1";
            portfolioSettings.Naziv = "Назив на Веб Сајтот";
            /*WebSiteSettings{IdKatalozi= id; FontFamily= "OpenSans-Regular"; 
                                                     FontColor1= "darkBlueDarker"; 
                                                     FontColor2= "greenLighter";
                                                     BGPocetna= "~/Areas/Portfolio/Images/HomeBackgroundImage.jpg";
                                                     BGZaNas= "#FFFFFF";
                                                     BGFZaNas= "~/Areas/Portfolio/Images/AboutFooter.jpg";
                                                     BGPortfolio= "#FFFFFF";
                                                     BGFPortfolio= "~/Areas/Portfolio/Images/AboutFooter.jpg";
                                                     BGContact= "#ECF0F1";
                                                     BGMenu= "#f7f9f7";
                                                     BGFooter= "#ECF0F1"};*/
            //Insert vo tabela WebSiteSettings so default vrednosti
            model.DodadiPortfolio(portfolioSettings);
            return View();
        }
        public ActionResult IzmeniPortfolio(int id) 
        {
            WebSiteSettings portfolioSettings = model.IzmeniPortfolioGet(id);
            //Editiranje na vrednosti vo tabela WebSiteSettings
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