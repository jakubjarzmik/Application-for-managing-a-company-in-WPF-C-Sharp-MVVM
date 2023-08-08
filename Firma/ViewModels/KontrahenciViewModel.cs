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
    public class KontrahenciViewModel : WszystkieViewModel<KontrahentForAllView>
    {
        #region Konstruktor
        public KontrahenciViewModel():base("Kontrahenci")
        {
        }
        public KontrahenciViewModel(string token):base("Kontrahenci", token)
        {
        }
        #endregion
        #region Properties
        public override KontrahentForAllView Selected
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
            List = new ObservableCollection<KontrahentForAllView>
                (
                    from kontrahent in Db.Kontrahenci
                    where kontrahent.CzyAktywny == true
                    select new KontrahentForAllView
                    {
                        KontrahentId = kontrahent.KontrahentId,
                        Kod = kontrahent.Kod,
                        Nazwa1 = kontrahent.Nazwa1,
                        RodzajKontrahenta = kontrahent.KontrahenciRodzaje.Nazwa,
                        Nip = kontrahent.Nip,
                        Regon = kontrahent.Regon,
                        Adres = kontrahent.Adresy.Ulica + " " + kontrahent.Adresy.NrDomu +
                        (kontrahent.Adresy.NrLokalu.Equals("") ? "":"/"+ kontrahent.Adresy.NrLokalu)+
                        "\n" + kontrahent.Adresy.KodPocztowy + " " + kontrahent.Adresy.Miejscowosc,
                        Url = kontrahent.URL,
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyKontrahentViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.Kontrahenci.Where(a => a.KontrahentId == Selected.KontrahentId).FirstOrDefault();
                Messenger.Default.Send(new NowyKontrahentViewModel(toEdit));
                Messenger.Default.Register<Kontrahenci>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Kontrahenci edited)
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
                var toDelete = Db.Kontrahenci.Where(a => a.KontrahentId == Selected.KontrahentId).FirstOrDefault();
                if (toDelete != null)
                {
                    toDelete.CzyAktywny = false;
                    toDelete.DataUsuniecia = DateTime.Now;
                    toDelete.KtoUsunalId = 1;
                    var kontaktyToDelete = Db.KontrahenciKontakty.Where(a => a.KontrahentId == toDelete.KontrahentId).ToList();
                    foreach (var kontakt in kontaktyToDelete)
                    {
                        kontakt.CzyAktywny = false;
                        kontakt.DataUsuniecia = DateTime.Now;
                        kontakt.KtoUsunalId = 1;
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
                    List = new ObservableCollection<KontrahentForAllView>(List.OrderBy(Item => Item.KontrahentId));
                    break;
                case "Kod":
                    List = new ObservableCollection<KontrahentForAllView>(List.OrderBy(Item => Item.Kod));
                    break;
                case "Nazwa":
                    List = new ObservableCollection<KontrahentForAllView>(List.OrderBy(Item => Item.Nazwa1));
                    break;
                case "Rodzaj":
                    List = new ObservableCollection<KontrahentForAllView>(List.OrderBy(Item => Item.RodzajKontrahenta));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Kod", "Nazwa", "Rodzaj" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Kod":
                        List = new ObservableCollection<KontrahentForAllView>(List.Where(i => i.Kod != null && i.Kod.StartsWith(FindTextBox)));
                        break;
                    case "Nazwa":
                        List = new ObservableCollection<KontrahentForAllView>(List.Where(i => i.Nazwa1 != null && i.Nazwa1.StartsWith(FindTextBox)));
                        break;
                    case "Rodzaj":
                        List = new ObservableCollection<KontrahentForAllView>(List.Where(i => i.RodzajKontrahenta != null && i.RodzajKontrahenta.StartsWith(FindTextBox)));
                        break;
                    case "Nip":
                        List = new ObservableCollection<KontrahentForAllView>(List.Where(i => i.Nip != null && i.Nip.StartsWith(FindTextBox)));
                        break;
                    case "Regon":
                        List = new ObservableCollection<KontrahentForAllView>(List.Where(i => i.Regon != null && i.Regon.StartsWith(FindTextBox)));
                        break;
                    case "Adres":
                        List = new ObservableCollection<KontrahentForAllView>(List.Where(i => i.Adres != null && i.Adres.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Kod", "Nazwa", "Rodzaj", "Nip", "Regon", "Adres" };
        }
        #endregion
    }
}
