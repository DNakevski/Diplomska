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
    
    public partial class WebSiteSettings
    {
        public int IdKatalozi { get; set; }
        public string FontFamily { get; set; }
        public string FontColor1 { get; set; }
        public string FontColor2 { get; set; }
        public string BGPocetna { get; set; }
        public string BGZaNas { get; set; }
        public string BGFZaNas { get; set; }
        public string BGPortfolio { get; set; }
        public string BGFPortfolio { get; set; }
        public string BGContact { get; set; }
        public string BGMenu { get; set; }
        public string BGFooter { get; set; }
        public string Naziv { get; set; }
        public string SodrzinaZaNasF { get; set; }
        public string SodrzinaPortfolioF { get; set; }
        public bool Objaven { get; set; }
        public Nullable<System.DateTime> DatumObjaven { get; set; }
        public string CoverUrl { get; set; }
    
        public virtual Katalozi Katalozi { get; set; }
    }
}
