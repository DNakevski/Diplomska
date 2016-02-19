using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using MojKatalog.Helpers.Enumerations;

namespace MojKatalog.Queries
{
    public class QProizvod
    {
        dbKatalogEntities _db = new dbKatalogEntities();
        QKategorija kategorijaQ = new QKategorija();
        public List<Proizvodi> IzlistajProizvodi() 
        {
            return _db.Proizvodi.ToList();
        }

        public List<Proizvodi> IzlistajSporedIdKategorii(int idKategorii) 
        {
            return _db.Proizvodi.Where(x => x.IdKategorii == idKategorii).ToList();
        }

        public List<ViewProizvodiKategorii> IzlistajProizvodiKategorii(int roditelId, int id, LogedUserTypeEnum userType)
        {
            List<ViewProizvodiKategorii> list = new List<ViewProizvodiKategorii>();
            List<Kategorii> kategorii = kategorijaQ.IzlistajSporedRoditelId(roditelId, id, userType);
            foreach(Kategorii item in kategorii)
            {
                list.Add(
                    new ViewProizvodiKategorii()
                    { 
                        MVKategorija=item,
                        MVVkupnoPodkategorii=kategorijaQ.PresmetajKategoriiSporedId(item.IdKategorii),
                        MVVkupnoProizvodi = kategorijaQ.PresmetajProizvodiSporedId(item.IdKategorii)
                    }
                );
            }
            return list;
        }
        public void DodadiProizvod(Proizvodi proizvod)
        {
            _db.Proizvodi.Add(proizvod);
            _db.SaveChanges();
        }
        public void IzmeniPateka(string pateka,Proizvodi newProizvod)
        {
            Proizvodi proizvod = _db.Proizvodi.Find(newProizvod.IdProizvodi);
            proizvod.SlikaNaProizvod = pateka;
            _db.SaveChanges();
        }
        public void IzmeniProizvod(ProizvodViewModel newProizvod)
        {
            Proizvodi proizvod = _db.Proizvodi.Find(newProizvod.IdProizvodi);
            proizvod.NazivNaProizvod = newProizvod.NazivNaProizvod;
            proizvod.Specifikacija = newProizvod.Specifikacija;
            proizvod.Cena = newProizvod.Cena;
            proizvod.Popust = newProizvod.Popust;
            proizvod.Dostapnost = newProizvod.Dostapnost;
            proizvod.SlikaNaProizvod = newProizvod.SlikaNaProizvod;

            _db.SaveChanges();
        }
        public Proizvodi VratiProizvod(int id)
        {
            return _db.Proizvodi.Find(id);
        }

        public ProizvodViewModel VratiProizvodViewModel(int id)
        {
            var proizvod = _db.Proizvodi.Find(id);
            if (proizvod == null)
                return new ProizvodViewModel();

            return new ProizvodViewModel
            {
                IdProizvodi = proizvod.IdProizvodi,
                Cena = proizvod.Cena,
                Dostapnost = proizvod.Dostapnost,
                IdKategorii = proizvod.IdKategorii,
                NazivNaProizvod = proizvod.NazivNaProizvod,
                Popust = proizvod.Popust,
                SlikaNaProizvod = proizvod.SlikaNaProizvod,
                Specifikacija = proizvod.Specifikacija
            };
        }
        public void IzbrisiProizvod(int id)
        {
            Proizvodi proizvod = _db.Proizvodi.Find(id);
            _db.Proizvodi.Remove(proizvod);
            _db.SaveChanges();
        }


    }
}