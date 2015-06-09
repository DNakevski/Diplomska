using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;

namespace MojKatalog.Queries
{
    public class QProizvod
    {
        dbKatalogEntities _db = new dbKatalogEntities();
        QKategorija kategorijaQ = new QKategorija();
        public List<Proizvodi> izlistaj() 
        {
            return _db.Proizvodi.ToList();
        }
        public List<Proizvodi> izlistajSporedIdKategorii(int idKategorii) 
        {
            return _db.Proizvodi.Where(x => x.IdKategorii == idKategorii).ToList();
        }
        public List<ViewProizvodiKategorii> izlistajProizvodiKategorii(int roditelId)
        {
            List<ViewProizvodiKategorii> list = new List<ViewProizvodiKategorii>();
            List<Kategorii> kategorii = kategorijaQ.izlistajSporedRoditelId(roditelId);
            foreach(Kategorii item in kategorii)
            {
                list.Add(
                    new ViewProizvodiKategorii()
                    { 
                        MVKategorija=item,
                        MVVkupnoPodkategorii=kategorijaQ.presmetajKategoriiSporedId(item.IdKategorii),
                        MVVkupnoProizvodi = kategorijaQ.presmetajProizvodiSporedId(item.IdKategorii)
                    }
                );
            }
            return list;
        }
        public void dodadi(Proizvodi proizvod)
        {
            _db.Proizvodi.Add(proizvod);
            _db.SaveChanges();
        }
        public void izmeniPateka(string pateka,Proizvodi newProizvod)
        {
            Proizvodi proizvod = _db.Proizvodi.Find(newProizvod.IdProizvodi);
            proizvod.SlikaNaProizvod = pateka;
            _db.SaveChanges();
        }
        public void izmeni(Proizvodi newProizvod)
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
        public Proizvodi vratiProizvod(int id)
        {
            return _db.Proizvodi.Find(id);
        }
    }
}