using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojKatalog.Models.ViewModels
{
    public class ViewProizvodiKategorii
    {
        public Kategorii MVKategorija { get; set; }
        [Display(Name = "Вкупно подкатегории")]
        public int MVVkupnoPodkategorii { get; set; }
        [Display(Name = "Вкупно производи")]
        public int MVVkupnoProizvodi { get; set; }
    }
}