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
    public class TowaryViewModel : WszystkieViewModel<TowarForAllView>
    {
        #region Konstruktor
        public TowaryViewModel() : base("Towary")
        {
        }
        public TowaryViewModel(string token) : base("Towary", token)
        {
        }
        #endregion
        #region Properties
        public override TowarForAllView Selected
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
            List = new ObservableCollection<TowarForAllView>
                (
                    from towar in Db.Towary
                    where towar.CzyAktywny == true
                    select new TowarForAllView
                    {
                        TowarId = towar.TowarId,
                        Kod = towar.Kod,
                        Nazwa = towar.Nazwa,
                        Typ = towar.TowaryTypy.Nazwa,
                        Grupa = towar.TowaryGrupy.Nazwa,
                        NumerKatalogowy = towar.NumerKatalogowy,
                        EAN = towar.EAN,
                        Producent = towar.Kontrahenci.Nazwa1,
                        KrajPochodzenia = towar.Kraje.ISO,
                        Ilosc = (
                                from pozycjaPZ in Db.PozycjePrzyjeciaZewnetrznego
                                where pozycjaPZ.CzyAktywny == true && pozycjaPZ.Towary.TowarId == towar.TowarId
                                select pozycjaPZ.Ilosc
                            ).Sum() -
                            (
                                from pozycjaWZ in Db.PozycjeWydaniaZewnetrznego
                                where pozycjaWZ.CzyAktywny == true && pozycjaWZ.Towary.TowarId == towar.TowarId
                                select pozycjaWZ.Ilosc
                            ).Sum(),
                        JednMiary = towar.JednostkiMiary.Skrot
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyTowarViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.Towary.Where(a => a.TowarId == Selected.TowarId).FirstOrDefault();
                Messenger.Default.Send(new NowyTowarViewModel(toEdit));
                Messenger.Default.Register<Towary>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Towary edited)
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
                var toDelete = Db.Towary.Where(a => a.TowarId == Selected.TowarId).FirstOrDefault();
                if (toDelete != null)
                {
                    toDelete.CzyAktywny = false;
                    toDelete.DataUsuniecia = DateTime.Now;
                    toDelete.KtoUsunalId = 1;
                    var cenyToDelete = Db.ZmianyCeny.Where(a => a.TowarId == toDelete.TowarId).ToList();
                    foreach (var cena in cenyToDelete)
                    {
                        cena.CzyAktywny = false;
                        cena.DataUsuniecia = DateTime.Now;
                        cena.KtoUsunalId = 1;
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
                    List = new ObservableCollection<TowarForAllView>(List.OrderBy(Item => Item.TowarId));
                    break;
                case "Kod":
                    List = new ObservableCollection<TowarForAllView>(List.OrderBy(Item => Item.Kod));
                    break;
                case "Nazwa":
                    List = new ObservableCollection<TowarForAllView>(List.OrderBy(Item => Item.Nazwa));
                    break;
                case "Grupa":
                    List = new ObservableCollection<TowarForAllView>(List.OrderBy(Item => Item.Grupa));
                    break;
                case "Producent":
                    List = new ObservableCollection<TowarForAllView>(List.OrderBy(Item => Item.Producent));
                    break;
                case "Kraj pochodzenia":
                    List = new ObservableCollection<TowarForAllView>(List.OrderBy(Item => Item.KrajPochodzenia));
                    break;
                case "Ilosc":
                    List = new ObservableCollection<TowarForAllView>(List.OrderBy(Item => Item.Ilosc));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Kod", "Nazwa", "Grupa", "Producent", "Kraj pochodzenia", "Ilosc" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Kod":
                        List = new ObservableCollection<TowarForAllView>(List.Where(i => i.Kod != null && i.Kod.StartsWith(FindTextBox)));
                        break;
                    case "Nazwa":
                        List = new ObservableCollection<TowarForAllView>(List.Where(i => i.Nazwa != null && i.Nazwa.StartsWith(FindTextBox)));
                        break;
                    case "Typ":
                        List = new ObservableCollection<TowarForAllView>(List.Where(i => i.Typ != null && i.Typ.StartsWith(FindTextBox)));
                        break;
                    case "Grupa":
                        List = new ObservableCollection<TowarForAllView>(List.Where(i => i.Grupa != null && i.Grupa.StartsWith(FindTextBox)));
                        break;
                    case "Numer katalogowy":
                        List = new ObservableCollection<TowarForAllView>(List.Where(i => i.NumerKatalogowy != null && i.NumerKatalogowy.StartsWith(FindTextBox)));
                        break;
                    case "Producent":
                        List = new ObservableCollection<TowarForAllView>(List.Where(i => i.Producent != null && i.Producent.StartsWith(FindTextBox)));
                        break;
                    case "Kraj pochodzenia":
                        List = new ObservableCollection<TowarForAllView>(List.Where(i => i.KrajPochodzenia != null && i.KrajPochodzenia.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Kod", "Nazwa", "Typ", "Grupa", "Numer katalogowy", "Producent", "Kraj pochodzenia" };
        }
        #endregion
    }
}
