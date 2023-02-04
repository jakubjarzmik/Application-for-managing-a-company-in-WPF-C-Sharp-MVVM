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
    class UmowyRodzajeViewModel : WszystkieViewModel<UmowyRodzaje>
    {
        #region Konstruktor
        public UmowyRodzajeViewModel() 
            : base("Rodzaje umowy")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<UmowyRodzaje>
                (
                    from rodzaj in JJFirmaEntities.UmowyRodzaje
                    where rodzaj.CzyAktywny == true
                    select rodzaj
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyRodzajUmowyViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.UmowyRodzaje.Where(a => a.RodzajUmowyId == Selected.RodzajUmowyId).FirstOrDefault();
                Messenger.Default.Send(new NowyRodzajUmowyViewModel(toEdit));
                Messenger.Default.Register<UmowyRodzaje>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(UmowyRodzaje edited)
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
                var toDelete = JJFirmaEntities.UmowyRodzaje.Where(a => a.RodzajUmowyId == Selected.RodzajUmowyId).FirstOrDefault();
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
