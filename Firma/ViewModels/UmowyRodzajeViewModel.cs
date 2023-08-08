using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                    from rodzaj in Db.UmowyRodzaje
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
                var toEdit = Db.UmowyRodzaje.Where(a => a.RodzajUmowyId == Selected.RodzajUmowyId).FirstOrDefault();
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
            Db.SaveChanges();
            Load();
        }
        public override void Delete()
        {
            try
            {
                var toDelete = Db.UmowyRodzaje.Where(a => a.RodzajUmowyId == Selected.RodzajUmowyId).FirstOrDefault();
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
        #region SortAndFind
        public override void Sort()
        {
            switch (SortField)
            {
                case "Domyślne":
                    List = new ObservableCollection<UmowyRodzaje>(List.OrderBy(Item => Item.RodzajUmowyId));
                    break;
                case "Kod":
                    List = new ObservableCollection<UmowyRodzaje>(List.OrderBy(Item => Item.Kod));
                    break;
                case "Nazwa":
                    List = new ObservableCollection<UmowyRodzaje>(List.OrderBy(Item => Item.Nazwa));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Kod", "Nazwa" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Kod":
                        List = new ObservableCollection<UmowyRodzaje>(List.Where(i => i.Kod != null && i.Kod.StartsWith(FindTextBox)));
                        break;
                    case "Nazwa":
                        List = new ObservableCollection<UmowyRodzaje>(List.Where(i => i.Nazwa != null && i.Nazwa.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Kod", "Nazwa" };
        }
        #endregion
    }
}
