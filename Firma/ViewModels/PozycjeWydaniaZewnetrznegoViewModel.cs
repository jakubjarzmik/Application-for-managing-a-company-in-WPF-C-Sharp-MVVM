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
    public class PozycjeWydaniaZewnetrznegoViewModel : WszystkieViewModel<PozycjaWydaniaZewnetrznegoForAllView>
    {
        #region Konstruktor
        public PozycjeWydaniaZewnetrznegoViewModel():base("Pozycje WZ")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PozycjaWydaniaZewnetrznegoForAllView>
                (
                    from pozycja in Db.PozycjeWydaniaZewnetrznego
                    where pozycja.CzyAktywny == true
                    select new PozycjaWydaniaZewnetrznegoForAllView
                    {
                        PozycjaWZId = pozycja.PozycjaWZId,
                        NumerWydaniaZewnetrznego = pozycja.WydaniaZewnetrzne.Numer,
                        NazwaKontrahenta = pozycja.WydaniaZewnetrzne.Kontrahenci.Nazwa1,
                        NazwaTowaru = pozycja.Towary.Nazwa,
                        Ilosc = pozycja.Ilosc,
                        JednostkaMiary = pozycja.JednostkiMiary.Skrot,
                        Rabat = pozycja.Rabat,
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowaPozycjaWydaniaZewnetrznegoViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.PozycjeWydaniaZewnetrznego.Where(a => a.PozycjaWZId == Selected.PozycjaWZId).FirstOrDefault();
                Messenger.Default.Send(new NowaPozycjaWydaniaZewnetrznegoViewModel(toEdit));
                Messenger.Default.Register<PozycjeWydaniaZewnetrznego>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(PozycjeWydaniaZewnetrznego edited)
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
                var toDelete = Db.PozycjeWydaniaZewnetrznego.Where(a => a.PozycjaWZId == Selected.PozycjaWZId).FirstOrDefault();
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
                    List = new ObservableCollection<PozycjaWydaniaZewnetrznegoForAllView>(List.OrderBy(Item => Item.PozycjaWZId));
                    break;
                case "Numer WZ":
                    List = new ObservableCollection<PozycjaWydaniaZewnetrznegoForAllView>(List.OrderBy(Item => Item.NumerWydaniaZewnetrznego));
                    break;
                case "Kontrahent":
                    List = new ObservableCollection<PozycjaWydaniaZewnetrznegoForAllView>(List.OrderBy(Item => Item.NazwaKontrahenta));
                    break;
                case "Towar":
                    List = new ObservableCollection<PozycjaWydaniaZewnetrznegoForAllView>(List.OrderBy(Item => Item.NazwaTowaru));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Numer WZ", "Kontrahent", "Towar" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Numer WZ":
                        List = new ObservableCollection<PozycjaWydaniaZewnetrznegoForAllView>(List.Where(i => i.NumerWydaniaZewnetrznego != null && i.NumerWydaniaZewnetrznego.StartsWith(FindTextBox)));
                        break;
                    case "Kontrahent":
                        List = new ObservableCollection<PozycjaWydaniaZewnetrznegoForAllView>(List.Where(i => i.NazwaKontrahenta != null && i.NazwaKontrahenta.StartsWith(FindTextBox)));
                        break;
                    case "Towar":
                        List = new ObservableCollection<PozycjaWydaniaZewnetrznegoForAllView>(List.Where(i => i.NazwaTowaru != null && i.NazwaTowaru.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Numer WZ", "Kontrahent", "Towar" };
        }
        #endregion
    }
}
