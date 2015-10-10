using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using MojKatalog.Helpers.Enumerations;
using System.Data.Entity;

namespace MojKatalog.Queries
{
    public class QPoraki
    {
        private dbKatalogEntities _db;

        public QPoraki()
        {
            _db = new dbKatalogEntities();
        }

        public QPoraki(dbKatalogEntities db)
        {
            _db = db;
        }

        public MailPageViewModel GetPoraki(int userId, LogedUserTypeEnum userType)
        {
            var poraki = new MailPageViewModel();

            if(userType == LogedUserTypeEnum.Poedinec)
            {
                poraki.IsprateniPoraki = _db.Poraki.Where(x => x.IdPoedinci == userId && x.IsSent == true && x.IsDeleted == false && x.IsReceived == false).ToList();
                poraki.IzbrishaniPoraki = _db.Poraki.Where(x => x.IdPoedinci == userId && x.IsDeleted == true).ToList();
                poraki.SocuvaniPoraki = _db.Poraki.Where(x => x.IdPoedinci == userId && x.IsSent == false && x.IsDeleted == false && x.IsReceived == false).ToList();
            }
            else if(userType == LogedUserTypeEnum.Kompanija)
            {
                poraki.IsprateniPoraki = _db.Poraki.Where(x => x.IdKompanii == userId && x.IsSent == true && x.IsDeleted == false && x.IsReceived == false).ToList();
                poraki.IzbrishaniPoraki = _db.Poraki.Where(x => x.IdKompanii == userId && x.IsDeleted == true).ToList();
                poraki.SocuvaniPoraki = _db.Poraki.Where(x => x.IdKompanii == userId && x.IsSent == false && x.IsDeleted == false && x.IsReceived == false).ToList();
            }
            
            return poraki;
        }

        public ViewPoraki InicijalizirajViewPoraki(int userId, LogedUserTypeEnum userType)
        {
            List<ViewKlienti> klienti = new List<ViewKlienti>();

            if(userType == LogedUserTypeEnum.Poedinec)
            {
                var user = _db.Poedinci.FirstOrDefault(x => x.IdPoedinci == userId);
                if(user != null)
                {
                    klienti = user.Klienti.GroupBy(k => k.Ime.Substring(0, 1)).Select(kl => new ViewKlienti
                    {
                        Karakter = kl.Key.ToUpper(),
                        Klienti = kl.OrderBy(obj => obj.Ime).ToList()
                    }).ToList();
                }
                
            }
            else if(userType == LogedUserTypeEnum.Kompanija)
            {
                var kompanija = _db.Kompanii.FirstOrDefault(x => x.IdKompanii == userId);
                if(kompanija != null)
                {
                    klienti = kompanija.Klienti.GroupBy(k => k.Ime.Substring(0, 1)).Select(kl => new ViewKlienti
                    {
                        Karakter = kl.Key.ToUpper(),
                        Klienti = kl.OrderBy(obj => obj.Ime).ToList()
                    }).ToList();
                }
            }
            
            return new ViewPoraki 
            { 
                Body = "",
                Subject = "",
                KlientiGrupirani = klienti
            };
        }

        public List<ViewKlienti> PrebarajKontaktiZaPoedinec(int userId, string searchString)
        {
            searchString = searchString.ToLower();
            List<ViewKlienti> klienti = new List<ViewKlienti>();

            var poedinec = _db.Poedinci.Include(x => x.Klienti).FirstOrDefault(x => x.IdPoedinci == userId);
            if(poedinec != null)
            {
                klienti = poedinec.Klienti
                .Where(pr => pr.Ime.ToLower().Contains(searchString))
                .GroupBy(k => k.Ime.Substring(0, 1))
                .Select(kl => new ViewKlienti
                {
                    Karakter = kl.Key.ToUpper(),
                    Klienti = kl.OrderBy(obj => obj.Ime).ToList()
                }).ToList();
            }

            return klienti;
        }

        public List<ViewKlienti> PrebarajKontaktiZaKompanija(int kompanijaId, string searchString)
        {
            searchString = searchString.ToLower();
            List<ViewKlienti> klienti = new List<ViewKlienti>();

            var kompanija = _db.Kompanii.Include(x => x.Klienti).FirstOrDefault(x => x.IdKompanii == kompanijaId);
            if(kompanija != null)
            {
                klienti = kompanija.Klienti
               .Where(pr => pr.Ime.ToLower().Contains(searchString))
               .GroupBy(k => k.Ime.Substring(0, 1))
               .Select(kl => new ViewKlienti
               {
                   Karakter = kl.Key.ToUpper(),
                   Klienti = kl.OrderBy(obj => obj.Ime).ToList()
               }).ToList();
            }
            return klienti;
        }


