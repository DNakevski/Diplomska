using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Models.ViewModels
{
    public class LogiranPoedinecViewModel
    {
        public int IdPoedinec { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string KorisnickoIme { get; set; }
        public string Mail { get; set; }
        public string Telefon { get; set; }
    }
}