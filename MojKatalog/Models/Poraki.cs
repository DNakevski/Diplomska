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
    
    public partial class Poraki
    {
        public Poraki()
        {
            this.Klienti = new HashSet<Klienti>();
        }
    
        public int IdPoraki { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual ICollection<Klienti> Klienti { get; set; }
    }
}
