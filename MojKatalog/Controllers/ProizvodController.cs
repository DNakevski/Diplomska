using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using MojKatalog.Queries;
using MojKatalog.Helpers;

namespace MojKatalog.Controllers
{
    public class ProizvodController : Controller
    {
        //
        // GET: /Proizvod/
        QProizvod model = new QProizvod();
        QKategorija kategorija = new QKategorija();
        public ActionResult Index()
        {
            return View(model.izlistajProizvodiKategorii(0));

        }
        public ActionResult PregledajProizvodi(int id)
        {
            List<ViewBreadcrumb> list = kategorija.getParentNames(id);
            ViewBag.BreadCrumb = kategorija.getParentNames(id);
            var kat = kategorija.vratiKategorijaSporedId(id);
            return View(kat);
        }
        [HttpGet]
        public ActionResult DodadiProizvod(int id)
        {
            List<ViewBreadcrumb> list = kategorija.getParentNames(id);
            ViewBag.BreadCrumb = kategorija.getParentNames(id);
            ViewBag.IdKategorija = id;
            return View(new Proizvodi());
        }
        [HttpPost]
        public ActionResult DodadiProizvod(Proizvodi proizvod, HttpPostedFileBase file, int id)
        {
            model.dodadi(proizvod);
            if (file != null)
            {
                string[] allowed = { ".jpg", ".jpeg", ".png", ".gif" };
                string extension = System.IO.Path.GetExtension(file.FileName);

                if (allowed.Contains(extension.ToLower()))
                {
                    string CoverPath = WebConfigurationManager.OpenWebConfiguration("/Views/Web.config").AppSettings.Settings["CoverPath"].Value;
                    string NewLocation = Server.MapPath("~") + CoverPath + "Cover_" + proizvod.IdProizvodi + extension;
                    string tip = file.GetType().ToString();
                    file.SaveAs(NewLocation);
                    string path = CoverPath + "Cover_" + proizvod.IdProizvodi + extension;
                    model.izmeniPateka(path, proizvod);
                }
            }
            return RedirectToAction("PregledajProizvodi", new { id = id });
        }
        public ActionResult PregledajProizvod(int idProizvod, int idKategorija)
        {
            List<ViewBreadcrumb> list = kategorija.getParentNames(idKategorija);
            ViewBag.BreadCrumb = kategorija.getParentNames(idKategorija);
            ViewBag.IdKategorija = idKategorija;
            Proizvodi proizvod = model.vratiProizvod(idProizvod);
            return View(proizvod);
        }
        public void IzbrisiProizvod()
        {
        }
        [HttpGet]
        public ActionResult IzmeniProizvod(int idProizvod, int idKategorija)
        {
            List<ViewBreadcrumb> list = kategorija.getParentNames(idKategorija);
            ViewBag.BreadCrumb = kategorija.getParentNames(idKategorija);
            ViewBag.IdKategorija = idKategorija;
            Proizvodi proizvod = model.vratiProizvod(idProizvod);
            return View(proizvod);
        }
        [HttpPost]
        public ActionResult IzmeniProizvod(Proizvodi proizvod, HttpPostedFileBase file, int id)
        {
           // System.IO.File.SetAttributes(Server.MapPath("~") + proizvod.SlikaNaProizvod, FileAttributes.Normal);
            if (!String.IsNullOrEmpty(proizvod.SlikaNaProizvod) && file != null)
            {
                System.IO.File.Delete(Server.MapPath("~") + proizvod.SlikaNaProizvod);
            }
            
           
            if (file != null)
            {
                string[] allowed = { ".jpg", ".jpeg", ".png", ".gif" };

                string extension = System.IO.Path.GetExtension(file.FileName);
                if (allowed.Contains(extension.ToLower()))
                {
                    long addition = DateTime.Now.GetTimestampSeconds();
                    string CoverPath = WebConfigurationManager.OpenWebConfiguration("/Views/Web.config").AppSettings.Settings["CoverPath"].Value;
                    proizvod.SlikaNaProizvod = CoverPath + "Cover_" + proizvod.IdProizvodi + "_" + addition + extension;
                    string NewLocation = Server.MapPath("~") + proizvod.SlikaNaProizvod;
                    string tip = file.GetType().ToString();
                    file.SaveAs(NewLocation);
                }
            }
            model.izmeni(proizvod);
            return RedirectToAction("PregledajProizvod", new { idProizvod = proizvod.IdProizvodi, idKategorija= proizvod.IdKategorii});
        }
    }
}
