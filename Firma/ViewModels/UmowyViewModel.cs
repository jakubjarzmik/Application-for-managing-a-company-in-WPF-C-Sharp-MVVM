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
    public class UmowyViewModel : WszystkieViewModel<UmowaForAllView>
    {
        #region Konstruktor
        public UmowyViewModel():base("Umowy")
        {
        }
        public UmowyViewModel(string token):base("Umowy", token)
        {
        }
        #endregion
        #region Properties
        public override UmowaForAllView Selected
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
            List = new ObservableCollection<UmowaForAllView>
                (
                    from umowa in Db.Umowy
                    where umowa.CzyAktywny == true
                    select new UmowaForAllView
                    {
                        UmowaId= umowa.UmowaId,
                        Numer = umowa.NrUmowy,
                        Rodzaj = umowa.UmowyRodzaje.Nazwa,
                        Stanowisko = umowa.UmowyStanowiska.Nazwa,
                        DataZawarcia = umowa.DataZawarcia,
                        DataOd = umowa.DataOd,
                        DataDo = umowa.DataDo,
                        StawkaMies = umowa.StawkaBruttoMies,
                        StawkaGodz = umowa.StawkaBruttoGodz,
                        CzasPracyMies = umowa.CzasPracyMies,
                        Opis = umowa.Opis,
                        JestEmerytalne = umowa.JestEmerytalne ? "Tak" : "Nie",
                        JestRentowe = umowa.JestRentowe ? "Tak" : "Nie",
                        JestChorobowe = umowa.JestChorobowe ? "Tak" : "Nie",
                        JestWypadkowe = umowa.JestWypadkowe ? "Tak" : "Nie"
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowaUmowaViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.Umowy.Where(a => a.UmowaId == Selected.UmowaId).FirstOrDefault();
                Messenger.Default.Send(new NowaUmowaViewModel(toEdit));
                Messenger.Default.Register<Umowy>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Umowy edited)
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
                var toDelete = Db.Umowy.Where(a => a.UmowaId == Selected.UmowaId).FirstOrDefault();
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
                    List = new ObservableCollection<UmowaForAllView>(List.OrderBy(Item => Item.UmowaId));
                    break;
                case "Numer":
                    List = new ObservableCollection<UmowaForAllView>(List.OrderBy(Item => Item.Numer));
                    break;
                case "Data zawarcia":
                    List = new ObservableCollection<UmowaForAllView>(List.OrderBy(Item => Item.DataZawarcia));
                    break;
                case "Poczatek umowy":
                    List = new ObservableCollection<UmowaForAllView>(List.OrderBy(Item => Item.DataOd));
                    break;
                case "Koniec umowy":
                    List = new ObservableCollection<UmowaForAllView>(List.OrderBy(Item => Item.DataDo));
                    break;
                case "Stawka (mies)":
                    List = new ObservableCollection<UmowaForAllView>(List.OrderBy(Item => Item.StawkaMies));
                    break;
                case "Stawka (godz)":
                    List = new ObservableCollection<UmowaForAllView>(List.OrderBy(Item => Item.StawkaGodz));
                    break;
                case "Czas pracy (mies)":
                    List = new ObservableCollection<UmowaForAllView>(List.OrderBy(Item => Item.CzasPracyMies));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Numer", "Data zawarcia", "Poczatek umowy", "Koniec umowy", "Stawka (mies)", "Stawka (godz)", "Czas pracy (mies)" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Numer":
                        List = new ObservableCollection<UmowaForAllView>(List.Where(i => i.Numer != null && i.Numer.StartsWith(FindTextBox)));
                        break;
                    case "Rodzaj":
                        List = new ObservableCollection<UmowaForAllView>(List.Where(i => i.Rodzaj != null && i.Rodzaj.StartsWith(FindTextBox)));
                        break;
                    case "Stanowisko":
                        List = new ObservableCollection<UmowaForAllView>(List.Where(i => i.Stanowisko != null && i.Stanowisko.StartsWith(FindTextBox)));
                        break;
                    case "Czas pracy (mies)":
                        List = new ObservableCollection<UmowaForAllView>(List.Where(i => i.CzasPracyMies == int.Parse(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Numer", "Rodzaj", "Stanowisko", "Czas pracy (mies)" };
        }
        #endregion
    }
}
