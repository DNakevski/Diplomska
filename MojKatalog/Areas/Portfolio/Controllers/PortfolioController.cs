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

namespace MojKatalog.Areas.Portfolio.Controllers
{
    public class PortfolioController : Controller
    {
        QPortfolio model = new QPortfolio();
        private void DeleteImage(string oldValPath)
        {

            string fullPath = Request.MapPath("~" + oldValPath);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }
 
        
        private string CreateAndSaveImage(string imgStream,int id,string bgName, string oldValPath)
        {
            if (imgStream!=null)
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
            WebSiteSettings portfolioSettings = new WebSiteSettings
            {
                IdKatalozi = id,
                FontFamily = "OpenSans-Regular",
                FontColor1 = "darkBlueDarker",
                FontColor2 = "greenLighter",
                BGPocetna = "~/Areas/Portfolio/Images/HomeBackgroundImage.jpg",
                BGZaNas = "white",
                BGFZaNas = "~/Areas/Portfolio/Images/AboutFooter.jpg",
                BGPortfolio = "white",
                BGFPortfolio = "~/Areas/Portfolio/Images/AboutFooter.jpg",
                BGContact = "lightGreyLighter",
                BGMenu = "lightGreyLighter",
                BGFooter = "lightGreyLighter"
            };
            //Insert vo tabela WebSiteSettings so default vrednosti
            model.DodadiPortfolio(portfolioSettings);
            return View();
        }
        [HttpGet]
        public ActionResult IzmeniPortfolio(int id) 
        {
            WebSiteSettings portfolioSettings = model.IzmeniPortfolioGet(id);
            //Editiranje na vrednosti vo tabela WebSiteSettings
            return View(portfolioSettings);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult IzmeniPortfolio(WebSiteSettings wsettings)
        {
            WebSiteSettings oldWsettings = model.IzmeniPortfolioGet(wsettings.IdKatalozi);
            wsettings.BGPocetna = CreateAndSaveImage(wsettings.BGPocetna, wsettings.IdKatalozi,"Pocetna", oldWsettings.BGPocetna);
            wsettings.BGZaNas = CreateAndSaveImage(wsettings.BGZaNas, wsettings.IdKatalozi,"ZaNas", oldWsettings.BGZaNas);
            wsettings.BGFZaNas = CreateAndSaveImage(wsettings.BGFZaNas, wsettings.IdKatalozi,"FooterZaNas", oldWsettings.BGFZaNas);
            wsettings.BGPortfolio = CreateAndSaveImage(wsettings.BGPortfolio, wsettings.IdKatalozi,"Portfolio", oldWsettings.BGPortfolio);
            wsettings.BGFPortfolio = CreateAndSaveImage(wsettings.BGFPortfolio, wsettings.IdKatalozi,"FooterPortfolio", oldWsettings.BGFPortfolio);
            wsettings.BGContact = CreateAndSaveImage(wsettings.BGContact, wsettings.IdKatalozi,"Contact", oldWsettings.BGContact);
            wsettings.BGMenu = CreateAndSaveImage(wsettings.BGMenu, wsettings.IdKatalozi,"Menu", oldWsettings.BGMenu);
            wsettings.BGFooter = CreateAndSaveImage(wsettings.BGFooter, wsettings.IdKatalozi,"Footer", oldWsettings.BGFooter);
            model.IzmeniPortfolioPost(wsettings);
           

            return Json(new { Status = "Success"});
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

        public ActionResult Test()
        {
            return View();
        }
    }
}