using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MojKatalog.Models.ViewModels
{
    public class ViewPoraki
    {
        [Display(Name = "Наслов")]
        [Required(ErrorMessage = "Насловот е задолжителен")]
        public string Subject { get; set; }

        [Display(Name="Содржина")]
        [Required(ErrorMessage ="Содржината е задолжителна")]
        public string Body { get; set; }

        public List<ViewKlienti> KlientiGrupirani { get; set; }
    }
}