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
    class UmowyStanowiskaViewModel : WszystkieViewModel<UmowyStanowiska>
    {
        #region Konstruktor
        public UmowyStanowiskaViewModel() : base("Stanowiska")
        {
        }
        public UmowyStanowiskaViewModel(string token) : base("Stanowiska", token)
        {
        }
        #endregion
        #region Properties
        public override UmowyStanowiska Selected
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
            List = new ObservableCollection<UmowyStanowiska>
                (
                    from stanowisko in Db.UmowyStanowiska
                    where stanowisko.CzyAktywny == true
                    select stanowisko
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NoweStanowiskoViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.UmowyStanowiska.Where(a => a.StanowiskoId == Selected.StanowiskoId).FirstOrDefault();
                Messenger.Default.Send(new NoweStanowiskoViewModel(toEdit));
                Messenger.Default.Register<UmowyStanowiska>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(UmowyStanowiska edited)
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
                var toDelete = Db.UmowyStanowiska.Where(a => a.StanowiskoId == Selected.StanowiskoId).FirstOrDefault();
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
                    List = new ObservableCollection<UmowyStanowiska>(List.OrderBy(Item => Item.StanowiskoId));
                    break;
                case "Kod zawodu":
                    List = new ObservableCollection<UmowyStanowiska>(List.OrderBy(Item => Item.KodZawodu));
                    break;
                case "Nazwa":
                    List = new ObservableCollection<UmowyStanowiska>(List.OrderBy(Item => Item.Nazwa));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Kod zawodu", "Nazwa" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Kod zawodu":
                        List = new ObservableCollection<UmowyStanowiska>(List.Where(i => i.KodZawodu != null && i.KodZawodu.StartsWith(FindTextBox)));
                        break;
                    case "Nazwa":
                        List = new ObservableCollection<UmowyStanowiska>(List.Where(i => i.Nazwa != null && i.Nazwa.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Kod zawodu", "Nazwa" };
        }
        #endregion
    }
}
