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
    }
}