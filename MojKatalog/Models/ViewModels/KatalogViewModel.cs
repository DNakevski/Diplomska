using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Models.ViewModels
{
    public class KatalogViewModel
    {
        public int? IdKatalog { get; set; }
        public string NazivNaKatalog { get; set; }
        public IEnumerable<TreeViewModel> Trees { get; set; }
    }
}