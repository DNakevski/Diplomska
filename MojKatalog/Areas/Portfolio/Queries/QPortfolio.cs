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
         
            wsettings.BGPocetna = (newWSettings.BGPocetna != null) ? newWSettings.BGPocetna : wsettings.BGPocetna;
            wsettings.BGZaNas = (newWSettings.BGZaNas != null) ? newWSettings.BGZaNas : wsettings.BGZaNas;
            wsettings.BGFZaNas = (newWSettings.BGFZaNas != null) ? newWSettings.BGFZaNas : wsettings.BGFZaNas;
            wsettings.BGPortfolio = (newWSettings.BGPortfolio != null) ? newWSettings.BGPortfolio : wsettings.BGPortfolio;
            wsettings.BGFPortfolio = (newWSettings.BGFPortfolio != null) ? newWSettings.BGFPortfolio : wsettings.BGFPortfolio;
            wsettings.BGContact = (newWSettings.BGContact != null) ? newWSettings.BGContact : wsettings.BGContact;
            wsettings.BGMenu = (newWSettings.BGMenu != null) ? newWSettings.BGMenu : wsettings.BGMenu;
            wsettings.BGFooter = (newWSettings.BGFooter != null) ? newWSettings.BGFooter : wsettings.BGFooter; 
            _db.SaveChanges();

        }
    }
}