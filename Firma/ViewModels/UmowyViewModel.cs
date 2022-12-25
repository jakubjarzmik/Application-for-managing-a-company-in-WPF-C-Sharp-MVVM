using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class UmowyViewModel : WszystkieViewModel<UmowaForAllView>
    {
        #region Konstruktor
        public UmowyViewModel():base("Umowy")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<UmowaForAllView>
                (
                    from umowa in JJFirmaEntities.Umowy
                    where umowa.CzyAktywny == true
                    select new UmowaForAllView
                    {
                        Numer = umowa.NrUmowy,
                        Rodzaj = umowa.UmowyRodzaje.Nazwa,
                        Stanowisko = umowa.UmowyStanowiska.Nazwa,
                        DataZawarcia = umowa.DataZawarcia,
                        DataOd = umowa.DataOd,
                        DataDo = umowa.DataDo,
                        StawkaMies = umowa.StawkaBruttoMies,
                        StawkaGodz = umowa.StawkaBruttoGodz,
                        CzasPracyMies = umowa.CzasPracyMies,
                        Opis = umowa.Opis,
                        JestEmerytalne = umowa.JestEmerytalne ? "Tak" : "Nie",
                        JestRentowe = umowa.JestRentowe ? "Tak" : "Nie",
                        JestChorobowe = umowa.JestChorobowe ? "Tak" : "Nie",
                        JestWypadkowe = umowa.JestWypadkowe ? "Tak" : "Nie"
                    }
                );
        }
        #endregion

    }
}
