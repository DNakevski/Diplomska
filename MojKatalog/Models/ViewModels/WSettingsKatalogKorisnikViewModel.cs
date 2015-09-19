using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Models.ViewModels
{
    public class WSettingsKatalogKorisnikViewModel
    {
        public WebSiteSettings WSettings { get; set; }
        public Katalozi Katalog { get; set; }
        public Poedinci Poedinec { get; set; }
        public Kompanii Kompanija { get; set; }
    }
}