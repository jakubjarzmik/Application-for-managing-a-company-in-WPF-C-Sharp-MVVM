using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        public override void Add()
        {
            Messenger.Default.Send(new NowyTypTowarowViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.TowaryTypy.Where(a => a.TypTowaruId == Selected.TypTowaruId).FirstOrDefault();
                Messenger.Default.Send(new NowyTypTowarowViewModel(toEdit));
                Messenger.Default.Register<TowaryTypy>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(TowaryTypy edited)
        {
            edited.DataMod = DateTime.Now;
            edited.KtoModId = 1;
            JJFirmaEntities.SaveChanges();
            Load();
        }
        public override void Delete()
        {
            try
            {
                var toDelete = JJFirmaEntities.TowaryTypy.Where(a => a.TypTowaruId == Selected.TypTowaruId).FirstOrDefault();
                if (toDelete != null)
                {
                    toDelete.CzyAktywny = false;
                    toDelete.DataUsuniecia = DateTime.Now;
                    toDelete.KtoUsunalId = 1;
                    JJFirmaEntities.SaveChanges();
                    Load();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz usunąć");
            }
        }
        #endregion

    }
}
