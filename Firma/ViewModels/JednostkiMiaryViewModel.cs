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
    class JednostkiMiaryViewModel : WszystkieViewModel<JednostkiMiary>
    {
        #region Konstruktor
        public JednostkiMiaryViewModel() : base("Jednostki miary")
        {
        }
        public JednostkiMiaryViewModel(string token) : base("Jednostki miary", token)
        {
        }
        #endregion
        #region Properties
        public override JednostkiMiary Selected
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
            List = new ObservableCollection<JednostkiMiary>
                (
                    from jednmiary in Db.JednostkiMiary
                    where jednmiary.CzyAktywny == true
                    select jednmiary
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowaJednostkaMiaryViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.JednostkiMiary.Where(a => a.JednostkaId == Selected.JednostkaId).FirstOrDefault();
                Messenger.Default.Send(new NowaJednostkaMiaryViewModel(toEdit));
                Messenger.Default.Register<JednostkiMiary>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(JednostkiMiary edited)
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
                var toDelete = Db.JednostkiMiary.Where(a => a.JednostkaId == Selected.JednostkaId).FirstOrDefault();
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
                    List = new ObservableCollection<JednostkiMiary>(List.OrderBy(Item => Item.JednostkaId));
                    break;
                case "Skrót":
                    List = new ObservableCollection<JednostkiMiary>(List.OrderBy(Item => Item.Skrot));
                    break;
                case "Nazwa":
                    List = new ObservableCollection<JednostkiMiary>(List.OrderBy(Item => Item.Nazwa));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Skrót", "Nazwa" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Skrót":
                        List = new ObservableCollection<JednostkiMiary>(List.Where(i => i.Skrot != null && i.Skrot.StartsWith(FindTextBox)));
                        break;
                    case "Nazwa":
                        List = new ObservableCollection<JednostkiMiary>(List.Where(i => i.Nazwa != null && i.Nazwa.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Skrót", "Nazwa" };
        }
        #endregion
    }
}
