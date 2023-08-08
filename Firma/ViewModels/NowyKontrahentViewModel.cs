using Firma.Helpers;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class NowyKontrahentViewModel : JedenViewModel<Kontrahenci>, IDataErrorInfo
    {
        #region Commands
        public ICommand ClearKorCommand
        {
            get
            {
                return new BaseCommand(() => ClearKor());
            }
        }
        #endregion
        #region Konstruktor
        public NowyKontrahentViewModel()
            : base("Nowy kontrahent")
        {
            Item = new Kontrahenci();
            IsEnabled = false;
            setMessengers();
        }
        public NowyKontrahentViewModel(Kontrahenci kontrahent)
            : base("Edytuj kontrahenta")
        {
            Item = kontrahent;
            isEditing = true;
            IsEnabled = true;
            setAllFields();
            setMessengers();
        }
        private void setMessengers()
        {
            Messenger.Default.Register<KontrahentForAllView>(this, DisplayName, getSelectedKontrahent);
            Messenger.Default.Register<AdresAndIsKor>(this, DisplayName, getSelectedAdres);
        }
        #endregion
        #region Properties

        public string Kod
        {
            get
            {
                return Item.Kod;
            }
            set
            {
                if (value != Item.Kod)
                {
                    Item.Kod = value;
                    base.OnPropertyChanged(() => Kod);
                }
            }
        }
        public string Nip
        {
            get
            {
                return Item.Nip;
            }
            set
            {
                if (value != Item.Nip)
                {
                    Item.Nip = value;
                    base.OnPropertyChanged(() => Nip);
                }
            }
        }
        public int RodzajKontrahentaId
        {
            get
            {
                return Item.RodzajKontrahentaId;
            }
            set
            {
                if (value != Item.RodzajKontrahentaId)
                {
                    Item.RodzajKontrahentaId = value;
                    base.OnPropertyChanged(() => RodzajKontrahentaId);
                }
            }
        }
        public IQueryable<KeyAndValue> RodzajeKontrahentaComboBoxItems
        {
            get
            {
                return
                (
                    from rodzaj in Db.KontrahenciRodzaje
                    where rodzaj.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = rodzaj.RodzajId,
                        Value = rodzaj.Nazwa
                    }
                ).ToList().AsQueryable();
            }
        }
        public string Regon
        {
            get
            {
                return Item.Regon;
            }
            set
            {
                if (value != Item.Regon)
                {
                    Item.Regon = value;
                    base.OnPropertyChanged(() => Regon);
                }
            }
        }
        public string DokumentTozsamosci
        {
            get
            {
                return Item.DokumentTozsamosci;
            }
            set
            {
                if (value != Item.DokumentTozsamosci)
                {
                    Item.DokumentTozsamosci = value;
                    base.OnPropertyChanged(() => DokumentTozsamosci);
                }
            }
        }
        public string PESEL
        {
            get
            {
                return Item.PESEL;
            }
            set
            {
                if (value != Item.PESEL)
                {
                    Item.PESEL = value;
                    base.OnPropertyChanged(() => PESEL);
                }
            }
        }
        public string Nazwa1
        {
            get
            {
                return Item.Nazwa1;
            }
            set
            {
                if (value != Item.Nazwa1)
                {
                    Item.Nazwa1 = value;
                    base.OnPropertyChanged(() => Nazwa1);
                }
            }
        }
        public string Nazwa2
        {
            get
            {
                return Item.Nazwa2;
            }
            set
            {
                if (value != Item.Nazwa2)
                {
                    Item.Nazwa2 = value;
                    base.OnPropertyChanged(() => Nazwa2);
                }
            }
        }
        public string Nazwa3
        {
            get
            {
                return Item.Nazwa3;
            }
            set
            {
                if (value != Item.Nazwa3)
                {
                    Item.Nazwa3 = value;
                    base.OnPropertyChanged(() => Nazwa3);
                }
            }
        }
        public int? JednostkaPodlegaPodId
        {
            get
            {
                return Item.JednostkaPodlegaPodId;
            }
            set
            {
                if (value != Item.JednostkaPodlegaPodId)
                {
                    Item.JednostkaPodlegaPodId = value;
                    setJednostkaPodlegaPodFields();
                    base.OnPropertyChanged(() => JednostkaPodlegaPodId);
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
                        Value = kontrahent.Kod
                    }
                ).ToList().AsQueryable();
            }
        }
        private string _JednostkaPodlegaPodNazwa;
        public string JednostkaPodlegaPodNazwa
        {
            get
            {
                return _JednostkaPodlegaPodNazwa;
            }
            set
            {
                if (value != _JednostkaPodlegaPodNazwa)
                {
                    _JednostkaPodlegaPodNazwa = value;
                    base.OnPropertyChanged(() => JednostkaPodlegaPodNazwa);
                }
            }
        }
        public string URL
        {
            get
            {
                return Item.URL;
            }
            set
            {
                if (value != Item.URL)
                {
                    Item.URL = value;
                    base.OnPropertyChanged(() => URL);
                }
            }
        }
        public int AdresId
        {
            get
            {
                return Item.AdresId;
            }
            set
            {
                if (value != Item.AdresId)
                {
                    Item.AdresId = value;
                    setAdresFields();
                    base.OnPropertyChanged(() => AdresId);
                }
            }
        }
        public IQueryable<KeyAndValue> AdresyComboBoxItems
        {
            get
            {
                return
                (
                    from adres in Db.Adresy
                    where adres.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = adres.AdresId,
                        Value = adres.Ulica + " " + adres.NrDomu +
                        (adres.NrLokalu.Equals("") ? "" : "/" + adres.NrLokalu) +
                        ", " + adres.KodPocztowy + " " + adres.Miejscowosc
                    }
                ).ToList().AsQueryable();
            }
        }
        private string _Ulica;
        public string Ulica
        {
            get
            {
                return _Ulica;
            }
            set
            {
                if (value != _Ulica)
                {
                    _Ulica = value;
                    base.OnPropertyChanged(() => Ulica);
                }
            }
        }
        private string _NrDomu;
        public string NrDomu
        {
            get
            {
                return _NrDomu;
            }
            set
            {
                if (value != _NrDomu)
                {
                    _NrDomu = value;
                    base.OnPropertyChanged(() => NrDomu);
                }
            }
        }
        private string _NrLokalu;
        public string NrLokalu
        {
            get
            {
                return _NrLokalu;
            }
            set
            {
                if (value != _NrLokalu)
                {
                    _NrLokalu = value;
                    base.OnPropertyChanged(() => NrLokalu);
                }
            }
        }
        private string _KodPocztowy;
        public string KodPocztowy
        {
            get
            {
                return _KodPocztowy;
            }
            set
            {
                if (value != _KodPocztowy)
                {
                    _KodPocztowy = value;
                    base.OnPropertyChanged(() => KodPocztowy);
                }
            }
        }
        private string _Miejscowosc;
        public string Miejscowosc
        {
            get
            {
                return _Miejscowosc;
            }
            set
            {
                if (value != _Miejscowosc)
                {
                    _Miejscowosc = value;
                    base.OnPropertyChanged(() => Miejscowosc);
                }
            }
        }
        private string _Wojewodztwo;
        public string Wojewodztwo
        {
            get
            {
                return _Wojewodztwo;
            }
            set
            {
                if (value != _Wojewodztwo)
                {
                    _Wojewodztwo = value;
                    base.OnPropertyChanged(() => Wojewodztwo);
                }
            }
        }

        private string _Kraj;
        public string Kraj
        {
            get
            {
                return _Kraj;
            }
            set
            {
                if (value != _Kraj)
                {
                    _Kraj = value;
                    base.OnPropertyChanged(() => Kraj);
                }
            }
        }
        private string _Dodatkowe;
        public string Dodatkowe
        {
            get
            {
                return _Dodatkowe;
            }
            set
            {
                if (value != _Dodatkowe)
                {
                    _Dodatkowe = value;
                    base.OnPropertyChanged(() => Dodatkowe);
                }
            }
        }
        public int? AdresKorId
        {
            get
            {
                return Item.AdresKorId;
            }
            set
            {
                if (value == null)
                {
                    Item.AdresKorId = value;
                    base.OnPropertyChanged(() => AdresKorId);
                }
                else if (value != Item.AdresKorId)
                {
                    Item.AdresKorId = value;
                    setAdresKorFields();
                    base.OnPropertyChanged(() => AdresKorId);
                }
            }
        }
        private string _UlicaKor;
        public string UlicaKor
        {
            get
            {
                return _UlicaKor;
            }
            set
            {
                if (value != _UlicaKor)
                {
                    _UlicaKor = value;
                    base.OnPropertyChanged(() => UlicaKor);
                }
            }
        }
        private string _NrDomuKor;
        public string NrDomuKor
        {
            get
            {
                return _NrDomuKor;
            }
            set
            {
                if (value != _NrDomuKor)
                {
                    _NrDomuKor = value;
                    base.OnPropertyChanged(() => NrDomuKor);
                }
            }
        }
        private string _NrLokaluKor;
        public string NrLokaluKor
        {
            get
            {
                return _NrLokaluKor;
            }
            set
            {
                if (value != _NrLokaluKor)
                {
                    _NrLokaluKor = value;
                    base.OnPropertyChanged(() => NrLokaluKor);
                }
            }
        }
        private string _KodPocztowyKor;
        public string KodPocztowyKor
        {
            get
            {
                return _KodPocztowyKor;
            }
            set
            {
                if (value != _KodPocztowyKor)
                {
                    _KodPocztowyKor = value;
                    base.OnPropertyChanged(() => KodPocztowyKor);
                }
            }
        }
        private string _MiejscowoscKor;
        public string MiejscowoscKor
        {
            get
            {
                return _MiejscowoscKor;
            }
            set
            {
                if (value != _MiejscowoscKor)
                {
                    _MiejscowoscKor = value;
                    base.OnPropertyChanged(() => MiejscowoscKor);
                }
            }
        }
        private string _WojewodztwoKor;
        public string WojewodztwoKor
        {
            get
            {
                return _WojewodztwoKor;
            }
            set
            {
                if (value != _WojewodztwoKor)
                {
                    _WojewodztwoKor = value;
                    base.OnPropertyChanged(() => WojewodztwoKor);
                }
            }
        }

        private string _KrajKor;
        public string KrajKor
        {
            get
            {
                return _KrajKor;
            }
            set
            {
                if (value != _KrajKor)
                {
                    _KrajKor = value;
                    base.OnPropertyChanged(() => KrajKor);
                }
            }
        }
        private string _DodatkoweKor;
        public string DodatkoweKor
        {
            get
            {
                return _DodatkoweKor;
            }
            set
            {
                if (value != _DodatkoweKor)
                {
                    _DodatkoweKor = value;
                    base.OnPropertyChanged(() => DodatkoweKor);
                }
            }
        }

        #endregion
        #region KontaktyProperties
        private KontrahenciKontaktyForAllView _Selected;
        public KontrahenciKontaktyForAllView Selected
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
        private ObservableCollection<KontrahenciKontaktyForAllView> _KontaktyList;
        public ObservableCollection<KontrahenciKontaktyForAllView> KontaktyList
        {
            get
            {
                if (_KontaktyList == null)
                    Load();
                return _KontaktyList;
            }
            set
            {
                _KontaktyList = value;
                OnPropertyChanged(() => KontaktyList);
            }
        }
        protected override void Load()
        {
            KontaktyList = new ObservableCollection<KontrahenciKontaktyForAllView>
                (
                    from kontrahentKontakt in Db.KontrahenciKontakty
                    where kontrahentKontakt.CzyAktywny == true
                    && kontrahentKontakt.KontrahentId == Item.KontrahentId
                    select new KontrahenciKontaktyForAllView
                    {
                        KontrahentKontaktId = kontrahentKontakt.KontrahentKontaktId,
                        KontaktNazwaDzialu = kontrahentKontakt.Kontakty.NazwaDzialu,
                        KontaktOpisOsoby = kontrahentKontakt.Kontakty.OpisOsoby,
                        KontaktTelefon1 = kontrahentKontakt.Kontakty.Telefon1,
                        KontaktTelefon2 = kontrahentKontakt.Kontakty.Telefon2,
                        KontaktEmail1 = kontrahentKontakt.Kontakty.Email1,
                        KontaktEmail2 = kontrahentKontakt.Kontakty.Email2,
                    }
                );
        }
        protected override void delete()
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
            if (Item.Nazwa2 == null)
                Item.Nazwa2 = "";
            if (Item.Nazwa3 == null)
                Item.Nazwa3 = "";
            if (Item.PESEL == null)
                Item.PESEL = "";
            if (Item.DokumentTozsamosci == null)
                Item.DokumentTozsamosci = "";
            if (Item.Nip == null)
                Item.Nip = "";
            if (Item.Regon == null)
                Item.Regon = "";
            if (Item.URL == null)
                Item.URL = "";
            Db.Kontrahenci.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        protected override void add()
        {
            Messenger.Default.Send(new NowyKontrahentKontaktViewModel(Item));
        }
        private void getSelectedKontrahent(KontrahentForAllView kontrahentForAllView)
        {
            JednostkaPodlegaPodId = kontrahentForAllView.KontrahentId;
        }
        private void getSelectedAdres(AdresAndIsKor adresAndIsKor)
        {
            if (!adresAndIsKor.isAdresKor)
                AdresId = adresAndIsKor.AdresForAllView.AdresId;
            else
                AdresKorId = adresAndIsKor.AdresForAllView.AdresId;
        }
        private void ClearKor()
        {
            AdresKorId = null;
            UlicaKor = "";
            NrDomuKor = "";
            NrLokaluKor = "";
            KodPocztowyKor = "";
            MiejscowoscKor = "";
            WojewodztwoKor = "";
            KrajKor = "";
            DodatkoweKor = "";
        }
        private void setAllFields()
        {
            setJednostkaPodlegaPodFields();
            setAdresFields();
            setAdresKorFields();
        }
        private void setJednostkaPodlegaPodFields()
        {
            try
            {
                var kontrahent = Db.Kontrahenci.Where(n => n.KontrahentId == JednostkaPodlegaPodId).FirstOrDefault();
                if (kontrahent != null)
                    JednostkaPodlegaPodNazwa = kontrahent.Nazwa1 + " " + kontrahent.Nazwa2 + " " + kontrahent.Nazwa3;
            }
            catch (Exception) { }
        }
        private void setAdresFields()
        {
            var adres = Db.Adresy.Where(n => n.AdresId == AdresId).FirstOrDefault();
            if (adres != null)
            {
                Ulica = adres.Ulica;
                NrDomu = adres.NrDomu;
                NrLokalu = adres.NrLokalu;
                KodPocztowy = adres.KodPocztowy;
                Miejscowosc = adres.Miejscowosc;
                Wojewodztwo = adres.Wojewodztwo;
                Kraj = Db.Kraje.Where(n => n.KrajId == adres.KrajId).Select(n => n.Nazwa).FirstOrDefault();
                Dodatkowe = adres.Dodatkowe;
            }
        }
        private void setAdresKorFields()
        {
            var adres = Db.Adresy.Where(n => n.AdresId == AdresKorId).FirstOrDefault();
            if (adres != null)
            {
                UlicaKor = adres.Ulica;
                NrDomuKor = adres.NrDomu;
                NrLokaluKor = adres.NrLokalu;
                KodPocztowyKor = adres.KodPocztowy;
                MiejscowoscKor = adres.Miejscowosc;
                WojewodztwoKor = adres.Wojewodztwo;
                KrajKor = Db.Kraje.Where(n => n.KrajId == adres.KrajId).Select(n => n.Nazwa).FirstOrDefault();
                DodatkoweKor = adres.Dodatkowe;
            }
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
                    case "Kod":
                        komunikat = StringValidator.CheckIsAllUpper(Kod);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Kod);
                        break;
                    case "Nip":
                        komunikat = BusinessValidator.CheckIsNip(Nip);
                        break;
                    case "RodzajKontrahenta":
                        komunikat = BusinessValidator.CheckIsSet(RodzajKontrahentaId);
                        break;
                    case "Regon":
                        komunikat = BusinessValidator.CheckIsRegon(Regon);
                        break;
                    case "DokumentTozsamosci":
                        komunikat = StringValidator.CheckIsAllUpper(DokumentTozsamosci);
                        break;
                    case "PESEL":
                        komunikat = BusinessValidator.CheckIsPesel(PESEL);
                        break;
                    case "Nazwa1":
                        komunikat = StringValidator.CheckIsStartsWithUpper(Nazwa1);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Nazwa1);
                        break;
                    case "URL":
                        komunikat = BusinessValidator.CheckIsUrl(URL);
                        break;
                    case "Adres":
                        komunikat = BusinessValidator.CheckIsSet(AdresId);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["Kod"] == null ? "" : "Kod: " + this["Kod"] + "\n";
            komunikat += this["Nip"] == null ? "" : "Nip: " + this["Nip"] + "\n";
            komunikat += this["RodzajKontrahenta"] == null ? "" : "Rodzaj kontrahenta: " + this["RodzajKontrahenta"] + "\n";
            komunikat += this["Regon"] == null ? "" : "Regon: " + this["Regon"] + "\n";
            komunikat += this["DokumentTozsamosci"] == null ? "" : "Dokument tożsamosci: " + this["DokumentTozsamosci"] + "\n";
            komunikat += this["PESEL"] == null ? "" : "PESEL: " + this["PESEL"] + "\n";
            komunikat += this["Nazwa1"] == null ? "" : "Nazwa: " + this["Nazwa1"] + "\n";
            komunikat += this["URL"] == null ? "" : "URL: " + this["URL"] + "\n";
            komunikat += this["Adres"] == null ? "" : "Adres: " + this["Adres"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
