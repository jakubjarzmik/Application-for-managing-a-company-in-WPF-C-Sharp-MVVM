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
    public class MagazynyViewModel : WszystkieViewModel<MagazynForAllView>
    {
        #region Konstruktor
        public MagazynyViewModel() : base("Magazyny")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<MagazynForAllView>
                (
                    from magazyn in Db.Magazyny
                    where magazyn.CzyAktywny == true
                    select new MagazynForAllView
                    {
                        MagazynId = magazyn.MagazynId,
                        Symbol = magazyn.Symbol,
                        Nazwa = magazyn.Nazwa,
                        Opis = magazyn.Opis,
                        Typ = magazyn.MagazynyTypy.Nazwa,
                        Adres = magazyn.Adresy.Ulica + " " + magazyn.Adresy.NrDomu +
                        (magazyn.Adresy.NrLokalu.Equals("") ? "" : "/" + magazyn.Adresy.NrLokalu) +
                        "\n" + magazyn.Adresy.KodPocztowy + " " + magazyn.Adresy.Miejscowosc,
                        Telefon = magazyn.Telefon
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyMagazynViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.Magazyny.Where(a => a.MagazynId == Selected.MagazynId).FirstOrDefault();
                Messenger.Default.Send(new NowyMagazynViewModel(toEdit));
                Messenger.Default.Register<Magazyny>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Magazyny edited)
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
                var toDelete = Db.Magazyny.Where(a => a.MagazynId == Selected.MagazynId).FirstOrDefault();
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
                    List = new ObservableCollection<MagazynForAllView>(List.OrderBy(Item => Item.MagazynId));
                    break;
                case "Symbol":
                    List = new ObservableCollection<MagazynForAllView>(List.OrderBy(Item => Item.Symbol));
                    break;
                case "Nazwa":
                    List = new ObservableCollection<MagazynForAllView>(List.OrderBy(Item => Item.Nazwa));
                    break;
                case "Typ":
                    List = new ObservableCollection<MagazynForAllView>(List.OrderBy(Item => Item.Typ));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Symbol", "Nazwa", "Typ" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Symbol":
                        List = new ObservableCollection<MagazynForAllView>(List.Where(i => i.Symbol != null && i.Symbol.StartsWith(FindTextBox)));
                        break;
                    case "Nazwa":
                        List = new ObservableCollection<MagazynForAllView>(List.Where(i => i.Nazwa != null && i.Nazwa.StartsWith(FindTextBox)));
                        break;
                    case "Typ":
                        List = new ObservableCollection<MagazynForAllView>(List.Where(i => i.Typ != null && i.Typ.StartsWith(FindTextBox)));
                        break;
                    case "Adres":
                        List = new ObservableCollection<MagazynForAllView>(List.Where(i => i.Adres != null && i.Adres.StartsWith(FindTextBox)));
                        break;
                    case "Telefon":
                        List = new ObservableCollection<MagazynForAllView>(List.Where(i => i.Telefon != null && i.Telefon.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Symbol", "Nazwa", "Typ", "Adres", "Telefon" };
        }
        #endregion
    }
}
