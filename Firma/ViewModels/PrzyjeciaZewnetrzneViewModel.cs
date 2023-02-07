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
    public class PrzyjeciaZewnetrzneViewModel : WszystkieViewModel<PrzyjecieZewnetrzneForAllView>
    {
        #region Konstruktor
        public PrzyjeciaZewnetrzneViewModel():base("Przyjecia Zewnetrzne")
        {
        }
        public PrzyjeciaZewnetrzneViewModel(string token):base("Przyjecia Zewnetrzne", token)
        {
        }
        #endregion
        #region Properties
        public override PrzyjecieZewnetrzneForAllView Selected
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
            List = new ObservableCollection<PrzyjecieZewnetrzneForAllView>
                (
                    from pz in Db.PrzyjeciaZewnetrzne
                    where pz.CzyAktywny == true
                    select new PrzyjecieZewnetrzneForAllView
                    {
                        PrzyjecieZewnetrzneId= pz.PrzyjecieZewnetrzneId,
                        Numer = pz.Numer,
                        DataPrzyjecia = pz.DataPrzyjecia,
                        NazwaMagazynu = pz.Magazyny.Nazwa,
                        NazwaKontrahenta = pz.Kontrahenci.Nazwa1,
                        NipKontrahenta = pz.Kontrahenci.Nip
                    }
                );
        }

        public override void Add()
        {
            Messenger.Default.Send(new NowePrzyjecieZewnetrzneViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.PrzyjeciaZewnetrzne.Where(a => a.PrzyjecieZewnetrzneId == Selected.PrzyjecieZewnetrzneId).FirstOrDefault();
                Messenger.Default.Send(new NowePrzyjecieZewnetrzneViewModel(toEdit));
                Messenger.Default.Register<PrzyjeciaZewnetrzne>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(PrzyjeciaZewnetrzne edited)
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
                var toDelete = Db.PrzyjeciaZewnetrzne.Where(a => a.PrzyjecieZewnetrzneId == Selected.PrzyjecieZewnetrzneId).FirstOrDefault();
                if (toDelete != null)
                {
                    toDelete.CzyAktywny = false;
                    toDelete.DataUsuniecia = DateTime.Now;
                    toDelete.KtoUsunalId = 1;
                    var pozycjeToDelete = Db.PozycjePrzyjeciaZewnetrznego.Where(a => a.PrzyjecieZewnetrzneId == toDelete.PrzyjecieZewnetrzneId).ToList();
                    foreach (var pozycja in pozycjeToDelete)
                    {
                        pozycja.CzyAktywny = false;
                        pozycja.DataUsuniecia = DateTime.Now;
                        pozycja.KtoUsunalId = 1;
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
                    List = new ObservableCollection<PrzyjecieZewnetrzneForAllView>(List.OrderBy(Item => Item.PrzyjecieZewnetrzneId));
                    break;
                case "Data przyjęcia":
                    List = new ObservableCollection<PrzyjecieZewnetrzneForAllView>(List.OrderBy(Item => Item.DataPrzyjecia));
                    break;
                case "Magazyn":
                    List = new ObservableCollection<PrzyjecieZewnetrzneForAllView>(List.OrderBy(Item => Item.NazwaMagazynu));
                    break;
                case "Kontrahent":
                    List = new ObservableCollection<PrzyjecieZewnetrzneForAllView>(List.OrderBy(Item => Item.NazwaKontrahenta));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Data przyjęcia", "Magazyn", "Kontrahent" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Data przyjęcia":
                        List = new ObservableCollection<PrzyjecieZewnetrzneForAllView>(List.Where(i => i.DataPrzyjecia != null && i.DataPrzyjecia.ToString("dd.MM.yyyy HH:mm").StartsWith(FindTextBox)));
                        break;
                    case "Magazyn":
                        List = new ObservableCollection<PrzyjecieZewnetrzneForAllView>(List.Where(i => i.NazwaMagazynu != null && i.NazwaMagazynu.StartsWith(FindTextBox)));
                        break;
                    case "Kontrahent":
                        List = new ObservableCollection<PrzyjecieZewnetrzneForAllView>(List.Where(i => i.NazwaKontrahenta != null && i.NazwaKontrahenta.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Data przyjęcia", "Magazyn", "Kontrahent" };
        }
        #endregion
    }
}
