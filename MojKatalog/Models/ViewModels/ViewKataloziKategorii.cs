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
        public Katalozi ViewKatalozi { get; set; }
        [DisplayName("Категории")]
        public string ViewKategorii { get; set; }
    }
}