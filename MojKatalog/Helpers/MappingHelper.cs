using MojKatalog.Models;
using MojKatalog.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MojKatalog.Helpers
{
    public class MappingHelper
    {
        public static LoggedInEntity LoggedInEntityFromKlient(Poedinci poedinec)
        {
            return new LoggedInEntity
            {
                Id = poedinec.IdPoedinci,
                KorisnickoIme = poedinec.KorisnickoIme,
                Mail = poedinec.Mail,
                Naziv = poedinec.Ime + " " + poedinec.Prezime,
                Telefon = poedinec.Telefon,
                Role = "Poedinec",
                UserType = Helpers.Enumerations.LogedUserTypeEnum.Poedinec
            };
        }

        public static LoggedInEntity LoggedInEntityFromKompanija(Kompanii kompanija)
        {
            return new LoggedInEntity
            {
                Id = kompanija.IdKompanii,
                KorisnickoIme = kompanija.KorisnickoIme,
                Mail = kompanija.Mail,
                Naziv = kompanija.NazivNaKompanija,
                Telefon = kompanija.Telefon,
                Role = "Kompanija",
                UserType = Helpers.Enumerations.LogedUserTypeEnum.Kompanija
            };
        }
    }
}