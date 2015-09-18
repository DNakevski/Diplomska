using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;

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

        public MailPageViewModel GetPoraki()
        {
            //TODO: Treba da se napravi da gi zima porakite spored Poedinec/Kompanija

            var poraki = new MailPageViewModel();

            poraki.IsprateniPoraki = _db.Poraki.Where(x => x.IsSent == true && x.IsDeleted == false).ToList();
            poraki.IzbrishaniPoraki = _db.Poraki.Where(x => x.IsDeleted == true).ToList();
            poraki.SocuvaniPoraki = _db.Poraki.Where(x => x.IsSent == false && x.IsDeleted == false).ToList();
            return poraki;
        }

        public ViewPoraki InicijalizirajViewPoraki(int userId)
        {
            List<ViewKlienti> klienti = _db.Klienti.GroupBy(k => k.Ime.Substring(0, 1)).Select(kl => new ViewKlienti { 
                Karakter=kl.Key.ToUpper(),
                Klienti=kl.OrderBy(obj=>obj.Ime).ToList()
            }).ToList();

            return new ViewPoraki 
            { 
                Body = "",
                Subject = "",
                KlientiGrupirani = klienti
            };
        }
        public List<ViewKlienti> PrebarajKontakti(int userId, string searchString)
        {
             return _db.Klienti
                .Where(pr => pr.Ime.Contains(searchString))
                .GroupBy(k => k.Ime.Substring(0, 1))
                .Select(kl => new ViewKlienti
            {
                Karakter = kl.Key.ToUpper(),
                Klienti = kl.OrderBy(obj => obj.Ime).ToList()
            }).ToList();
           
          
        }
        public void IspratiISnimiPoraka(Poraki poraka)
        {
            string mailUser = "applicationclientsmail@gmail.com";
            string mailUserPwd = "applicationClientsPass";
            SmtpClient client = new SmtpClient("smtp.gmail.com");
            client.Port = 587;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            System.Net.NetworkCredential credentials = new System.Net.NetworkCredential(mailUser, mailUserPwd);
            client.EnableSsl = true;
            client.Credentials = credentials;

            //send the message to all the listed clients
            //foreach (Klienti klient in poraka.Klienti)
            //{
            //    MailMessage mail = new MailMessage(mailUser, klient.Mail);
            //    mail.Subject = poraka.Subject;
            //    mail.Body = poraka.Body;
            //    try
            //    {
            //        client.Send(mail);
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}

            //store the message to database
            SocuvajPoraka(poraka);
        }

        public void SocuvajPoraka(Poraki poraka)
        {
            _db.Poraki.Add(poraka);
            _db.SaveChanges();
        }

        public List<Poraki> GetIsprateniPoraki()
        {
            //TODO: ovoj treba da se napravi da raboti spored Poedinec/Kompanija
            return _db.Poraki.Where(x => x.IsSent == true && x.IsDeleted == false).ToList();
        }

        public List<Poraki> GetIzbrishaniPoraki()
        {
            //TODO: ovoj treba da se napravi da raboti spored Poedinec/Kompanija
            return _db.Poraki.Where(x => x.IsDeleted == true).ToList();
        }

        public List<Poraki> GetSocuvaniPoraki()
        {
            //TODO: ovoj treba da se napravi da raboti spored Poedinec/Kompanija
            return _db.Poraki.Where(x => x.IsSent == false && x.IsDeleted == false).ToList();
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
                .Where(x => x.IsSent == false && x.IsDeleted == false && porakiIds.Contains(x.IdPoraki))
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
    }
}