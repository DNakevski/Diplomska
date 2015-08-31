using MojKatalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Areas.Portfolio.Queries 
{
    
    public class QPortfolio
    {
        dbKatalogEntities _db = new dbKatalogEntities();

        public void DodadiPortfolio(WebSiteSettings portfolio)
        {
            _db.WebSiteSettings.Add(portfolio);
            _db.SaveChanges();
        }
        public WebSiteSettings IzmeniPortfolioGet(int idKatalog)
        {
            return _db.WebSiteSettings.Find(idKatalog);
        }
        public void IzmeniPortfolioPost(WebSiteSettings newWSettings)
        {
            WebSiteSettings wsettings = _db.WebSiteSettings.Find(newWSettings.IdKatalozi);
            wsettings.Naziv = newWSettings.Naziv;
            wsettings.FontFamily = newWSettings.FontFamily;
            wsettings.FontColor1 = newWSettings.FontColor1;
            wsettings.FontColor2 = newWSettings.FontColor2;
            wsettings.BGPocetna = newWSettings.BGPocetna;
            wsettings.BGZaNas = newWSettings.BGZaNas;
            wsettings.BGFZaNas = newWSettings.BGFZaNas;
            wsettings.BGPortfolio = newWSettings.BGPortfolio;
            wsettings.BGFPortfolio = newWSettings.BGFPortfolio;
            wsettings.BGContact = newWSettings.BGContact;
            wsettings.BGMenu = newWSettings.BGMenu;
            wsettings.BGFooter = newWSettings.BGFooter;
            _db.SaveChanges();

        }
    }
}