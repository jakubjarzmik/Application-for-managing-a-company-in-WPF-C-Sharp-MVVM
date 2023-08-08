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
    public class PracownicyUmowyViewModel : WszystkieViewModel<PracownicyUmowyForAllView>
    {
        #region Konstruktor
        public PracownicyUmowyViewModel():base("Pracownicy umowy")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PracownicyUmowyForAllView>
                (
                    from pracownikUmowa in Db.PracownicyUmowy
                    where pracownikUmowa.CzyAktywny == true
                    select new PracownicyUmowyForAllView
                    {
                        PracownikUmowaId = pracownikUmowa.PracownikUmowaId,
                        PracownikId = pracownikUmowa.PracownikId,
                        PracownikNazwa = pracownikUmowa.Pracownicy.Imie + " " + pracownikUmowa.Pracownicy.Nazwisko
                        + (pracownikUmowa.Pracownicy.NazwiskoRodowe.Equals("") ? "": " ("+pracownikUmowa.Pracownicy.NazwiskoRodowe+")"),
                        PracownikPESEL = pracownikUmowa.Pracownicy.PESEL,
                        UmowaId = pracownikUmowa.UmowaId,
                        UmowaNumer = pracownikUmowa.Umowy.NrUmowy,
                        UmowaRodzaj = pracownikUmowa.Umowy.UmowyRodzaje.Nazwa,
                        UmowaStanowisko = pracownikUmowa.Umowy.UmowyStanowiska.Nazwa,
                        UmowaDataOd = pracownikUmowa.Umowy.DataOd,
                        UmowaDataDo = pracownikUmowa.Umowy.DataDo,
                        UmowaStawkaMies = pracownikUmowa.Umowy.StawkaBruttoMies,
                        UmowaStawkaGodz = pracownikUmowa.Umowy.StawkaBruttoGodz,
                        UmowaCzasPracyMies = pracownikUmowa.Umowy.CzasPracyMies,
                        UmowaWartoscMies = pracownikUmowa.Umowy.StawkaBruttoMies + (pracownikUmowa.Umowy.StawkaBruttoGodz * pracownikUmowa.Umowy.CzasPracyMies)
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyPracownikUmowaViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.PracownicyUmowy.Where(a => a.PracownikUmowaId == Selected.PracownikUmowaId).FirstOrDefault();
                Messenger.Default.Send(new NowyPracownikUmowaViewModel(toEdit));
                Messenger.Default.Register<PracownicyUmowy>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(PracownicyUmowy edited)
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
                var toDelete = Db.PracownicyUmowy.Where(a => a.PracownikUmowaId == Selected.PracownikUmowaId).FirstOrDefault();
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
                    List = new ObservableCollection<PracownicyUmowyForAllView>(List.OrderBy(Item => Item.PracownikUmowaId));
                    break;
                case "Pracownik":
                    List = new ObservableCollection<PracownicyUmowyForAllView>(List.OrderBy(Item => Item.PracownikNazwa));
                    break;
                case "Nr umowy":
                    List = new ObservableCollection<PracownicyUmowyForAllView>(List.OrderBy(Item => Item.UmowaNumer));
                    break;
                case "Data rozpoczęcia":
                    List = new ObservableCollection<PracownicyUmowyForAllView>(List.OrderBy(Item => Item.UmowaDataOd));
                    break;
                case "Data zakończenia":
                    List = new ObservableCollection<PracownicyUmowyForAllView>(List.OrderBy(Item => Item.UmowaDataDo));
                    break;
                case "Wartość (mies)":
                    List = new ObservableCollection<PracownicyUmowyForAllView>(List.OrderBy(Item => Item.UmowaWartoscMies));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Pracownik", "Nr umowy", "Data rozpoczęcia", "Data zakończenia", "Wartość (mies)" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Pracownik":
                        List = new ObservableCollection<PracownicyUmowyForAllView>(List.Where(i => i.PracownikNazwa != null && i.PracownikNazwa.StartsWith(FindTextBox)));
                        break;
                    case "Nr umowy":
                        List = new ObservableCollection<PracownicyUmowyForAllView>(List.Where(i => i.UmowaNumer != null && i.UmowaNumer.StartsWith(FindTextBox)));
                        break;
                    case "Rodzaj umowy":
                        List = new ObservableCollection<PracownicyUmowyForAllView>(List.Where(i => i.UmowaRodzaj != null && i.UmowaRodzaj.StartsWith(FindTextBox)));
                        break;
                    case "Stanowisko":
                        List = new ObservableCollection<PracownicyUmowyForAllView>(List.Where(i => i.UmowaStanowisko != null && i.UmowaStanowisko.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Pracownik", "Nr umowy", "Rodzaj umowy", "Stanowisko" };
        }
        #endregion
    }
}
