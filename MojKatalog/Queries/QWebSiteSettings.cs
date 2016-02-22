using MojKatalog.Helpers.Enumerations;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Queries
{
    public class QWebSiteSettings
    {
        dbKatalogEntities _db = new dbKatalogEntities();
        public List<KataloziWebSiteViewModel> KataloziWebSiteList(int id, LogedUserTypeEnum userType)
        {
            List<KataloziWebSiteViewModel> lista = new List<KataloziWebSiteViewModel>();
            if (userType == LogedUserTypeEnum.Poedinec)
            {
                lista = _db.Katalozi
                .Where(x => x.IdPoedinci == id)
                .Select(x => new KataloziWebSiteViewModel
                {
                    IdKatalozi = x.IdKatalozi,
                    HasWebSite = (x.WebSiteSettings == null) ? false : true,
                    Naziv = (x.WebSiteSettings == null) ? "" : x.WebSiteSettings.Naziv,
                    NazivNaKatalog = x.NazivNaKatalog,
                    Objaven = (x.WebSiteSettings == null) ? false : x.WebSiteSettings.Objaven,
                    DatumObjaven = (x.WebSiteSettings == null) ? null : x.WebSiteSettings.DatumObjaven
                    }).ToList();
            }
            else
            {

                    lista = _db.Katalozi
                    .Where(x=>x.IdKompanii==id)
                    .Select(x => new KataloziWebSiteViewModel 
                    {
                        IdKatalozi=x.IdKatalozi,
                        HasWebSite = (x.WebSiteSettings == null) ? false : true,
                        Naziv = (x.WebSiteSettings == null) ? "" : x.WebSiteSettings.Naziv, 
                        NazivNaKatalog=x.NazivNaKatalog,
                        Objaven = (x.WebSiteSettings == null) ? false : x.WebSiteSettings.Objaven,
                        DatumObjaven = (x.WebSiteSettings == null) ? null : x.WebSiteSettings.DatumObjaven
                    }).ToList();
            }
            return lista.ToList();
             
        } 

        public bool PublishWebSite(int katalogId)
        {
            try
            {
                var settings = _db.WebSiteSettings.FirstOrDefault(x => x.IdKatalozi == katalogId);
                if(settings != null)
                {
                    settings.Objaven = true;
                    settings.DatumObjaven = DateTime.Now;
                    _db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool UnPublishWebSite(int katalogId)
        {
            try
            {
                var settings = _db.WebSiteSettings.FirstOrDefault(x => x.IdKatalozi == katalogId);
                if (settings != null)
                {
                    settings.Objaven = false;
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool DeleteWebSajt(int siteId)
        {
            try
            {
                var item = _db.WebSiteSettings.FirstOrDefault(x => x.IdKatalozi == siteId);
                if (item != null)
                {
                    _db.WebSiteSettings.Remove(item);
                    _db.SaveChanges();
                   
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            

            return true;
        }
    }
}