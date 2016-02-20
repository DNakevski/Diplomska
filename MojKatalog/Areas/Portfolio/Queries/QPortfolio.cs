using MojKatalog.Helpers.Enumerations;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Areas.Portfolio.Queries 
{
    
    public class QPortfolio
    {
        dbKatalogEntities _db = new dbKatalogEntities();

        public void DodadiPortfolio(int idKatalog, int idKorisnik, LogedUserTypeEnum userType)
        {
            string nazivNaWebSite = "";
           
            if (userType == Helpers.Enumerations.LogedUserTypeEnum.Poedinec)
            {
                Poedinci poedinec = _db.Poedinci.Find(idKorisnik);
                nazivNaWebSite = poedinec.Ime + " " + poedinec.Prezime;
            }
            else
            {
                Kompanii kompanija = _db.Kompanii.Find(idKorisnik);
                nazivNaWebSite = kompanija.NazivNaKompanija;
            }
            WebSiteSettings wsettings = new WebSiteSettings
            {
                IdKatalozi = idKatalog,
                FontFamily = "OpenSans-Regular",
                FontColor1 = "darkBlueDarker",
                FontColor2 = "greenLighter",
                BGPocetna = "/Areas/Portfolio/Images/HomeBackgroundImage.jpg",
                BGZaNas = "white",
                BGFZaNas = "/Areas/Portfolio/Images/AboutFooter.jpg",
                BGPortfolio = "white",
                BGFPortfolio = "/Areas/Portfolio/Images/AboutFooter.jpg",
                BGContact = "lightGreyLighter",
                BGMenu = "lightGreyLighter",
                BGFooter = "lightGreyLighter",
                Naziv = nazivNaWebSite,
                SodrzinaZaNasF = "The details are not details , they make the design",
                SodrzinaPortfolioF = "The details are not details , they make the design"
            };
            _db.WebSiteSettings.Add(wsettings);
            _db.SaveChanges();
        }

        public WebSiteSettings GetWsettings(int id)
        {
            return _db.WebSiteSettings.Find(id);
        }
        public WSettingsKatalogKorisnikViewModel IzmeniPortfolioGet(int idKatalog)
        {
            WSettingsKatalogKorisnikViewModel vmodel = new WSettingsKatalogKorisnikViewModel();
            vmodel.Katalog = _db.Katalozi.Find(idKatalog);
            vmodel.Katalog.Kategorii = vmodel.Katalog.Kategorii.Where(x => x.RoditelId == null).ToList();
            vmodel.WSettings = vmodel.Katalog.WebSiteSettings;
            vmodel.Poedinec = vmodel.Katalog.Poedinci;
            vmodel.Kompanija = vmodel.Katalog.Kompanii;
            return vmodel;
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
            wsettings.SodrzinaPortfolioF = newWSettings.SodrzinaPortfolioF;
            wsettings.SodrzinaZaNasF = newWSettings.SodrzinaZaNasF;
            _db.SaveChanges();

        }
        public List<Kategorii> IzlistajKategoriiSporedParentId(int? parentId, int? katalogId)
        {
            List<Kategorii> pom = _db.Kategorii.Where(x => x.RoditelId == parentId && x.IdKatalozi == katalogId).ToList();
            return pom;
        }
        public Kategorii IzlistajKategorijaSporedKategoriiId(int kategoriiId)
        {
            return _db.Kategorii.Find(kategoriiId);
        }
        public void SocuvajPoraka(Poraki poraka)
        {
            _db.Poraki.Add(poraka);
            _db.SaveChanges();
        }
    }
}