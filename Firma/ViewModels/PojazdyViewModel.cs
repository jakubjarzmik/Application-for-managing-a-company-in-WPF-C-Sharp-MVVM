using Firma.Helpers;
using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firma.ViewModels
{
    internal class PojazdyViewModel : WszystkieViewModel<Pojazdy>
    {

        #region Konstruktor
        public PojazdyViewModel()
            :base("Towary")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Pojazdy>(
                from pojazd in JJFirmaEntities.Pojazdy
                where pojazd.CzyAktywny == true
                select pojazd
                ) ;
        }
        #endregion
    }
}
