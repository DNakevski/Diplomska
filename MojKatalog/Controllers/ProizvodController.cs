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
using System.Drawing;

namespace MojKatalog.Controllers
{
    [CustomAuthorize(Roles = "Admin,Poedinec,Kompanija")]
    public class ProizvodController : Controller
    {
        //
        // GET: /Proizvod/
        QProizvod model = new QProizvod();
        QKategorija kategorija = new QKategorija();
        private void DeleteImage(string oldValPath)
        {

            string fullPath = Request.MapPath("~" + oldValPath);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }


        private string CreateAndSaveImage(string imgStream, int id, string bgName, string oldValPath, string folderName)
        {
            //folderName= Logos or Profile
            if (imgStream != null)
            {
                if (imgStream.Contains("/Images/" + folderName + "/"))
                {
                    return imgStream;
                }
                else
                {
                    long addition = DateTime.Now.GetTimestampSeconds();
                    string extension = ".jpeg";
                    byte[] bytes = Convert.FromBase64String(imgStream);

                    Image image;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = Image.FromStream(ms);
                    }
                    string name = bgName + "_" + id + "_" + addition + extension;
                    string path = Path.Combine(Server.MapPath("~/Images/" + folderName + "/"), name);
                    Bitmap bitmap = new Bitmap(image);
                    bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //Se brise starata slika od likacijata
                    DeleteImage(oldValPath);
                    //Se vraca lokacija kade sto e socuvana slikata, koja treba da se socuva vo baza
                    return "/Images/" + folderName + "/" + name;
                }
            }
            else
            {
                return null;
            }

        }
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
        [ValidateAntiForgeryToken]
        public ActionResult DodadiProizvod(Proizvodi proizvod, int id)
        {
            Proizvodi oldProizvod = model.VratiProizvod(proizvod.IdProizvodi);
            proizvod.SlikaNaProizvod = CreateAndSaveImage(proizvod.SlikaNaProizvod, proizvod.IdProizvodi, "Logo", oldProizvod.SlikaNaProizvod, "UserImages");
          
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

        [HttpGet]
        public ActionResult IzbrisiProizvod(int idProizvod, int idKategorija)
        {
            model.IzbrisiProizvod(idProizvod);
            return RedirectToAction("PregledajProizvodi", new { id = idKategorija });
        }
    }
}
