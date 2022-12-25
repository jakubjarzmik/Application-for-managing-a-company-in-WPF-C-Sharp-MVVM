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
    class KrajeViewModel : WszystkieViewModel<Kraje>
    {
        #region Konstruktor
        public KrajeViewModel() 
            : base("Kraje")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Kraje>
                (
                    from kraj in JJFirmaEntities.Kraje
                    where kraj.CzyAktywny == true
                    select kraj
                );
        }
        #endregion

    }
}
