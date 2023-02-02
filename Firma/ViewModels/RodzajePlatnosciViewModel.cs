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
    class RodzajePlatnosciViewModel : WszystkieViewModel<RodzajePlatnosci>
    {
        #region Konstruktor
        public RodzajePlatnosciViewModel() 
            : base("Rodzaje płatności")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<RodzajePlatnosci>
                (
                    from rodzaj in JJFirmaEntities.RodzajePlatnosci
                    where rodzaj.CzyAktywny == true
                    select rodzaj
                );
        }
        public override void Delete()
        {
            try
            {
                var toDelete = JJFirmaEntities.RodzajePlatnosci.Where(a => a.RodzajPlatnosciId == Selected.RodzajPlatnosciId).FirstOrDefault();
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