        public void IspratiISnimiPoraka(Poraki poraka)
        {
            string mailUser = "applicationclientsmail@gmail.com";
            string mailUserPwd = "applicationClientsPass";
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(mailUser, mailUserPwd);
            client.EnableSsl = true;
            client.Credentials = credentials;

            foreach (Klienti klient in poraka.Klienti)
            {
                MailMessage mail = new MailMessage(mailUser, klient.Mail);
                mail.Subject = poraka.Subject;
                mail.Body = poraka.Body;
                try
                {
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            SocuvajPoraka(poraka);
        }

        public void SocuvajPoraka(Poraki poraka)
        {
            _db.Poraki.Add(poraka);
            _db.SaveChanges();
        }

        public List<Poraki> GetIsprateniPoraki(int userId, LogedUserTypeEnum userType)
        {
            List<Poraki> poraki = new List<Poraki>();

            if(userType == LogedUserTypeEnum.Poedinec)
            {
                poraki = _db.Poraki
                    .Include(x => x.Klienti)
                    .Where(x => x.IdPoedinci == userId && x.IsSent == true && x.IsDeleted == false && x.IsReceived == false)
                    .ToList();
            }
            else if(userType == LogedUserTypeEnum.Kompanija)
            {
                poraki = _db.Poraki
                    .Include(x => x.Klienti)
                    .Where(x => x.IdKompanii == userId && x.IsSent == true && x.IsDeleted == false && x.IsReceived == false)
                    .ToList();
            } 

            return poraki;
        }

        public List<Poraki> GetIzbrishaniPoraki(int userId, LogedUserTypeEnum userType)
        {
            var poraki = new List<Poraki>();
            if(userType == LogedUserTypeEnum.Poedinec)
            {
                poraki = _db.Poraki
                    .Include(x => x.Klienti)
                    .Where(x => x.IdPoedinci == userId && x.IsDeleted == true)
                    .ToList();
            }
            else if(userType == LogedUserTypeEnum.Kompanija)
            {
                poraki = _db.Poraki
                    .Include(x => x.Klienti)
                    .Where(x => x.IdKompanii == userId && x.IsDeleted == true)
                    .ToList();
            }

            return poraki;
        }

        public List<Poraki> GetSocuvaniPoraki(int userId, LogedUserTypeEnum userType)
        {
            var poraki = new List<Poraki>();
            if(userType == LogedUserTypeEnum.Poedinec)
            {
                poraki = _db.Poraki.Where(x => x.IdPoedinci == userId && x.IsSent == false && x.IsDeleted == false && x.IsReceived == false).ToList();
            }
            else if(userType == LogedUserTypeEnum.Kompanija)
            {
                poraki = _db.Poraki.Where(x => x.IdKompanii == userId && x.IsSent == false && x.IsDeleted == false && x.IsReceived == false).ToList();
            }
            

            return poraki;
        }

        public List<Poraki> GetPrimeniPoraki(int userId, LogedUserTypeEnum userType)
        {
            var poraki = new List<Poraki>();
            if (userType == LogedUserTypeEnum.Poedinec)
            {
                poraki = _db.Poraki.Where(x => x.IdPoedinci == userId && x.IsReceived == true).ToList();
            }
            else if (userType == LogedUserTypeEnum.Kompanija)
            {
                poraki = _db.Poraki.Where(x => x.IdKompanii == userId && x.IsReceived == true).ToList();
            }


            return poraki;
        }

        public bool DeleteIsprateniPoraki(List<int> porakiIds)
        {
            _db.Poraki
                .Where(x => x.IsSent == true && x.IsDeleted == false && porakiIds.Contains(x.IdPoraki))
                .ToList()
                .ForEach(x => x.IsDeleted = true);

            try
            {
                _db.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool DeleteIzbrishaniPoraki(List<int> porakiIds)
        {
            var poraki = _db.Poraki
                .Where(x => x.IsDeleted == true && porakiIds.Contains(x.IdPoraki))
                .ToList();

            _db.Poraki.RemoveRange(poraki);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool DeleteSocuvaniPoraki(List<int> porakiIds)
        {
            _db.Poraki
                .Where(x => x.IsSent == false && x.IsDeleted == false && x.IsReceived == false && porakiIds.Contains(x.IdPoraki))
                .ToList()
                .ForEach(x => x.IsDeleted = true);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool DeletePrimeniPoraki(List<int> porakiIds)
        {
            _db.Poraki
                .Where(x => x.IsReceived == true && porakiIds.Contains(x.IdPoraki))
                .ToList()
                .ForEach(x => x.IsDeleted = true);

            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
        public Poraki VratiPorakaSporedId(int idPoraka)
        {
            return _db.Poraki.Find(idPoraka);
        }
    }
}