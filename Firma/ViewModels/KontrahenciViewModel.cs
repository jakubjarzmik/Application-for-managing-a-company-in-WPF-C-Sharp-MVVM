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
    public class KontrahenciViewModel : WszystkieViewModel<KontrahentForAllView>
    {
        #region Konstruktor
        public KontrahenciViewModel():base("Kontrahenci")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<KontrahentForAllView>
                (
                    from kontrahent in JJFirmaEntities.Kontrahenci
                    where kontrahent.CzyAktywny == true
                    select new KontrahentForAllView
                    {
                        Kod = kontrahent.Kod,
                        Nazwa1 = kontrahent.Nazwa1,
                        Nip = kontrahent.Nip,
                        Regon = kontrahent.Regon,
                        Adres = kontrahent.Adresy.Ulica + " " + kontrahent.Adresy.NrDomu +
                        (kontrahent.Adresy.NrLokalu.Equals("") ? "":"/"+ kontrahent.Adresy.NrLokalu)+
                        "\n" + kontrahent.Adresy.KodPocztowy + " " + kontrahent.Adresy.Poczta,
                        Url = kontrahent.URL
                    }
                );
        }
        #endregion

    }
}
