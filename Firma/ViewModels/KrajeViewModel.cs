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

namespace Firma.ViewModels
{
    class KrajeViewModel : WszystkieViewModel<Kraje>
    {
        #region Konstruktor
        public KrajeViewModel() : base("Kraje")
        {
        }
        public KrajeViewModel(string token) : base("Kraje", token)
        {
        }
        #endregion
        #region Properties
        public override Kraje Selected
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
            List = new ObservableCollection<Kraje>
                (
                    from kraj in Db.Kraje
                    where kraj.CzyAktywny == true
                    select kraj
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyKrajViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.Kraje.Where(a => a.KrajId == Selected.KrajId).FirstOrDefault();
                Messenger.Default.Send(new NowyKrajViewModel(toEdit));
                Messenger.Default.Register<Kraje>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Kraje edited)
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
                var toDelete = Db.Kraje.Where(a => a.KrajId == Selected.KrajId).FirstOrDefault();
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
                    List = new ObservableCollection<Kraje>(List.OrderBy(Item => Item.KrajId));
                    break;
                case "ISO":
                    List = new ObservableCollection<Kraje>(List.OrderBy(Item => Item.ISO));
                    break;
                case "Nazwa":
                    List = new ObservableCollection<Kraje>(List.OrderBy(Item => Item.Nazwa));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "ISO", "Nazwa" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "ISO":
                        List = new ObservableCollection<Kraje>(List.Where(i => i.ISO != null && i.ISO.StartsWith(FindTextBox)));
                        break;
                    case "Nazwa":
                        List = new ObservableCollection<Kraje>(List.Where(i => i.Nazwa != null && i.Nazwa.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "ISO", "Nazwa" };
        }
        #endregion
    }
}
