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
    class TowaryJednMiaryViewModel : WszystkieViewModel<TowaryJednMiary>
    {
        #region Konstruktor
        public TowaryJednMiaryViewModel() 
            : base("Jednostki miary")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<TowaryJednMiary>
                (
                    from jednmiary in JJFirmaEntities.TowaryJednMiary
                    where jednmiary.CzyAktywny == true
                    select jednmiary
                );
        }
        #endregion

    }
}
