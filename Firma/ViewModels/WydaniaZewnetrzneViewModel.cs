using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Firma.ViewModels
{
    public class WydaniaZewnetrzneViewModel : WszystkieViewModel<WydanieZewnetrzneForAllView>
    {
        #region Konstruktor
        public WydaniaZewnetrzneViewModel():base("Wydania Zewnetrzne")
        {
        }
        public WydaniaZewnetrzneViewModel(string token):base("Wydania Zewnetrzne",token)
        {
        }
        #endregion
        #region Properties
        public override WydanieZewnetrzneForAllView Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                if (value != _Selected)
                {
                    _Selected = value;
                    Messenger.Default.Send(_Selected, token);
                    if (toClose)
                        OnRequestClose();
                }
            }
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<WydanieZewnetrzneForAllView>
                (
                    from wz in Db.WydaniaZewnetrzne
                    where wz.CzyAktywny == true
                    select new WydanieZewnetrzneForAllView
                    {
                        WydanieZewnetrzneId= wz.WydanieZewnetrzneId,
                        Numer = wz.Numer,
                        DataWydania = wz.DataWydania,
                        NazwaMagazynu = wz.Magazyny.Nazwa,
                        NazwaKontrahenta = wz.Kontrahenci.Nazwa1,
                        NipKontrahenta = wz.Kontrahenci.Nip
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NoweWydanieZewnetrzneViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.WydaniaZewnetrzne.Where(a => a.WydanieZewnetrzneId == Selected.WydanieZewnetrzneId).FirstOrDefault();
                Messenger.Default.Send(new NoweWydanieZewnetrzneViewModel(toEdit));
                Messenger.Default.Register<WydaniaZewnetrzne>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(WydaniaZewnetrzne edited)
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
                var toDelete = Db.WydaniaZewnetrzne.Where(a => a.WydanieZewnetrzneId == Selected.WydanieZewnetrzneId).FirstOrDefault();
                if (toDelete != null)
                {
                    toDelete.CzyAktywny = false;
                    toDelete.DataUsuniecia = DateTime.Now;
                    toDelete.KtoUsunalId = 1;
                    var pozycjeToDelete = Db.PozycjeWydaniaZewnetrznego.Where(a => a.WydanieZewnetrzneId == toDelete.WydanieZewnetrzneId).ToList();
                    foreach(var pozycja in pozycjeToDelete)
                    {
                        pozycja.CzyAktywny = false;
                        pozycja.DataUsuniecia = DateTime.Now;
                        pozycja.KtoUsunalId = 1;
                    }
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
                    List = new ObservableCollection<WydanieZewnetrzneForAllView>(List.OrderBy(Item => Item.WydanieZewnetrzneId));
                    break;
                case "Data wydania":
                    List = new ObservableCollection<WydanieZewnetrzneForAllView>(List.OrderBy(Item => Item.DataWydania));
                    break;
                case "Magazyn":
                    List = new ObservableCollection<WydanieZewnetrzneForAllView>(List.OrderBy(Item => Item.NazwaMagazynu));
                    break;
                case "Kontrahent":
                    List = new ObservableCollection<WydanieZewnetrzneForAllView>(List.OrderBy(Item => Item.NazwaKontrahenta));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Data wydania", "Magazyn", "Kontrahent" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Data wydania":
                        List = new ObservableCollection<WydanieZewnetrzneForAllView>(List.Where(i => i.DataWydania != null && i.DataWydania.ToString("dd.MM.yyyy HH:mm").StartsWith(FindTextBox)));
                        break;
                    case "Magazyn":
                        List = new ObservableCollection<WydanieZewnetrzneForAllView>(List.Where(i => i.NazwaMagazynu != null && i.NazwaMagazynu.StartsWith(FindTextBox)));
                        break;
                    case "Kontrahent":
                        List = new ObservableCollection<WydanieZewnetrzneForAllView>(List.Where(i => i.NazwaKontrahenta != null && i.NazwaKontrahenta.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Data wydania", "Magazyn", "Kontrahent" };
        }
        #endregion
    }
}
