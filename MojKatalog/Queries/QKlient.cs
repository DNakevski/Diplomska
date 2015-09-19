using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using System.Data.Entity;
using MojKatalog.Helpers;

namespace MojKatalog.Queries
{
    public class QKlient
    {
        dbKatalogEntities _db;

        public QKlient()
        {
            _db = new dbKatalogEntities();
        }

        public QKlient(dbKatalogEntities db)
        {
            _db = db;
        }

        public List<Klienti> IzlistajKlientiZaPoedinec(int poedinecId)
        {
            var poedinec = _db.Poedinci
                .Include(x => x.Klienti)
                .Where(x => x.IdPoedinci == poedinecId)
                .FirstOrDefault();

            if (poedinec == null)
                return new List<Klienti>();

            return poedinec.Klienti.ToList();
        }

        public List<Klienti> IzlistajKlientiZaKompanija(int kompanijaId)
        {
            var kompanija = _db.Kompanii
                .Include(x => x.Klienti)
                .Where(x => x.IdKompanii == kompanijaId)
                .FirstOrDefault();

            if (kompanija == null)
                return new List<Klienti>();

            return kompanija.Klienti.ToList();
        }

        public void DodadiKlientZaPoedinec(Klienti klient, int poedinecId)
        {
            var poedinec = _db.Poedinci.Find(poedinecId);

            if (poedinec == null)
                return;//vakov poedinec ne e najden...treba da se hendla 

            poedinec.Klienti.Add(klient);
            _db.SaveChanges();
        }

        public void DodadiKlientZaKompanija(Klienti klient, int kompanijaId)
        {
            var kompanija = _db.Kompanii.Find(kompanijaId);

            if (kompanija == null)
                return;//vakva kompanija ne e najdena...treba da se hendla

            kompanija.Klienti.Add(klient);
            _db.SaveChanges();
        }

        public Klienti VratiKlient(int id)
        {
            return _db.Klienti.Find(id);      
        }
        public void IzmeniKlient(Klienti newKlient)
        {
            Klienti klient = _db.Klienti.Find(newKlient.IdKlienti);
            klient.Ime = newKlient.Ime;
            klient.Prezime = newKlient.Prezime;
            klient.NazivNaFirma = newKlient.NazivNaFirma;
            klient.Mail = newKlient.Mail;
            klient.Telefon = newKlient.Telefon;
            _db.SaveChanges();
        }
        public void IzbrisiKlient(int id)
        {
            Klienti klientDel = _db.Klienti.Find(id);
            _db.Klienti.Remove(klientDel);
            _db.SaveChanges();
        }
        public List<Klienti> ListaNaKlientiSporedId(int[] id)
        {
            List<Klienti> pomKlienti=_db.Klienti.Where(x => id.Contains(x.IdKlienti)).ToList();
            return _db.Klienti.Where(x => id.Contains(x.IdKlienti)).ToList();
        }

        
    }
}