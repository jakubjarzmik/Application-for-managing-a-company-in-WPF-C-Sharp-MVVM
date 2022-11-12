using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    class TowaryStawkiVatViewModel : WszystkieViewModel<TowaryStawkiVat>
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
            List = new ObservableCollection<TowaryStawkiVat>
                (
                    from stawkivat in JJFirmaEntities.TowaryStawkiVat
                    where stawkivat.CzyAktywny == true
                    select stawkivat
                );
        }
        #endregion

    }
}
