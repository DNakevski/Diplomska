using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojKatalog.Models.ViewModels
{
    public class ProizvodViewModel
    {
        public int IdProizvodi { get; set; }
        [Display(Name = "Назив на Производ")]
        public string NazivNaProizvod { get; set; }
        [Display(Name = "Спецификација")]
        public string Specifikacija { get; set; }
        [Display(Name = "Цена")]
        public Nullable<decimal> Cena { get; set; }
        [Display(Name = "Попуст")]
        public Nullable<int> Popust { get; set; }
        [Display(Name = "Слика на производ")]
        public string SlikaNaProizvod { get; set; }
        [Display(Name = "Достапност")]
        public Nullable<bool> Dostapnost { get; set; }
        public Nullable<int> IdKategorii { get; set; }
    }
}