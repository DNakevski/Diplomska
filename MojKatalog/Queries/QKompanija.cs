using MojKatalog.Helpers;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Queries
{
    public class QKompanija
    {
        dbKatalogEntities _db = new dbKatalogEntities();

        public QKompanija()
        {
            _db = new dbKatalogEntities();
        }

        public QKompanija(dbKatalogEntities db)
        {
            this._db = db;
        }

        #region Logika za logiranje

        public LoggedInEntity GetLogiranaKompanija(string username, string password)
        {
            var kompanija = _db.Kompanii.Where(x => x.KorisnickoIme == username && x.Lozinka == password).FirstOrDefault();

            if (kompanija == null)
                return null;

            return MappingHelper.LoggedInEntityFromKompanija(kompanija);
        }
        #endregion
    }
}