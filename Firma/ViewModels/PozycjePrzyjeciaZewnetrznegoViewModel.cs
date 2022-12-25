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
    public class PozycjePrzyjeciaZewnetrznegoViewModel : WszystkieViewModel<PozycjaPrzyjeciaZewnetrznegoForAllView>
    {
        #region Konstruktor
        public PozycjePrzyjeciaZewnetrznegoViewModel():base("Pozycje PZ")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PozycjaPrzyjeciaZewnetrznegoForAllView>
                (
                    from pozycja in JJFirmaEntities.PozycjePrzyjeciaZewnetrznego
                    where pozycja.CzyAktywny == true
                    select new PozycjaPrzyjeciaZewnetrznegoForAllView
                    {
                        NumerPrzyjeciaZewnetrznego = pozycja.PrzyjeciaZewnetrzne.Numer,
                        NazwaKontrahenta = pozycja.PrzyjeciaZewnetrzne.Kontrahenci.Nazwa1,
                        NazwaTowaru = pozycja.Towary.Nazwa,
                        Ilosc = pozycja.Ilosc,
                        JednostkaMiary = pozycja.JednostkiMiary.Skrot,
                        PierwotnaCenaZakupu = pozycja.PierwotnaCenaZakupu,
                        Rabat = pozycja.Rabat,
                        CenaPoRabacieZaSzt = pozycja.PierwotnaCenaZakupu * (100 - pozycja.Rabat) / 100,
                        Wartosc = (pozycja.PierwotnaCenaZakupu * pozycja.Ilosc * (100 - pozycja.Rabat) / 100 ),
                    }
                );
        }
        #endregion

    }
}
