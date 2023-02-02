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
    internal class FakturyRodzajeViewModel : WszystkieViewModel<FakturyRodzaje>
    {

        #region Konstruktor
        public FakturyRodzajeViewModel()
            :base("Rodzaje faktur")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<FakturyRodzaje>(
                from rodzaj in JJFirmaEntities.FakturyRodzaje
                where rodzaj.CzyAktywny == true
                select rodzaj
                ) ;
        }
        public override void Delete()
        {
            try
            {
                var toDelete = JJFirmaEntities.FakturyRodzaje.Where(a => a.RodzajFakturyId == Selected.RodzajFakturyId).FirstOrDefault();
                if (toDelete != null)
                {
                    toDelete.CzyAktywny = false;
                    JJFirmaEntities.SaveChanges();
                    Load();
                }
            }
            catch (Exception) { }
        }
        #endregion
    }
}
