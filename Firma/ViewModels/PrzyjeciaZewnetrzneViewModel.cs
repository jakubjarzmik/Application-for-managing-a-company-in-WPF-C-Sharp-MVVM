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
    public class PrzyjeciaZewnetrzneViewModel : WszystkieViewModel<PrzyjecieZewnetrzneForAllView>
    {
        #region Konstruktor
        public PrzyjeciaZewnetrzneViewModel():base("Przyjecia Zewnetrzne")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PrzyjecieZewnetrzneForAllView>
                (
                    from pz in JJFirmaEntities.PrzyjeciaZewnetrzne
                    where pz.CzyAktywny == true
                    select new PrzyjecieZewnetrzneForAllView
                    {
                        Numer = pz.Numer,
                        DataPrzyjecia = pz.DataPrzyjecia,
                        NazwaMagazynu = pz.Magazyny.Nazwa,
                        NazwaKontrahenta = pz.Kontrahenci.Nazwa1,
                        NipKontrahenta = pz.Kontrahenci.Nip
                    }
                );
        }
        #endregion

    }
}
