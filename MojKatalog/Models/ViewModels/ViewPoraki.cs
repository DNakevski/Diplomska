using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Models.ViewModels
{
    public class ViewPoraki
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<ViewKlienti> KlientiGrupirani { get; set; }
    }
}