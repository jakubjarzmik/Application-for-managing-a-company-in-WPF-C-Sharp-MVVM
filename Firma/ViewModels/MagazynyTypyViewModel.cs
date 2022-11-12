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
    class MagazynyTypyViewModel : WszystkieViewModel<MagazynyTypy>
    {
        #region Konstruktor
        public MagazynyTypyViewModel() 
            : base("Rodzaje kontrahentów")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<MagazynyTypy>
                (
                    from typ in JJFirmaEntities.MagazynyTypy
                    where typ.CzyAktywny == true
                    select typ
                );
        }
        #endregion

    }
}
