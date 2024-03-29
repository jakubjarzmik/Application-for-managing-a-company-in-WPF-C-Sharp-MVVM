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
    internal class KontrahenciRodzajeViewModel : WszystkieViewModel<KontrahenciRodzaje>
    {
        #region Konstruktor

        public KontrahenciRodzajeViewModel()
            : base("Rodzaje kontrahentów")
        {
        }

        #endregion Konstruktor

        #region Helpers

        public override void Add()
        {
            Messenger.Default.Send(new NowyRodzajKontrahentaViewModel());
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

        public override void Load()
        {
            List = new ObservableCollection<KontrahenciRodzaje>
                (
                    from rodzaj in Db.KontrahenciRodzaje
                    where rodzaj.CzyAktywny == true
                    select rodzaj
                );
        }
        private void saveEdit(KontrahenciRodzaje edited)
        {
            edited.DataMod = DateTime.Now;
            edited.KtoModId = 1;
            Db.SaveChanges();
            Load();
        }
        #endregion Helpers

        #region SortAndFind

        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Nazwa":
                        List = new ObservableCollection<KontrahenciRodzaje>(List.Where(i => i.Nazwa != null && i.Nazwa.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }

        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Nazwa" };
        }

        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Nazwa" };
        }

        public override void Sort()
        {
            switch (SortField)
            {
                case "Domyślne":
                    List = new ObservableCollection<KontrahenciRodzaje>(List.OrderBy(Item => Item.RodzajId));
                    break;

                case "Nazwa":
                    List = new ObservableCollection<KontrahenciRodzaje>(List.OrderBy(Item => Item.Nazwa));
                    break;
            }
        }
        #endregion SortAndFind
    }
}