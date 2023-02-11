using Firma.Helpers;
using Firma.Models.BusinessLogic;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Firma.ViewModels
{
    public class NowaFakturaViewModel : JedenViewModel<Faktury>, IDataErrorInfo
    {
        #region Konstruktor
        public NowaFakturaViewModel() : base("Nowa faktura")
        {
            Item = new Faktury();
            IsEnabled = false;
            Messenger.Default.Register<KontrahentForAllView>(this, DisplayName, getSelectedKontrahent);
            DataWystawienia = DateTime.Now;
            DataSprzedazy = DateTime.Now;
            RodzajFakturyId = 1;
        }
        public NowaFakturaViewModel(Faktury faktura) : base("Edytuj fakture")
        {
            Item = faktura;
            isEditing = true;
            IsEnabled = true;
            Messenger.Default.Register<KontrahentForAllView>(this, DisplayName, getSelectedKontrahent);
        }
        #endregion
        #region Properties
        public int RodzajFakturyId
        {
            get
            {
                return Item.RodzajFakturyId;
            }
            set
            {
                if (value != Item.RodzajFakturyId)
                {
                    Item.RodzajFakturyId = value;
                    Numer = Db.FakturyRodzaje.Where(n => n.RodzajFakturyId == RodzajFakturyId).Select(n => n.Kod).FirstOrDefault() + DataWystawienia.ToString("yyMMddHHmmss");
                    RodzajFakturyNazwa = Db.FakturyRodzaje.Where(n => n.RodzajFakturyId == RodzajFakturyId).Select(n => n.Nazwa).FirstOrDefault();
                    base.OnPropertyChanged(() => RodzajFakturyId);
                }
            }
        }
        public IQueryable<KeyAndValue> FakturyRodzajeComboBoxItems
        {
            get
            {
                return
                (
                    from rodzaj in Db.FakturyRodzaje
                    where rodzaj.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = rodzaj.RodzajFakturyId,
                        Value = rodzaj.Kod
                    }
                ).ToList().AsQueryable();
            }
        }

        private String _RodzajFakturyNazwa;
        public string RodzajFakturyNazwa
        {
            get
            {
                return _RodzajFakturyNazwa;
            }
            set
            {
                if (value != _RodzajFakturyNazwa)
                {
                    _RodzajFakturyNazwa = value;
                    base.OnPropertyChanged(() => RodzajFakturyNazwa);
                }
            }
        }
        public string Numer
        {
            get
            {
                return Item.Numer;
            }
            set
            {
                if (value != Item.Numer)
                {
                    Item.Numer = value;
                    base.OnPropertyChanged(() => Numer);
                }
            }
        }
        public int KontrahentId
        {
            get
            {
                return Item.KontrahentId;
            }
            set
            {
                if (value != Item.KontrahentId)
                {
                    Item.KontrahentId = value;
                    var kontrahent = Db.Kontrahenci.Where(n => n.KontrahentId == KontrahentId).FirstOrDefault();
                    KontrahentPelnaNazwa = kontrahent.Nazwa1 + " " + kontrahent.Nazwa2 + " " + kontrahent.Nazwa3;
                    base.OnPropertyChanged(() => KontrahentId);
                }
            }
        }
        public IQueryable<KeyAndValue> KontrahenciComboBoxItems
        {
            get
            {
                return
                (
                    from kontrahent in Db.Kontrahenci
                    where kontrahent.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = kontrahent.KontrahentId,
                        Value = kontrahent.Nazwa1
                    }
                ).ToList().AsQueryable();
            }
        }

        private String _KontrahentPelnaNazwa;
        public string KontrahentPelnaNazwa
        {
            get
            {
                return _KontrahentPelnaNazwa;
            }
            set
            {
                if (value != _KontrahentPelnaNazwa)
                {
                    _KontrahentPelnaNazwa = value;
                    base.OnPropertyChanged(() => KontrahentPelnaNazwa);
                }
            }
        }
        public int KategoriaFakturyId
        {
            get
            {
                return Item.KategoriaFakturyId;
            }
            set
            {
                if (value != Item.KategoriaFakturyId)
                {
                    Item.KategoriaFakturyId = value;
                    KategoriaFakturyOpis = Db.FakturyKategorie.Where(n => n.KategoriaFakturyId == KategoriaFakturyId).Select(n => n.Opis).FirstOrDefault();
                    base.OnPropertyChanged(() => KategoriaFakturyId);
                }
            }
        }
        public IQueryable<KeyAndValue> FakturyKategorieComboBoxItems
        {
            get
            {
                return
                (
                    from kategoria in Db.FakturyKategorie
                    where kategoria.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = kategoria.KategoriaFakturyId,
                        Value = kategoria.Nazwa
                    }
                ).ToList().AsQueryable();
            }
        }

        private String _KategoriaFakturyOpis;
        public string KategoriaFakturyOpis
        {
            get
            {
                return _KategoriaFakturyOpis;
            }
            set
            {
                if (value != _KategoriaFakturyOpis)
                {
                    _KategoriaFakturyOpis = value;
                    base.OnPropertyChanged(() => KategoriaFakturyOpis);
                }
            }
        }
        public DateTime DataWystawienia
        {
            get
            {
                return Item.DataWystawienia;
            }
            set
            {
                if (value != Item.DataWystawienia)
                {
                    Item.DataWystawienia = value;
                    base.OnPropertyChanged(() => DataWystawienia);
                }
            }
        }
        public DateTime DataSprzedazy
        {
            get
            {
                return Item.DataSprzedazy;
            }
            set
            {
                if (value != Item.DataSprzedazy)
                {
                    Item.DataSprzedazy = value;
                    base.OnPropertyChanged(() => DataSprzedazy);
                }
            }
        }
        public decimal Rabat
        {
            get
            {
                return Item.Rabat;
            }
            set
            {
                if (value != Item.Rabat)
                {
                    Item.Rabat = value;
                    base.OnPropertyChanged(() => Rabat);
                }
            }
        }
        public int RodzajePlatnosciId
        {
            get
            {
                return Item.RodzajePlatnosciId;
            }
            set
            {
                if (value != Item.RodzajePlatnosciId)
                {
                    Item.RodzajePlatnosciId = value;
                    var IloscDniSplaty = Db.RodzajePlatnosci.Where(n => n.RodzajPlatnosciId == RodzajePlatnosciId).Select(n => n.IloscDniSplaty).FirstOrDefault();
                    TerminPlatnosci = DateTime.Now.AddDays(IloscDniSplaty);
                    base.OnPropertyChanged(() => RodzajePlatnosciId);
                }
            }
        }
        public IQueryable<KeyAndValue> RodzajePlatnosciComboBoxItems
        {
            get
            {
                return
                (
                    from rodzaj in Db.RodzajePlatnosci
                    where rodzaj.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = rodzaj.RodzajPlatnosciId,
                        Value = rodzaj.Nazwa
                    }
                ).ToList().AsQueryable();
            }
        }
        private DateTime _TerminPlatnosci;
        public DateTime TerminPlatnosci
        {
            get
            {
                return _TerminPlatnosci;
            }
            set
            {
                if (value != _TerminPlatnosci)
                {
                    _TerminPlatnosci = value;
                    base.OnPropertyChanged(() => TerminPlatnosci);
                }
            }
        }
        private decimal _Netto;
        public decimal Netto
        {
            get
            {
                return _Netto;
            }
            set
            {
                if (value != _Netto)
                {
                    _Netto = value;
                    base.OnPropertyChanged(() => Netto);
                }
            }
        }
        private decimal _Brutto;
        public decimal Brutto
        {
            get
            {
                return _Brutto;
            }
            set
            {
                if (value != _Brutto)
                {
                    _Brutto = value;
                    base.OnPropertyChanged(() => Brutto);
                }
            }
        }

        #endregion
        #region WZProperties
        private FakturyWydaniaZewnetrzneForAllView _Selected;
        public FakturyWydaniaZewnetrzneForAllView Selected
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
                }
            }
        }
        private ObservableCollection<FakturyWydaniaZewnetrzneForAllView> _WZList;
        public ObservableCollection<FakturyWydaniaZewnetrzneForAllView> WZList
        {
            get
            {
                if (_WZList == null)
                    Load();
                return _WZList;
            }
            set
            {
                _WZList = value;
                OnPropertyChanged(() => WZList);
            }
        }
        protected override void Load()
        {
            WZList = new ObservableCollection<FakturyWydaniaZewnetrzneForAllView>
                (
                    from fakturaWz in Db.FakturyWydaniaZewnetrzne
                    where fakturaWz.CzyAktywny == true &&
                    fakturaWz.FakturaId == Item.FakturaId
                    select new FakturyWydaniaZewnetrzneForAllView
                    {
                        FakturaWZId = fakturaWz.FakturaWZId,
                        WZNumer = fakturaWz.WydaniaZewnetrzne.Numer,
                        WydanieZewnetrzneId = fakturaWz.WydanieZewnetrzneId,
                        WZMagazynNazwa = fakturaWz.WydaniaZewnetrzne.Magazyny.Nazwa,
                        WZRabat = fakturaWz.WydaniaZewnetrzne.Rabat,
                    }
                );
            if (isEditing)
                setNettoAndBrutto();
        }
        private void setNettoAndBrutto()
        {
            var wz = WZList.ToList().Select(y => y.WydanieZewnetrzneId).ToList();
            var pozycjeWZ = Db.PozycjeWydaniaZewnetrznego.Where(x => wz.Contains(x.WydanieZewnetrzneId) && x.CzyAktywny).ToList();

            Netto = 0;
            Brutto = 0;
            foreach (var pozycjaWZ in pozycjeWZ)
            {
                var wydanie = Db.WydaniaZewnetrzne.Where(x => x.WydanieZewnetrzneId == pozycjaWZ.WydanieZewnetrzneId && pozycjaWZ.CzyAktywny).FirstOrDefault();
                var cena = Db.ZmianyCeny.Where
                    (
                        x => x.TowarId == pozycjaWZ.TowarId
                        && x.DataObowiazywaniaOd <= wydanie.DataWydania
                        && (x.DataObowiazywaniaDo >= wydanie.DataWydania || x.DataObowiazywaniaDo == null)
                        && x.CzyAktywny
                    ).OrderByDescending(x=>x.DataObowiazywaniaOd).Select(x => x.CenaNetto).FirstOrDefault();
                var wartoscNettoPozycji = cena * pozycjaWZ.Ilosc
                    * (100 - pozycjaWZ.Rabat) / 100
                    * (100 - wydanie.Rabat) / 100;
                Netto += wartoscNettoPozycji;
                var stawkaVatSprzId = Db.Towary.Where(x => x.TowarId == pozycjaWZ.TowarId).Select(x => x.VatSprzId).FirstOrDefault();
                var vat = Db.TowaryStawkiVat.Where(x => x.StawkiVatId == stawkaVatSprzId).Select(x => x.Stawka).FirstOrDefault();
                Brutto += wartoscNettoPozycji * (100 + vat) / 100;
            }
            Netto *= (100 - Rabat) / 100;
            Brutto *= (100 - Rabat) / 100;
        }
        protected override void delete()
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
                MessageBox.Show("Wybierz rekord, który chcesz usunąć ");
            }
        }
        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.KtoUtworzylId = 1;
            Item.CzyAktywny = true;
            Db.Faktury.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        protected override void add()
        {
            Messenger.Default.Send(new NowaFakturaWydanieZewnetrzneViewModel(Item));
        }
        private void getSelectedKontrahent(KontrahentForAllView kontrahentForAllView)
        {
            KontrahentId = kontrahentForAllView.KontrahentId;
        }
        #endregion
        #region Validation
        public string Error
        {
            get
            {
                return null;
            }
        }
        public string this[string name]
        {
            get
            {
                string komunikat = null;
                switch (name)
                {
                    case "Rabat":
                        komunikat = BusinessValidator.CheckBetween0and100(Rabat);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Rabat);
                        break;
                    case "Platnosc":
                        komunikat = BusinessValidator.CheckIsSet(RodzajePlatnosciId);
                        break;
                    case "Kontrahent":
                        komunikat = BusinessValidator.CheckIsSet(KontrahentId);
                        break;
                    case "Kategoria":
                        komunikat = BusinessValidator.CheckIsSet(KategoriaFakturyId);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["Rabat"] == null ? "" : "Rabat: " + this["Rabat"] + "\n";
            komunikat += this["Platnosc"] == null ? "" : "Płatność: " + this["Platnosc"] + "\n";
            komunikat += this["Kontrahent"] == null ? "" : "Kontrahent: " + this["Kontrahent"] + "\n";
            komunikat += this["Kategoria"] == null ? "" : "Kategoria: " + this["Kategoria"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
