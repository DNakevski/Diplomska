using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MojKatalog.Queries;
using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using MojKatalog.Helpers;
using MojKatalog.Filters;
using System.Drawing;
using System.IO;

namespace MojKatalog.Controllers
{
    public class KompanijaController : Controller
    {
        QKompanija _qkompanija = new QKompanija();
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
                if (imgStream.Contains("/Images/CompanyImages/"+folderName+"/"))
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
                    string path = Path.Combine(Server.MapPath("~/Images/CompanyImages/" + folderName + "/"), name);
                    Bitmap bitmap = new Bitmap(image);
                    bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //Se brise starata slika od likacijata
                    DeleteImage(oldValPath);
                    //Se vraca lokacija kade sto e socuvana slikata, koja treba da se socuva vo baza
                    return "/Images/CompanyImages/"+folderName+"/" + name;
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
            Kompanii model = _qkompanija.GetKompanija(user.Id);
            return View(model);
        }
        public ActionResult IzmeniKompanijaInfo(string status = "")
        {
            ViewBag.Status = status;
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            Kompanii model = _qkompanija.GetKompanija(user.Id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IzmeniKompanijaInfo(Kompanii newKompanija)
        {
            Kompanii oldKompanija = _qkompanija.GetKompanija(newKompanija.IdKompanii);
            newKompanija.LogoNaKompanija = CreateAndSaveImage(newKompanija.LogoNaKompanija, newKompanija.IdKompanii, "Logo", oldKompanija.LogoNaKompanija,"Logos");
            newKompanija.ProfilnaSlika = CreateAndSaveImage(newKompanija.ProfilnaSlika, newKompanija.IdKompanii, "Profile", oldKompanija.ProfilnaSlika, "Profile");
            _qkompanija.SetKompanija(newKompanija);
            return RedirectToAction("IzmeniKompanijaInfo", new { status = "success" });
        }
    }
}