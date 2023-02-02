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
            : base("Typy magazynów")
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
        public override void Delete()
        {
            try
            {
                var toDelete = JJFirmaEntities.MagazynyTypy.Where(a => a.TypMagazynuId == Selected.TypMagazynuId).FirstOrDefault();
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
