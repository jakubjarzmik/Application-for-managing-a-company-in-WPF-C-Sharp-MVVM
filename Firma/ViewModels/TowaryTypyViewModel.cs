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
    class TowaryTypyViewModel : WszystkieViewModel<TowaryTypy>
    {
        #region Konstruktor
        public TowaryTypyViewModel() 
            : base("Typy towarów")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<TowaryTypy>
                (
                    from typ in JJFirmaEntities.TowaryTypy
                    where typ.CzyAktywny == true
                    select typ
                );
        }
        #endregion

    }
}
