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
    public class FakturyViewModel : WszystkieViewModel<FakturaForAllView>
    {
        #region Konstruktor
        public FakturyViewModel():base("Faktury")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<FakturaForAllView>
                (
                    from faktura in JJFirmaEntities.Faktury
                    where faktura.CzyAktywny == true
                    select new FakturaForAllView
                    {
                        Numer = faktura.Numer,
                        DataWystawienia = faktura.DataWystawienia,
                        KontrahentNazwa = faktura.Kontrahenci.Nazwa1,
                        KontrahentNip = faktura.Kontrahenci.Nip,
                        RodzajePlatnosciNazwa = faktura.RodzajePlatnosci.Nazwa
                    }
                );
        }
        #endregion

    }
}
