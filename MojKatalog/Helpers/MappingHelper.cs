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
        public static LoggedInEntity LoggedInEntityFromKlient(Klienti klient)
        {
            return new LoggedInEntity
            {
                Id = klient.IdKlienti,
                KorisnickoIme = klient.KorisnickoIme,
                Mail = klient.Mail,
                Naziv = klient.Ime + " " + klient.Prezime,
                Telefon = klient.Telefon,
                Role = "Poedinec",
                UserType = Helpers.Enumerations.LogedUserTypeEnum.Poedinec
            };
        }
    }
}