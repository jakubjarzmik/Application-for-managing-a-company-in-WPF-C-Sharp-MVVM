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
                    from typ in Db.TowaryTypy
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
                var toEdit = Db.TowaryTypy.Where(a => a.TypTowaruId == Selected.TypTowaruId).FirstOrDefault();
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
            Db.SaveChanges();
            Load();
        }
        public override void Delete()
        {
            try
            {
                var toDelete = Db.TowaryTypy.Where(a => a.TypTowaruId == Selected.TypTowaruId).FirstOrDefault();
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
                    List = new ObservableCollection<TowaryTypy>(List.OrderBy(Item => Item.TypTowaruId));
                    break;
                case "Nazwa":
                    List = new ObservableCollection<TowaryTypy>(List.OrderBy(Item => Item.Nazwa));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Nazwa" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Nazwa":
                        List = new ObservableCollection<TowaryTypy>(List.Where(i => i.Nazwa != null && i.Nazwa.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Nazwa" };
        }
        #endregion
    }
}
