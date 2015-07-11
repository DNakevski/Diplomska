using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using MojKatalog.Helpers.Enumerations;
using System.Data.Entity;

namespace MojKatalog.Queries
{
    public class TreeViewModel
    {
        public string id { get; set; }
        public string parent { get; set; }
        public string text { get; set; }

    }
    public class KatalogViewModel 
    {
        public int? IdKatalog { get; set; }
        public string NazivNaKatalog { get; set; }
        public IEnumerable<TreeViewModel> Trees { get; set; }
    }

   
    public class QKategorija
    {
        dbKatalogEntities _db = new dbKatalogEntities();

        public int DodadiKategorija(Kategorii kategorija) {
            _db.Kategorii.Add(kategorija);

            _db.SaveChanges();
            return kategorija.IdKategorii;
        }

        public void IzmeniKategorija(int id, string naziv)
        {
            Kategorii kategorija=_db.Kategorii.Find(id);
            kategorija.NazivNaKategorija = naziv;

            _db.SaveChanges();
        }

        private void DeleteChildCategories(Kategorii category)
        {
            foreach (Kategorii subCategory in category.Kategorii1.ToList())
            {
                if (subCategory.Kategorii1.Count() > 0)
                {
                    DeleteChildCategories(subCategory);
                }
                else
                {
                    _db.Kategorii.Remove(subCategory);
                }
            }
            _db.Kategorii.Remove(category);
        }

        public void IzbrisiKategorija(int id)
        {
            Kategorii kategorija = _db.Kategorii.Find(id);
            DeleteChildCategories(kategorija);
            _db.SaveChanges();
        }

        public List<KatalogViewModel> IzlistajKatalozi(int id, LogedUserTypeEnum userType) 
        {
            int [] distinctKataloziId = AllKataloziId(id, userType);//distinctKatalozi();
            int kataloziIdLength = distinctKataloziId.Length;
            var katalozi = new List<KatalogViewModel>();
            for (int i = 0; i < kataloziIdLength; i++) {
                int ? katalogId = distinctKataloziId[i];
                katalozi.Add(new KatalogViewModel { 
                    IdKatalog = katalogId,
                    NazivNaKatalog=_db.Katalozi.Find(katalogId).NazivNaKatalog,
                    Trees = _db.Kategorii
                        .Where(x => x.IdKatalozi == katalogId)
                        .Select(x => new TreeViewModel
                        {
                            id = x.IdKategorii.ToString(),
                            parent = (x.RoditelId == null) ? "#" : x.RoditelId.ToString(),
                            text = x.NazivNaKategorija
                        })
                        as IEnumerable<TreeViewModel>
                });
            }
            return katalozi;
        }

        public KatalogViewModel IzlistajSporedId(int id) {
            var katalog = _db.Katalozi.Include(x => x.Kategorii).Where(x => x.IdKatalozi == id).FirstOrDefault();
            if (katalog == null)
                return new KatalogViewModel();

            var katalogViewModel= new KatalogViewModel
            {
                IdKatalog = id,
                NazivNaKatalog = katalog.NazivNaKatalog,
                Trees = katalog.Kategorii
                   .Select(x => new TreeViewModel
                   {
                       id = x.IdKategorii.ToString(),
                       parent = (x.RoditelId == null) ? "#" : x.RoditelId.ToString(),
                       text = x.NazivNaKategorija
                   })
                   .ToList()
            };
            return katalogViewModel;
        }
        public int?[] DistinctKatalozi()
        {
            return _db.Kategorii.Select(x => x.IdKatalozi).Distinct().ToArray();
        }

        public int[] AllKataloziId(int id, LogedUserTypeEnum userType)
        {
            if(userType == LogedUserTypeEnum.Poedinec)
                return _db.Katalozi.Where(x => x.IdPoedinci == id).Select(x => x.IdKatalozi).ToArray();
            else
                return _db.Katalozi.Where(x => x.IdKompanii == id).Select(x => x.IdKatalozi).ToArray();
        }
        public List<Kategorii> IzlistajSporedRoditelId(int roditelId, int id, LogedUserTypeEnum userType)
        {
            if (roditelId == 0)
            {
                if(userType == LogedUserTypeEnum.Poedinec)
                    return _db.Kategorii.Include(x => x.Katalozi).Where(x => x.RoditelId == null && x.Katalozi.IdPoedinci == id).ToList();
                else
                    return _db.Kategorii.Include(x => x.Katalozi).Where(x => x.RoditelId == null && x.Katalozi.IdKompanii == id).ToList();
            }
            else
            {
                if(userType == LogedUserTypeEnum.Poedinec)
                    return _db.Kategorii.Include(x => x.Katalozi).Where(x => x.RoditelId == roditelId && x.Katalozi.IdPoedinci == id).ToList();
                else
                    return _db.Kategorii.Include(x => x.Katalozi).Where(x => x.RoditelId == roditelId && x.Katalozi.IdKompanii == id).ToList();

            }
           
        }

        public int PresmetajKategoriiSporedId(int idKategorija)
        {
            int vkupno = 0;
            var kategorija = _db.Kategorii.Find(idKategorija);
            if (kategorija != null)
            {
                vkupno = VkupnoPodkategorii(kategorija);
            }

            return vkupno;
        }
        private int VkupnoPodkategorii(Kategorii kategorija) 
        {
            int vkupno = 0;
            foreach (Kategorii podkategorija in kategorija.Kategorii1.ToList())
            {

                vkupno += VkupnoPodkategorii(podkategorija);

            }
            return vkupno+kategorija.Kategorii1.Count();
        }
        public int PresmetajProizvodiSporedId(int idKategorija)
        {
            int vkupno = 0;
            var kategorija = _db.Kategorii.Find(idKategorija);
            if (kategorija != null)
            {
                vkupno = VkupnoProizvodi(kategorija);
            }

            return vkupno;
        }
        private int VkupnoProizvodi(Kategorii kategorija)
        {
            int vkupno = 0;
            foreach (Kategorii podkategorija in kategorija.Kategorii1.ToList())
            {

                vkupno += VkupnoProizvodi(podkategorija);

            }
            return vkupno + kategorija.Proizvodi.Count();
        }
        public Kategorii VratiKategorijaSporedId(int idKategorii)
        {
            return _db.Kategorii.Find(idKategorii);
        }

        public List<ViewBreadcrumb> GetParentNames(int id)
        {
            Kategorii kategorija = _db.Kategorii.Find(id);
            List<ViewBreadcrumb> breadcrumb = new List<ViewBreadcrumb>();
            while (kategorija.RoditelId != null)
              {
                  breadcrumb.Add(new ViewBreadcrumb{
                      Name = kategorija.NazivNaKategorija,
                      Id = kategorija.IdKategorii
                     });
                  kategorija = _db.Kategorii.Find(kategorija.RoditelId);
              }
            breadcrumb.Add(new ViewBreadcrumb
            {
                Name = kategorija.NazivNaKategorija,
                Id = kategorija.IdKategorii
            });
            return breadcrumb;
        }
    }
}
