using Firma.Helpers;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Firma.ViewModels
{
    internal class FakturyKategorieViewModel : WszystkieViewModel<FakturyKategorie>
    {

        #region Konstruktor
        public FakturyKategorieViewModel()
            : base("Kategorie faktur")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<FakturyKategorie>(
                from faktura in Db.FakturyKategorie
                where faktura.CzyAktywny == true
                select faktura
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowaKategoriaFakturyViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.FakturyKategorie.Where(a => a.KategoriaFakturyId == Selected.KategoriaFakturyId).FirstOrDefault();
                Messenger.Default.Send(new NowaKategoriaFakturyViewModel(toEdit));
                Messenger.Default.Register<FakturyKategorie>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(FakturyKategorie edited)
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
                var toDelete = Db.FakturyKategorie.Where(a => a.KategoriaFakturyId == Selected.KategoriaFakturyId).FirstOrDefault();
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
