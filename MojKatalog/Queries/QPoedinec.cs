using MojKatalog.Helpers;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Queries
{
    public class QPoedinec
    {
        dbKatalogEntities _db;

        public QPoedinec()
        {
            _db = new dbKatalogEntities();
        }

        public QPoedinec(dbKatalogEntities db)
        {
            this._db = db;
        }

        #region Logika za logiranje

        public LoggedInEntity GetLogiranPoedinec(string username, string password)
        {
            var poedinec = _db.Poedinci.Where(x => x.KorisnickoIme == username && x.Lozinka == password).FirstOrDefault();

            if (poedinec == null)
                return null;

            return MappingHelper.LoggedInEntityFromKlient(poedinec);
        }
        #endregion
    }
}