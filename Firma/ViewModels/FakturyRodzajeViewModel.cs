﻿using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Firma.ViewModels
{
    public class FakturyRodzajeViewModel : WszystkieViewModel<FakturyRodzaje>
    {
        #region Konstruktor
        public FakturyRodzajeViewModel()
            :base("Rodzaje faktur")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<FakturyRodzaje>(
                from rodzaj in Db.FakturyRodzaje
                where rodzaj.CzyAktywny == true
                select rodzaj
                ) ;
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyRodzajFakturyViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.FakturyRodzaje.Where(a => a.RodzajFakturyId == Selected.RodzajFakturyId).FirstOrDefault();
                Messenger.Default.Send(new NowyRodzajFakturyViewModel(toEdit));
                Messenger.Default.Register<FakturyRodzaje>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(FakturyRodzaje edited)
        {
            edited.DataModyfikacji = DateTime.Now;
            edited.KtoModId = 1;
            Db.SaveChanges();
            Load();
        }
        public override void Delete()
        {
            try
            {
                var toDelete = Db.FakturyRodzaje.Where(a => a.RodzajFakturyId == Selected.RodzajFakturyId).FirstOrDefault();
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
                    List = new ObservableCollection<FakturyRodzaje>(List.OrderBy(Item => Item.RodzajFakturyId));
                    break;
                case "Kod":
                    List = new ObservableCollection<FakturyRodzaje>(List.OrderBy(Item => Item.Kod));
                    break;
                case "Nazwa":
                    List = new ObservableCollection<FakturyRodzaje>(List.OrderBy(Item => Item.Nazwa));
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
                        List = new ObservableCollection<FakturyRodzaje>(List.Where(i => i.Kod != null && i.Kod.StartsWith(FindTextBox)));
                        break;
                    case "Nazwa":
                        List = new ObservableCollection<FakturyRodzaje>(List.Where(i => i.Nazwa != null && i.Nazwa.StartsWith(FindTextBox)));
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
