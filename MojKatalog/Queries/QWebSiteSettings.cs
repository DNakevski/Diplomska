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
                    .Where(x=>x.IdPoedinci==id)
                    .Select(x => new KataloziWebSiteViewModel
                    {
                        IdKatalozi = x.IdKatalozi,
                        HasWebSite = (x.WebSiteSettings==null) ? false:true,
                        Naziv = (x.WebSiteSettings == null) ? "" : x.WebSiteSettings.Naziv,
                        NazivNaKatalog = x.NazivNaKatalog
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
                        NazivNaKatalog=x.NazivNaKatalog
                    }).ToList();
            }
            return lista.ToList();
             
        } 
    }
}