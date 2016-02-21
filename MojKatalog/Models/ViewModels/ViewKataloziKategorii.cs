using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MojKatalog.Models;

namespace MojKatalog.Models.ViewModels
{
    public class ViewKataloziKategorii
    {
        public int IdKatalog { get; set; }
        [Display(Name="Назив")]
        public string Naziv { get; set; }
        [Display(Name = "Опис")]
        public string Opis { get; set; }
        [Display(Name = "Дата на креирање")]
        public DateTime? DataNaKreiranje { get; set; }
        [Display(Name = "Објавен")]
        public bool Objaven { get; set; }
        [DisplayName("Категории")]
        public string ViewKategorii { get; set; }
    }
}