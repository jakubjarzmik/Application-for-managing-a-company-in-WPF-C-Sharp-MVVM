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
    class KontaktyViewModel : WszystkieViewModel<Kontakty>
    {
        #region Konstruktor
        public KontaktyViewModel() 
            : base("Kontakty")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Kontakty>
                (
                    from kontakt in JJFirmaEntities.Kontakty
                    where kontakt.CzyAktywny == true
                    select kontakt
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyKontaktViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.Kontakty.Where(a => a.KontaktId == Selected.KontaktId).FirstOrDefault();
                Messenger.Default.Send(new NowyKontaktViewModel(toEdit));
                Messenger.Default.Register<Kontakty>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Kontakty edited)
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
                var toDelete = JJFirmaEntities.Kontakty.Where(a => a.KontaktId == Selected.KontaktId).FirstOrDefault();
                if (toDelete != null)
                {
                    toDelete.CzyAktywny = false;
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
