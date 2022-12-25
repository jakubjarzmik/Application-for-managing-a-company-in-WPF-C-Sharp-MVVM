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
    class TowaryStawkiVatViewModel : WszystkieViewModel<StawkaVatTowaruForAllView>
    {
        #region Konstruktor
        public TowaryStawkiVatViewModel() 
            : base("Stawki VAT")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<StawkaVatTowaruForAllView>
                (
                    from stawka in JJFirmaEntities.TowaryStawkiVat
                    where stawka.CzyAktywny == true
                    select new StawkaVatTowaruForAllView
                    {
                        Kraj = stawka.Kraje.Nazwa,
                        Stawka = stawka.Stawka,
                        Opis = stawka.Opis
                    }
                );
        }
        #endregion

    }
}
