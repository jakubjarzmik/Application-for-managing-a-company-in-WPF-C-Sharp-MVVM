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
    public class FakturyWydaniaZewnetrzneViewModel : WszystkieViewModel<FakturyWydaniaZewnetrzneForAllView>
    {
        #region Konstruktor
        public FakturyWydaniaZewnetrzneViewModel():base("Faktury WZ")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<FakturyWydaniaZewnetrzneForAllView>
                (
                    from fakturawz in Db.FakturyWydaniaZewnetrzne
                    where fakturawz.CzyAktywny == true
                    select new FakturyWydaniaZewnetrzneForAllView
                    {
                        FakturaWZId = fakturawz.FakturaWZId,
                        FakturaId = fakturawz.FakturaId,
                        FakturaNumer = fakturawz.Faktury.Numer,
                        FakturaKontrahentNazwa = fakturawz.Faktury.Kontrahenci.Nazwa1,
                        WydanieZewnetrzneId = fakturawz.WydanieZewnetrzneId,
                        WZNumer = fakturawz.WydaniaZewnetrzne.Numer,
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowaFakturaWydanieZewnetrzneViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.FakturyWydaniaZewnetrzne.Where(a => a.FakturaWZId == Selected.FakturaWZId).FirstOrDefault();
                Messenger.Default.Send(new NowaFakturaWydanieZewnetrzneViewModel(toEdit));
                Messenger.Default.Register<FakturyWydaniaZewnetrzne>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(FakturyWydaniaZewnetrzne edited)
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
                var toDelete = Db.FakturyWydaniaZewnetrzne.Where(a => a.FakturaWZId == Selected.FakturaWZId).FirstOrDefault();
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
                    List = new ObservableCollection<FakturyWydaniaZewnetrzneForAllView>(List.OrderBy(Item => Item.FakturaWZId));
                    break;
                case "Numer faktury":
                    List = new ObservableCollection<FakturyWydaniaZewnetrzneForAllView>(List.OrderBy(Item => Item.FakturaNumer));
                    break;
                case "Nazwa kontrahenta":
                    List = new ObservableCollection<FakturyWydaniaZewnetrzneForAllView>(List.OrderBy(Item => Item.FakturaKontrahentNazwa));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Numer faktury", "Nazwa kontrahenta" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Numer faktury":
                        List = new ObservableCollection<FakturyWydaniaZewnetrzneForAllView>(List.Where(i => i.FakturaNumer != null && i.FakturaNumer.StartsWith(FindTextBox)));
                        break;
                    case "Nazwa kontrahenta":
                        List = new ObservableCollection<FakturyWydaniaZewnetrzneForAllView>(List.Where(i => i.FakturaKontrahentNazwa != null && i.FakturaKontrahentNazwa.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Numer faktury", "Nazwa kontrahenta" };
        }
        #endregion
    }
}
