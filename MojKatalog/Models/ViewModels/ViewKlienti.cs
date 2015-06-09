using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Models.ViewModels
{
    public class ViewKlienti
    {
        public string Karakter { get; set; }
        public List<Klienti> Klienti { get; set; }
    }
}