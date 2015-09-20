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
using MojKatalog.Filters;

namespace MojKatalog.Controllers
{
    [CustomAuthorize(Roles = "Admin,Poedinec,Kompanija")]
    public class ProizvodController : Controller
    {
        //
        // GET: /Proizvod/
        QProizvod model = new QProizvod();
        QKategorija kategorija = new QKategorija();
        public ActionResult Index()
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            return View(model.IzlistajProizvodiKategorii(0, user.Id, user.UserType));
        }
        public ActionResult PregledajProizvodi(int id)
        {
            List<ViewBreadcrumb> list = kategorija.GetParentNames(id);
            ViewBag.BreadCrumb = kategorija.GetParentNames(id);
            var kat = kategorija.VratiKategorijaSporedId(id);
            return View(kat);
        }
        [HttpGet]
        public ActionResult DodadiProizvod(int id)
        {
            List<ViewBreadcrumb> list = kategorija.GetParentNames(id);
            ViewBag.BreadCrumb = kategorija.GetParentNames(id);
            ViewBag.IdKategorija = id;
            return View(new ProizvodViewModel());
        }
        [HttpPost]
        public ActionResult DodadiProizvod(Proizvodi proizvod, HttpPostedFileBase file, int id)
        {
            model.DodadiProizvod(proizvod);
            if (file != null)
            {
                string[] allowed = { ".jpg", ".jpeg", ".png", ".gif" };
                string extension = System.IO.Path.GetExtension(file.FileName);

                if (allowed.Contains(extension.ToLower()))
                {
                    string CoverPath = "/Images/UserImages/";//TODO: patekata treba da se zima od web.config
                    string NewLocation = Server.MapPath("~") + CoverPath + "Cover_" + proizvod.IdProizvodi + extension;
                    string tip = file.GetType().ToString();
                    file.SaveAs(NewLocation);
                    string path = CoverPath + "Cover_" + proizvod.IdProizvodi + extension;
                    model.IzmeniPateka(path, proizvod);
                }
            }
            return RedirectToAction("PregledajProizvodi", new { id = id });
        }
        public ActionResult PregledajProizvod(int idProizvod, int idKategorija)
        {
            List<ViewBreadcrumb> list = kategorija.GetParentNames(idKategorija);
            ViewBag.BreadCrumb = kategorija.GetParentNames(idKategorija);
            ViewBag.IdKategorija = idKategorija;
            Proizvodi proizvod = model.VratiProizvod(idProizvod);
            return View(proizvod);
        }
        public void IzbrisiProizvod()
        {
        }
        [HttpGet]
        public ActionResult IzmeniProizvod(int idProizvod, int idKategorija)
        {
            List<ViewBreadcrumb> list = kategorija.GetParentNames(idKategorija);
            ViewBag.BreadCrumb = kategorija.GetParentNames(idKategorija);
            ViewBag.IdKategorija = idKategorija;
            ProizvodViewModel proizvod = model.VratiProizvodViewModel(idProizvod);
            return View(proizvod);
        }
        [HttpPost]
        public ActionResult IzmeniProizvod(ProizvodViewModel proizvod, HttpPostedFileBase file, int id)
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
                    string CoverPath = "/Images/UserImages/";
                    proizvod.SlikaNaProizvod = CoverPath + "Cover_" + proizvod.IdProizvodi + "_" + addition + extension;
                    string NewLocation = Server.MapPath("~") + proizvod.SlikaNaProizvod;
                    string tip = file.GetType().ToString();
                    file.SaveAs(NewLocation);
                }
            }
            model.IzmeniProizvod(proizvod);
            return RedirectToAction("PregledajProizvod", new { idProizvod = proizvod.IdProizvodi, idKategorija= proizvod.IdKategorii});
        }
    }
}
