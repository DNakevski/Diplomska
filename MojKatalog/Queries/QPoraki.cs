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
        dbKatalogEntities _db = new dbKatalogEntities();

        public ViewPoraki inicijalizirajViewPoraki(int userId)
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
        public List<ViewKlienti> prebarajKontakti(int userId, string searchString)
        {
             return _db.Klienti.Where(pr => pr.Ime.Contains(searchString)).GroupBy(k => k.Ime.Substring(0, 1)).Select(kl => new ViewKlienti
            {
                Karakter = kl.Key.ToUpper(),
                Klienti = kl.OrderBy(obj => obj.Ime).ToList()
            }).ToList();
           
          
        }
        public void isprati(Poraki poraka)
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
            foreach (Klienti klient in poraka.Klienti)
            {
                MailMessage mail = new MailMessage(mailUser, klient.Mail);
                mail.Subject = poraka.Subject;
                mail.Body = poraka.Body;
                try
                {
                    client.Send(mail);
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}