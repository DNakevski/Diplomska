using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Models.ViewModels
{
    public class LogiranaKompanijaViewModel
    {
        public int IdKompanija { get; set; }
        public string Naziv { get; set; }
        public string KorisnickoIme { get; set; }
        public string Mail { get; set; }
        public string Logo { get; set; }
        public string Opis { get; set; }
        public string Telefon { get; set; }
        public string Role { get; set; }
    }
}