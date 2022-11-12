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
    class TowaryGrupyViewModel : WszystkieViewModel<TowaryGrupy>
    {
        #region Konstruktor
        public TowaryGrupyViewModel() 
            : base("Grupy towarów")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<TowaryGrupy>
                (
                    from grupa in JJFirmaEntities.TowaryGrupy
                    where grupa.CzyAktywny == true
                    select grupa
                );
        }
        #endregion

    }
}
