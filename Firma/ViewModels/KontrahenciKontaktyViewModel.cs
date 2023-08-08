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
    public class KontrahenciKontaktyViewModel : WszystkieViewModel<KontrahenciKontaktyForAllView>
    {
        #region Konstruktor
        public KontrahenciKontaktyViewModel() : base("Kontrahenci kontakty")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<KontrahenciKontaktyForAllView>
                (
                    from kontrahentKontakt in Db.KontrahenciKontakty
                    where kontrahentKontakt.CzyAktywny == true
                    select new KontrahenciKontaktyForAllView
                    {
                        KontrahentKontaktId = kontrahentKontakt.KontrahentKontaktId,
                        KontrahentId = kontrahentKontakt.KontrahentId,
                        KontrahentKod = kontrahentKontakt.Kontrahenci.Kod,
                        KontrahentNazwa = kontrahentKontakt.Kontrahenci.Nazwa1,
                        KontrahentNip = kontrahentKontakt.Kontrahenci.Nip,
                        KontaktId = kontrahentKontakt.KontaktId,
                        KontaktNazwaDzialu = kontrahentKontakt.Kontakty.NazwaDzialu,
                        KontaktOpisOsoby = kontrahentKontakt.Kontakty.OpisOsoby,
                        KontaktTelefon1 = kontrahentKontakt.Kontakty.Telefon1,
                        KontaktTelefon2 = kontrahentKontakt.Kontakty.Telefon2,
                        KontaktEmail1 = kontrahentKontakt.Kontakty.Email1,
                        KontaktEmail2 = kontrahentKontakt.Kontakty.Email2,
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyKontrahentKontaktViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.KontrahenciKontakty.Where(a => a.KontrahentKontaktId == Selected.KontrahentKontaktId).FirstOrDefault();
                Messenger.Default.Send(new NowyKontrahentKontaktViewModel(toEdit));
                Messenger.Default.Register<KontrahenciKontakty>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(KontrahenciKontakty edited)
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
                var toDelete = Db.KontrahenciKontakty.Where(a => a.KontrahentKontaktId == Selected.KontrahentKontaktId).FirstOrDefault();
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
                    List = new ObservableCollection<KontrahenciKontaktyForAllView>(List.OrderBy(Item => Item.KontrahentKontaktId));
                    break;
                case "Nazwa kontrahenta":
                    List = new ObservableCollection<KontrahenciKontaktyForAllView>(List.OrderBy(Item => Item.KontrahentNazwa));
                    break;
                case "Nazwa działu":
                    List = new ObservableCollection<KontrahenciKontaktyForAllView>(List.OrderBy(Item => Item.KontaktNazwaDzialu));
                    break;
                case "Opis osoby":
                    List = new ObservableCollection<KontrahenciKontaktyForAllView>(List.OrderBy(Item => Item.KontaktOpisOsoby));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Nazwa kontrahenta", "Nazwa działu", "Opis osoby" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Nazwa kontrahenta":
                        List = new ObservableCollection<KontrahenciKontaktyForAllView>(List.Where(i => i.KontrahentNazwa != null && i.KontrahentNazwa.StartsWith(FindTextBox)));
                        break;
                    case "Nazwa działu":
                        List = new ObservableCollection<KontrahenciKontaktyForAllView>(List.Where(i => i.KontaktNazwaDzialu != null && i.KontaktNazwaDzialu.StartsWith(FindTextBox)));
                        break;
                    case "Opis osoby":
                        List = new ObservableCollection<KontrahenciKontaktyForAllView>(List.Where(i => i.KontaktOpisOsoby != null && i.KontaktOpisOsoby.StartsWith(FindTextBox)));
                        break;
                    case "Telefon 1":
                        List = new ObservableCollection<KontrahenciKontaktyForAllView>(List.Where(i => i.KontaktTelefon1 != null && i.KontaktTelefon1.StartsWith(FindTextBox)));
                        break;
                    case "Telefon 2":
                        List = new ObservableCollection<KontrahenciKontaktyForAllView>(List.Where(i => i.KontaktTelefon2 != null && i.KontaktTelefon2.StartsWith(FindTextBox)));
                        break;
                    case "Email 1":
                        List = new ObservableCollection<KontrahenciKontaktyForAllView>(List.Where(i => i.KontaktEmail1 != null && i.KontaktEmail1.StartsWith(FindTextBox)));
                        break;
                    case "Email 2":
                        List = new ObservableCollection<KontrahenciKontaktyForAllView>(List.Where(i => i.KontaktEmail2 != null && i.KontaktEmail2.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Nazwa kontrahenta", "Nazwa działu", "Opis osoby", "Telefon 1", "Telefon 2", "Email 1", "Email 2" };
        }
        #endregion
    }
}
