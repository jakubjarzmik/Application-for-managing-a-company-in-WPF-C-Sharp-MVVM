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
                    from typ in Db.MagazynyTypy
                    where typ.CzyAktywny == true
                    select typ
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyTypMagazynuViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.MagazynyTypy.Where(a => a.TypMagazynuId == Selected.TypMagazynuId).FirstOrDefault();
                Messenger.Default.Send(new NowyTypMagazynuViewModel(toEdit));
                Messenger.Default.Register<MagazynyTypy>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(MagazynyTypy edited)
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
                var toDelete = Db.MagazynyTypy.Where(a => a.TypMagazynuId == Selected.TypMagazynuId).FirstOrDefault();
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
                    List = new ObservableCollection<MagazynyTypy>(List.OrderBy(Item => Item.TypMagazynuId));
                    break;
                case "Symbol":
                    List = new ObservableCollection<MagazynyTypy>(List.OrderBy(Item => Item.Symbol));
                    break;
                case "Nazwa":
                    List = new ObservableCollection<MagazynyTypy>(List.OrderBy(Item => Item.Nazwa));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Symbol", "Nazwa" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Symbol":
                        List = new ObservableCollection<MagazynyTypy>(List.Where(i => i.Symbol != null && i.Symbol.StartsWith(FindTextBox)));
                        break;
                    case "Nazwa":
                        List = new ObservableCollection<MagazynyTypy>(List.Where(i => i.Nazwa != null && i.Nazwa.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Symbol", "Nazwa" };
        }
        #endregion
    }
}
