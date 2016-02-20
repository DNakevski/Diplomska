using MojKatalog.Areas.Portfolio.Queries;
using MojKatalog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Drawing;
using MojKatalog.Models.ViewModels;
using MojKatalog.Helpers;
using MojKatalog.Filters;

namespace MojKatalog.Areas.Portfolio.Controllers
{
    [CustomAuthorize(Roles = "Admin,Poedinec,Kompanija")]
    public class PortfolioController : Controller
    {
        QPortfolio model = new QPortfolio();
        private void DeleteImage(string oldValPath)
        {

            string fullPath = Request.MapPath("~" + oldValPath);

            if (System.IO.File.Exists(fullPath) && oldValPath!= "/Areas/Portfolio/Images/HomeBackgroundImage.jpg" && oldValPath!= "/Areas/Portfolio/Images/AboutFooter.jpg")
            {
                System.IO.File.Delete(fullPath);
            }
        }


        private string CreateAndSaveImage(string imgStream, int id, string bgName, string oldValPath)
        {
            if (imgStream != null)
            {
                if (imgStream.Contains("/Areas/Portfolio/"))
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
                    string path = Path.Combine(Server.MapPath("~/Areas/Portfolio/UploadedFiles"), name);
                    Bitmap bitmap = new Bitmap(image);
                    bitmap.Save(path, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //Se brise starata slika od likacijata
                    DeleteImage(oldValPath);
                    //Se vraca lokacija kade sto e socuvana slikata, koja treba da se socuva vo baza
                    return "/Areas/Portfolio/UploadedFiles/" + name;
                }
            }
            else
            {
                return null;
            }

        }
        // GET: Portfolio/Portfolio
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DodadiPortfolio(int id)
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            model.DodadiPortfolio(id, user.Id, user.UserType);
            return RedirectToAction("IzmeniPortfolio", new {id = id });
        }

        [HttpGet]
        public ActionResult IzmeniPortfolio(int id)
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            WSettingsKatalogKorisnikViewModel portfolioSettings = model.IzmeniPortfolioGet(id);
            //Editiranje na vrednosti vo tabela WebSiteSettings
            return View(portfolioSettings);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult IzmeniPortfolio(WebSiteSettings wsettings)
        {
            WebSiteSettings oldWsettings = model.GetWsettings(wsettings.IdKatalozi);
            if (!ColorHelper.IsColor(wsettings.BGPocetna))
            {
                wsettings.BGPocetna = CreateAndSaveImage(wsettings.BGPocetna, wsettings.IdKatalozi, "Pocetna", oldWsettings.BGPocetna);
            }
            if (!ColorHelper.IsColor(wsettings.BGZaNas))
            {
                wsettings.BGZaNas = CreateAndSaveImage(wsettings.BGZaNas, wsettings.IdKatalozi, "ZaNas", oldWsettings.BGZaNas);
            }
            if (!ColorHelper.IsColor(wsettings.BGFZaNas))
            {
                wsettings.BGFZaNas = CreateAndSaveImage(wsettings.BGFZaNas, wsettings.IdKatalozi, "FooterZaNas", oldWsettings.BGFZaNas);
            }
            if (!ColorHelper.IsColor(wsettings.BGPortfolio))
            {
                wsettings.BGPortfolio = CreateAndSaveImage(wsettings.BGPortfolio, wsettings.IdKatalozi, "Portfolio", oldWsettings.BGPortfolio);
            }
            if (!ColorHelper.IsColor(wsettings.BGFPortfolio))
            {
                wsettings.BGFPortfolio = CreateAndSaveImage(wsettings.BGFPortfolio, wsettings.IdKatalozi, "FooterPortfolio", oldWsettings.BGFPortfolio);
            }

            if (!ColorHelper.IsColor(wsettings.BGContact))
            {
                wsettings.BGContact = CreateAndSaveImage(wsettings.BGContact, wsettings.IdKatalozi, "Contact", oldWsettings.BGContact);
            }
            if (!ColorHelper.IsColor(wsettings.BGMenu))
            {
                wsettings.BGMenu = CreateAndSaveImage(wsettings.BGMenu, wsettings.IdKatalozi, "Menu", oldWsettings.BGMenu);
            }
            if (!ColorHelper.IsColor(wsettings.BGFooter))
            {
                wsettings.BGFooter = CreateAndSaveImage(wsettings.BGFooter, wsettings.IdKatalozi, "Footer", oldWsettings.BGFooter);
            }

            model.IzmeniPortfolioPost(wsettings);


            return Json(new { Status = "Success" });
        }

        public ActionResult IzbrisiPortfolio()
        {
            //Brisenje na vrednosti od tabela WebSiteSettings
            return View();

        }

        public ActionResult PregledajPortfolio()
        {
            //Pregled na website bez edit funkcionalnosti
            return View();
        }
        [HttpGet]
        public ActionResult IzlistajGalerija(int id)
        {
            Kategorii kategorija = model.IzlistajKategorijaSporedKategoriiId(id);
            return PartialView("_IzlistajGalerija", kategorija);
        }
        [HttpGet]
        public ActionResult DodadiGalerija(int? parentId, int? katalogId)
        {
            if (parentId == 0)
            {
                parentId = null;
            }
            List<Kategorii> pviewModel = model.IzlistajKategoriiSporedParentId(parentId, katalogId);
            ViewBag.ParentId = parentId;
            ViewBag.KatalogId = katalogId;
            return PartialView("_DodadiGalerija", pviewModel);
        }
        [HttpPost]
        public JsonResult IspratiPorakaOdWS(Poraki poraka)
        {
            var user = (LoggedInEntity)Session["LoggedInEntity"];
            poraka.Date = DateTime.Now;
            poraka.IsSent = false;
            poraka.IsDeleted = false;
            poraka.IsReceived = true;


            if (user.UserType == Helpers.Enumerations.LogedUserTypeEnum.Poedinec)
                poraka.IdPoedinci = user.Id;
            else
                poraka.IdKompanii = user.Id;

            model.SocuvajPoraka(poraka);
            return Json(new { Status = "Success" });
        }
        public ActionResult Test()
        {
            return View();
        }

    }
}