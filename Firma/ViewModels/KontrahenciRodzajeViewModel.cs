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
    class KontrahenciRodzajeViewModel : WszystkieViewModel<KontrahenciRodzaje>
    {
        #region Konstruktor
        public KontrahenciRodzajeViewModel() 
            : base("Rodzaje kontrahentów")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<KontrahenciRodzaje>
                (
                    from rodzaj in JJFirmaEntities.KontrahenciRodzaje
                    where rodzaj.CzyAktywny == true
                    select rodzaj
                );
        }
        #endregion

    }
}
