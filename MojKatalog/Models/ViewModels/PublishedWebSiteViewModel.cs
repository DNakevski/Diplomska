using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Models.ViewModels
{
    public class PublishedWebSiteViewModel
    {
        public int WebSiteId { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public DateTime? DataNaKreiranje { get; set; }
        public string Sopstvenik { get; set; }
        public string ImageUrl { get; set; }
    }
}