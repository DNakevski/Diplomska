using MojKatalog.Helpers;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MojKatalog.Helpers.Exceptions;

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

        public bool RegisterPoedinec(RegisterModel poedinecModel)
        {
            try
            {
                Poedinci poedinec = new Poedinci
                {
                    Ime = poedinecModel.Ime,
                    Prezime = poedinecModel.Prezime,
                    KorisnickoIme = poedinecModel.KorisnickoIme,
                    Lozinka = poedinecModel.Password,
                    Mail = poedinecModel.Email,
                    Telefon = poedinecModel.Telefon
                };

                _db.Poedinci.Add(poedinec);
                _db.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool VerifyPoedinecRegistration(RegisterModel poedinecModel)
        {
            var poedinec = _db.Poedinci.FirstOrDefault(x => x.KorisnickoIme == poedinecModel.KorisnickoIme || x.Mail == poedinecModel.Email);

            if(poedinec != null)
            {
                if (poedinec.KorisnickoIme == poedinecModel.KorisnickoIme)
                    throw new ExistingUsernameException("Корисничкото име веќе постои");

                if (poedinec.Mail == poedinecModel.Email)
                    throw new ExistingEmailException("Е-Маил адресата веќе постои");

                return false;
            }

            return true;
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