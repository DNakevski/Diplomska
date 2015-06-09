using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MojKatalog.Models;

namespace MojKatalog.Queries
{
    public class QKlient
    {
        dbKatalogEntities _db = new dbKatalogEntities();
        public List<Klienti> izlistaj()
        {
            return _db.Klienti.ToList();
        }
        public void dodadi(Klienti klient)
        {
            _db.Klienti.Add(klient);
            _db.SaveChanges();
        }
        public Klienti vratiKlient(int id)
        {
            return _db.Klienti.Find(id);      
        }
        public void izmeni(Klienti newKlient)
        {
            Klienti klient = _db.Klienti.Find(newKlient.IdKlienti);
            klient.Ime = newKlient.Ime;
            klient.Prezime = newKlient.Prezime;
            klient.NazivNaFirma = newKlient.NazivNaFirma;
            klient.Mail = newKlient.Mail;
            klient.Telefon = newKlient.Telefon;
            _db.SaveChanges();
        }
        public void izbrisi(int id)
        {
            Klienti klientDel = _db.Klienti.Find(id);
            _db.Klienti.Remove(klientDel);
            _db.SaveChanges();
        }
        public List<Klienti> listaNaKlientiSporedId(int[] id)
        {
            List<Klienti> pomKlienti=_db.Klienti.Where(x => id.Contains(x.IdKlienti)).ToList();
            return _db.Klienti.Where(x => id.Contains(x.IdKlienti)).ToList();
        }
    }
}