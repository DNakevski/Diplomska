//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MojKatalog.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Proizvodi
    {
        public int IdProizvodi { get; set; }
        public string NazivNaProizvod { get; set; }
        public string Specifikacija { get; set; }
        public Nullable<decimal> Cena { get; set; }
        public Nullable<int> Popust { get; set; }
        public string SlikaNaProizvod { get; set; }
        public Nullable<bool> Dostapnost { get; set; }
        public Nullable<int> IdKategorii { get; set; }
    
        public virtual Kategorii Kategorii { get; set; }
    }
}
