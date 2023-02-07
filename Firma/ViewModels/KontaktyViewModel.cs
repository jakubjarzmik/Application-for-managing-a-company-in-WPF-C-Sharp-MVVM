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
    class KontaktyViewModel : WszystkieViewModel<Kontakty>
    {
        #region Konstruktor
        public KontaktyViewModel() : base("Kontakty")
        {
        }
        public KontaktyViewModel(string token) : base("Kontakty", token)
        {
        }
        #endregion
        #region Properties
        public override Kontakty Selected
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
            List = new ObservableCollection<Kontakty>
                (
                    from kontakt in Db.Kontakty
                    where kontakt.CzyAktywny == true
                    select kontakt
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyKontaktViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.Kontakty.Where(a => a.KontaktId == Selected.KontaktId).FirstOrDefault();
                Messenger.Default.Send(new NowyKontaktViewModel(toEdit));
                Messenger.Default.Register<Kontakty>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Kontakty edited)
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
                var toDelete = Db.Kontakty.Where(a => a.KontaktId == Selected.KontaktId).FirstOrDefault();
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
                    List = new ObservableCollection<Kontakty>(List.OrderBy(Item => Item.KontaktId));
                    break;
                case "Nazwa działu":
                    List = new ObservableCollection<Kontakty>(List.OrderBy(Item => Item.NazwaDzialu));
                    break;
                case "Opis osoby":
                    List = new ObservableCollection<Kontakty>(List.OrderBy(Item => Item.OpisOsoby));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Nazwa działu", "Opis osoby" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Nazwa działu":
                        List = new ObservableCollection<Kontakty>(List.Where(i => i.NazwaDzialu != null && i.NazwaDzialu.StartsWith(FindTextBox)));
                        break;
                    case "Opis osoby":
                        List = new ObservableCollection<Kontakty>(List.Where(i => i.OpisOsoby != null && i.OpisOsoby.StartsWith(FindTextBox)));
                        break;
                    case "Telefon 1":
                        List = new ObservableCollection<Kontakty>(List.Where(i => i.Telefon1 != null && i.Telefon1.StartsWith(FindTextBox)));
                        break;
                    case "Telefon 2":
                        List = new ObservableCollection<Kontakty>(List.Where(i => i.Telefon2 != null && i.Telefon2.StartsWith(FindTextBox)));
                        break;
                    case "Email 1":
                        List = new ObservableCollection<Kontakty>(List.Where(i => i.Email1 != null && i.Email1.StartsWith(FindTextBox)));
                        break;
                    case "Email 2":
                        List = new ObservableCollection<Kontakty>(List.Where(i => i.Email2 != null && i.Email2.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Nazwa działu", "Opis osoby", "Telefon 1", "Telefon 2", "Email 1", "Email 2" };
        }
        #endregion
    }
}
