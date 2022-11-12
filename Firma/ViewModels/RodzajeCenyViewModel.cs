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
    class RodzajeCenyViewModel : WszystkieViewModel<RodzajeCeny>
    {
        #region Konstruktor
        public RodzajeCenyViewModel() 
            : base("Rodzaje ceny")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<RodzajeCeny>
                (
                    from rodzaj in JJFirmaEntities.RodzajeCeny
                    where rodzaj.CzyAktywny == true
                    select rodzaj
                );
        }
        #endregion

    }
}
