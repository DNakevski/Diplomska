using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Models.ViewModels;
using MojKatalog.Models;

namespace MojKatalog.Queries
{
    public class KataloziIdINaziv 
    {
        public int idkatalozi { get; set; }
        public string naziv { get; set; }
    }
    public class QKatalog
    {
        //
        // GET: /QKatalog/
        dbKatalogEntities _db = new dbKatalogEntities();
        QKategorija kategorii = new QKategorija();
        public void dodadi(Katalozi katalog)
        {
            _db.Katalozi.Add(katalog);
            _db.SaveChanges();
        }
        public List<ViewKataloziKategorii> izlistaj()
        {
           int idpom;
           string stringpom;
          int [] kataloziId = allKatalozi();
          int kataloziIdLength = kataloziId.Length;
          List<ViewKataloziKategorii> kataloziIkategorii = new List<ViewKataloziKategorii>();
          for (int i = 0; i < kataloziIdLength; i++)
          {

              idpom = kataloziId[i];
              stringpom=ConvertStringListToString(_db.Kategorii.Where(x => x.IdKatalozi == idpom && x.RoditelId == null).Select(x => x.NazivNaKategorija).ToList());
              kataloziIkategorii.Add(new ViewKataloziKategorii
                            {
                                ViewKatalozi = _db.Katalozi.Find(kataloziId[i]),
                                ViewKategorii = stringpom
                            });
          }

          return kataloziIkategorii.ToList();
        }
        public Katalozi vratiKatalog(int id) 
        {
            return _db.Katalozi.Find(id);
        }
        public void izmeni(Katalozi newKatalog)
        {
            Katalozi katalog = vratiKatalog(newKatalog.IdKatalozi);
            katalog.NazivNaKatalog = newKatalog.NazivNaKatalog;
            katalog.OpisNaKatalog = newKatalog.OpisNaKatalog;
            katalog.DataNaKreiranje = newKatalog.DataNaKreiranje;
            _db.SaveChanges();
        }
        public void izbrisi(int id)
        {
            Katalozi katalog = _db.Katalozi.Find(id);
            _db.Katalozi.Remove(katalog);
            _db.SaveChanges();
        }
        public KataloziIdINaziv[] kataloziIdINaziv()
        {
            return _db.Katalozi.Select(x => new KataloziIdINaziv { idkatalozi = x.IdKatalozi, naziv = x.NazivNaKatalog }).ToArray();

        }
        public int[] allKatalozi()
        {
            return _db.Katalozi.Select(x => x.IdKatalozi).ToArray();
        }
        private string ConvertStringListToString(List<string> lista)
        {
            string resultString="";
            if (lista.Count() == 0) {
                lista.Add("");
            }
            foreach (string elem in lista)
            {
                resultString = resultString + elem + ",";
            }
            resultString = resultString.Remove(resultString.Length - 1, 1);
            return resultString;
        }

    }
}
