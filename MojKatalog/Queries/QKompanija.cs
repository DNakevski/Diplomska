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

        public bool RegisterKompanija(RegisterKompanijaModel model, HttpPostedFileBase logo)
        {
            var kompanija = new Kompanii
            {
                KorisnickoIme = model.KorisnickoIme,
                Lozinka = model.Password,
                Mail = model.Email,
                NazivNaKompanija = model.Naziv,
                Telefon = model.Telefon
            };

            _db.Kompanii.Add(kompanija);
            _db.SaveChanges();

            //save the image if is provided
            if (logo != null)
            {
                string[] allowed = { ".jpg", ".jpeg", ".png", ".gif" };
                string extension = System.IO.Path.GetExtension(logo.FileName);

                if (allowed.Contains(extension.ToLower()))
                {
                    string CoverPath = "/Images/CompanyImages/Logos/"; //TODO: patekata treba da se zima od web.cofig
                    string imageName = "Logo_" + kompanija.IdKompanii + extension;
                    string NewLocation = HttpContext.Current.Server.MapPath("~") + CoverPath + imageName;
                    string tip = logo.GetType().ToString();
                    logo.SaveAs(NewLocation);
                    string path = CoverPath + imageName;

                    kompanija.LogoNaKompanija = path;
                    _db.SaveChanges();
                }
            }

            return true;
        }

        public bool VerifyKompanijaRegistration(RegisterKompanijaModel model)
        {
            var kompanija = _db.Kompanii.FirstOrDefault(x => x.KorisnickoIme == model.KorisnickoIme || x.Mail == model.Email);
            if(kompanija != null)
            {
                if (kompanija.KorisnickoIme == model.KorisnickoIme)
                    throw new ExistingUsernameException("Корисничкото име веќе постои");

                if (kompanija.Mail == model.Email)
                    throw new ExistingEmailException("Е-Маил адресате веќе постои");

                return false;
            }

            return true;
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

        public Kompanii GetKompanija(int id)
        {
            return _db.Kompanii.Find(id);
        }

        public bool ChangePassword(int kompanijaId, string newPassword)
        {
            var kompanija = _db.Kompanii.Find(kompanijaId);
            if (kompanija == null)
            {
                throw new Exception("Корисникот не постои");
            }

            kompanija.Lozinka = newPassword;

            try
            {
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void SetKompanija(Kompanii newKompanija)
        {
            Kompanii kompanija = _db.Kompanii.Find(newKompanija.IdKompanii);
            kompanija.NazivNaKompanija = newKompanija.NazivNaKompanija;
            kompanija.KorisnickoIme = newKompanija.KorisnickoIme;
            kompanija.Lozinka = newKompanija.Lozinka;
            kompanija.OpisNaKompanija = newKompanija.OpisNaKompanija;
            kompanija.Mail = newKompanija.Mail;

            _db.SaveChanges();
        }
    }
}