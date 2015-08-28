using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MojKatalog.Models.ViewModels
{
    public class KataloziWebSiteViewModel
    {
        public int ? IdKatalozi { get; set; }
        public bool HasWebSite { get; set; }
        [DisplayName("Каталог")]
        public string NazivNaKatalog { get; set; }
        [DisplayName("Веб Сајт")]
        public string Naziv { get; set; }

    }
}