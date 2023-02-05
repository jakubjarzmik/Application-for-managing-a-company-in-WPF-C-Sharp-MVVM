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
    class KontrahenciRodzajeViewModel : WszystkieViewModel<KontrahenciRodzaje>
    {
        #region Konstruktor
        public KontrahenciRodzajeViewModel() 
            : base("Rodzaje kontrahentów")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<KontrahenciRodzaje>
                (
                    from rodzaj in Db.KontrahenciRodzaje
                    where rodzaj.CzyAktywny == true
                    select rodzaj
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyRodzajKontrahentaViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.KontrahenciRodzaje.Where(a => a.RodzajId == Selected.RodzajId).FirstOrDefault();
                Messenger.Default.Send(new NowyRodzajKontrahentaViewModel(toEdit));
                Messenger.Default.Register<KontrahenciRodzaje>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(KontrahenciRodzaje edited)
        {
            edited.DataMod = DateTime.Now;
            edited.KtoModId = 1;
            Db.SaveChanges();
            Load();
        }
        public override void Delete()
        {
            try
            {
                var toDelete = Db.KontrahenciRodzaje.Where(a => a.RodzajId == Selected.RodzajId).FirstOrDefault();
                if (toDelete != null)
                {
                    toDelete.CzyAktywny = false;
                    toDelete.DataUsuniecia = DateTime.Now;
                    toDelete.KtoUsunalId = 1;
                    Db.SaveChanges();
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
