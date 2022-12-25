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
    class JednostkiMiaryViewModel : WszystkieViewModel<JednostkiMiary>
    {
        #region Konstruktor
        public JednostkiMiaryViewModel() 
            : base("Jednostki miary")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<JednostkiMiary>
                (
                    from jednmiary in JJFirmaEntities.JednostkiMiary
                    where jednmiary.CzyAktywny == true
                    select jednmiary
                );
        }
        #endregion

    }
}
