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
    public class PozycjeWydaniaZewnetrznegoViewModel : WszystkieViewModel<PozycjaWydaniaZewnetrznegoForAllView>
    {
        #region Konstruktor
        public PozycjeWydaniaZewnetrznegoViewModel():base("Pozycje WZ")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PozycjaWydaniaZewnetrznegoForAllView>
                (
                    from pozycja in JJFirmaEntities.PozycjeWydaniaZewnetrznego
                    where pozycja.CzyAktywny == true
                    select new PozycjaWydaniaZewnetrznegoForAllView
                    {
                        NumerWydaniaZewnetrznego = pozycja.WydaniaZewnetrzne.Numer,
                        NazwaKontrahenta = pozycja.WydaniaZewnetrzne.Kontrahenci.Nazwa1,
                        NazwaTowaru = pozycja.Towary.Nazwa,
                        Ilosc = pozycja.Ilosc,
                        JednostkaMiary = pozycja.JednostkiMiary.Skrot,
                        Rabat = pozycja.Rabat,
                    }
                );
        }
        #endregion

    }
}
