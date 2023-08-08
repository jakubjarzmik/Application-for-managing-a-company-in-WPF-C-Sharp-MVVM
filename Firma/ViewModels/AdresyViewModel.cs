using Firma.Helpers;
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
    public class AdresyViewModel : WszystkieViewModel<AdresForAllView>
    {
        #region Konstruktor
        public AdresyViewModel() : base("Adresy")
        {
        }
        public AdresyViewModel(string token) : base("Adresy", token)
        {
            isAdresKor = false;
        }
        public AdresyViewModel(string token, bool isAdresKor) : base("Adresy", token)
        {
            this.isAdresKor = isAdresKor;
        }
        #endregion
        #region Properties
        private bool isAdresKor;
        public override AdresForAllView Selected
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
                    Messenger.Default.Send(new AdresAndIsKor { AdresForAllView = _Selected, isAdresKor = this.isAdresKor }, token);
                    if (toClose)
                        OnRequestClose();
                }
            }
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<AdresForAllView>
                (
                    from adres in Db.Adresy
                    where adres.CzyAktywny == true
                    select new AdresForAllView
                    {
                        AdresId = adres.AdresId,
                        Kraj = adres.Kraje.Nazwa,
                        Wojewodztwo = adres.Wojewodztwo,
                        KodPocztowy = adres.KodPocztowy,
                        Miejscowosc = adres.Miejscowosc,
                        Ulica = adres.Ulica,
                        NrDomu = adres.NrDomu,
                        NrLokalu = adres.NrLokalu
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyAdresViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.Adresy.Where(a => a.AdresId == Selected.AdresId).FirstOrDefault();
                Messenger.Default.Send(new NowyAdresViewModel(toEdit));
                Messenger.Default.Register<Adresy>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Adresy edited)
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
                var toDelete = Db.Adresy.Where(a => a.AdresId == Selected.AdresId).FirstOrDefault();
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
                    List = new ObservableCollection<AdresForAllView>(List.OrderBy(Item => Item.AdresId));
                    break;
                case "Kraj":
                    List = new ObservableCollection<AdresForAllView>(List.OrderBy(Item => Item.Kraj));
                    break;
                case "Województwo":
                    List = new ObservableCollection<AdresForAllView>(List.OrderBy(Item => Item.Wojewodztwo));
                    break;
                case "Miejscowość":
                    List = new ObservableCollection<AdresForAllView>(List.OrderBy(Item => Item.Miejscowosc));
                    break;
                case "Ulica":
                    List = new ObservableCollection<AdresForAllView>(List.OrderBy(Item => Item.Ulica));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Kraj", "Województwo", "Miejscowość", "Ulica" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Kraj":
                        List = new ObservableCollection<AdresForAllView>(List.Where(i => i.Kraj != null && i.Kraj.StartsWith(FindTextBox)));
                        break;
                    case "Województwo":
                        List = new ObservableCollection<AdresForAllView>(List.Where(i => i.Wojewodztwo != null && i.Wojewodztwo.StartsWith(FindTextBox)));
                        break;
                    case "Kod pocztowy":
                        List = new ObservableCollection<AdresForAllView>(List.Where(i => i.KodPocztowy != null && i.KodPocztowy.StartsWith(FindTextBox)));
                        break;
                    case "Miejscowość":
                        List = new ObservableCollection<AdresForAllView>(List.Where(i => i.Miejscowosc != null && i.Miejscowosc.StartsWith(FindTextBox)));
                        break;
                    case "Ulica":
                        List = new ObservableCollection<AdresForAllView>(List.Where(i => i.Ulica != null && i.Ulica.StartsWith(FindTextBox)));
                        break;
                    case "Nr domu":
                        List = new ObservableCollection<AdresForAllView>(List.Where(i => i.NrDomu != null && i.NrDomu.StartsWith(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Kraj", "Województwo", "Kod pocztowy", "Miejscowość", "Ulica", "Nr domu" };
        }
        #endregion
    }
}
