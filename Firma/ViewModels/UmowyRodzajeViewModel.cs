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
    class UmowyRodzajeViewModel : WszystkieViewModel<UmowyRodzaje>
    {
        #region Konstruktor
        public UmowyRodzajeViewModel() 
            : base("Rodzaje umów")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<UmowyRodzaje>
                (
                    from rodzaj in JJFirmaEntities.UmowyRodzaje
                    where rodzaj.CzyAktywny == true
                    select rodzaj
                );
        }
        #endregion

    }
}
