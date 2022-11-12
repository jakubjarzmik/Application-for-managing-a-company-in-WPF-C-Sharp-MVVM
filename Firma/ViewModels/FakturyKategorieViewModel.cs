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
    internal class FakturyKategorieViewModel : WszystkieViewModel<FakturyKategorie>
    {

        #region Konstruktor
        public FakturyKategorieViewModel()
            :base("Kategorie faktur")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<FakturyKategorie>(
                from faktura in JJFirmaEntities.FakturyKategorie
                where faktura.CzyAktywny == true
                select faktura
                ) ;
        }
        #endregion
    }
}
