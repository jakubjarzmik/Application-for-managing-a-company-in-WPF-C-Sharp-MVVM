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
    public class FakturyViewModel : WszystkieViewModel<FakturaForAllView>
    {
        #region Konstruktor
        public FakturyViewModel():base("Faktury")
        {
        }
        public FakturyViewModel(string token) :base("Faktury", token)
        {
        }
        #endregion
        #region Properties
        public override FakturaForAllView Selected
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
            List = new ObservableCollection<FakturaForAllView>
                (
                    from faktura in Db.Faktury
                    where faktura.CzyAktywny == true
                    select new FakturaForAllView
                    {
                        FakturaId = faktura.FakturaId,
                        Numer = faktura.Numer,
                        DataWystawienia = faktura.DataWystawienia,
                        KontrahentNazwa = faktura.Kontrahenci.Nazwa1,
                        KontrahentNip = faktura.Kontrahenci.Nip,
                        RodzajePlatnosciNazwa = faktura.RodzajePlatnosci.Nazwa
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowaFakturaViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.Faktury.Where(a => a.FakturaId == Selected.FakturaId).FirstOrDefault();
                Messenger.Default.Send(new NowaFakturaViewModel(toEdit));
                Messenger.Default.Register<Faktury>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Faktury edited)
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
                var toDelete = Db.Faktury.Where(a => a.FakturaId == Selected.FakturaId).FirstOrDefault();
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
                    List = new ObservableCollection<FakturaForAllView>(List.OrderBy(Item => Item.FakturaId));
                    break;
                case "Data wystawienia":
                    List = new ObservableCollection<FakturaForAllView>(List.OrderBy(Item => Item.DataWystawienia));
                    break;
                case "Nazwa kontrahenta":
                    List = new ObservableCollection<FakturaForAllView>(List.OrderBy(Item => Item.KontrahentNazwa));
                    break;
                case "Rodzaj płatności":
                    List = new ObservableCollection<FakturaForAllView>(List.OrderBy(Item => Item.RodzajePlatnosciNazwa));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Data wystawienia", "Nazwa kontrahenta", "Rodzaj płatności" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Data wystawienia":
                        List = new ObservableCollection<FakturaForAllView>(List.Where(i => i.DataWystawienia != null && i.DataWystawienia.ToString("dd.MM.yyyy HH:mm").StartsWith(FindTextBox)));
                        break;
                    case "Nazwa kontrahenta":
                        List = new ObservableCollection<FakturaForAllView>(List.Where(i => i.KontrahentNazwa != null && i.KontrahentNazwa.StartsWith(FindTextBox)));
                        break;
                    case "Rodzaj płatności":
                        List = new ObservableCollection<FakturaForAllView>(List.Where(i => i.RodzajePlatnosciNazwa != null && i.RodzajePlatnosciNazwa.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Data wystawienia", "Nazwa kontrahenta", "Rodzaj płatności" };
        }
        #endregion
    }
}
