using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Models.ViewModels
{
    public class MailPageViewModel
    {
        public IEnumerable<Poraki> IsprateniPoraki { get; set; }
        public IEnumerable<Poraki> IzbrishaniPoraki { get; set; }
        public IEnumerable<Poraki> SocuvaniPoraki { get; set; }
        public IEnumerable<Poraki> PrimeniPoraki { get; set; }

        public int IsprateniPorakiCount
        {
            get
            {
                return (IsprateniPoraki == null) ? 0 : IsprateniPoraki.Count();
            }
        }

        public int IzbrishaniPorakiCount
        {
            get
            {
                return (IzbrishaniPoraki == null) ? 0 : IzbrishaniPoraki.Count();
            }
        }

        public int SocuvaniPorakiCount
        {
            get
            {
                return (SocuvaniPoraki == null) ? 0 : SocuvaniPoraki.Count();
            }
        }

        public int PrimeniPorakiCount
        {
            get
            {
                return (PrimeniPoraki == null) ? 0 : PrimeniPoraki.Count();
            }
        }
    }
}