using Firma.Helpers;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class NowyPracownikViewModel : JedenViewModel<Pracownicy>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyPracownikViewModel() : base("Nowy pracownik")
        {
            Item = new Pracownicy();
            IsEnabled = false;
            Messenger.Default.Register<AdresAndIsKor>(this, DisplayName, getSelectedAdres);
            DataUrodzenia = DateTime.Today;
            Akronim = "  ";
        }
        public NowyPracownikViewModel(Pracownicy pracownik) : base("Edytuj pracownika")
        {
            Item = pracownik;
            isEditing = true;
            IsEnabled = true;
            Messenger.Default.Register<AdresAndIsKor>(this, DisplayName, getSelectedAdres);
        }
        #endregion
        #region Properties
        public string Akronim
        {
            get
            {
                return Item.Akronim;
            }
            set
            {
                if (value != Item.Akronim)
                {
                    Item.Akronim = value;
                    base.OnPropertyChanged(() => Akronim);
                }
            }
        }
        public string Imie
        {
            get
            {
                return Item.Imie;
            }
            set
            {
                if (value != Item.Imie)
                {
                    Item.Imie = value;
                    var akronimArr = Akronim.ToCharArray();
                    try
                    {
                        akronimArr[0] = Imie[0];
                    }catch(IndexOutOfRangeException)
                    {
                        akronimArr[0] = ' ';
                    }
                    Akronim = new string(akronimArr);
                    base.OnPropertyChanged(() => Imie);
                }
            }
        }
        public string DrugieImie
        {
            get
            {
                return Item.DrugieImie;
            }
            set
            {
                if(value != Item.DrugieImie)
                {
                    Item.DrugieImie = value;
                    base.OnPropertyChanged(() => DrugieImie);
                }
            }
        }
        public string Nazwisko
        {
            get
            {
                return Item.Nazwisko;
            }
            set
            {
                if(value != Item.Nazwisko)
                {
                    Item.Nazwisko = value;
                    var akronimArr = Akronim.ToCharArray();
                    try
                    {
                        akronimArr[1] = Nazwisko[0];
                    }
                    catch (IndexOutOfRangeException)
                    {
                        akronimArr[1] = ' ';
                    }
                    Akronim = new string(akronimArr);
                    base.OnPropertyChanged(() => Nazwisko);
                }
            }
        }
        public string NazwiskoRodowe
        {
            get
            {
                return Item.NazwiskoRodowe;
            }
            set
            {
                if(value != Item.NazwiskoRodowe)
                {
                    Item.NazwiskoRodowe = value;
                    base.OnPropertyChanged(() => NazwiskoRodowe);
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
                if(value != Item.PESEL)
                {
                    Item.PESEL = value;
                    base.OnPropertyChanged(() => PESEL);
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
                if(value != Item.Nip)
                {
                    Item.Nip = value;
                    base.OnPropertyChanged(() => Nip);
                }
            }
        }
        public DateTime DataUrodzenia
        {
            get
            {
                return Item.DataUrodzenia;
            }
            set
            {
                if(value != Item.DataUrodzenia)
                {
                    Item.DataUrodzenia = value;
                    base.OnPropertyChanged(() => DataUrodzenia);
                }
            }
        }
        public string MiejsceUrodzenia
        {
            get
            {
                return Item.MiejsceUrodzenia;
            }
            set
            {
                if (value != Item.MiejsceUrodzenia)
                {
                    Item.MiejsceUrodzenia = value;
                    base.OnPropertyChanged(() => MiejsceUrodzenia);
                }
            }
        }
        public string ImieOjca
        {
            get
            {
                return Item.ImieOjca;
            }
            set
            {
                if (value != Item.ImieOjca)
                {
                    Item.ImieOjca = value;
                    base.OnPropertyChanged(() => ImieOjca);
                }
            }
        }
        public string ImieMatki
        {
            get
            {
                return Item.ImieMatki;
            }
            set
            {
                if (value != Item.ImieMatki)
                {
                    Item.ImieMatki = value;
                    base.OnPropertyChanged(() => ImieMatki);
                }
            }
        }
        public string NazwiskoRodoweMatki
        {
            get
            {
                return Item.NazwiskoRodoweMatki;
            }
            set
            {
                if (value != Item.NazwiskoRodoweMatki)
                {
                    Item.NazwiskoRodoweMatki = value;
                    base.OnPropertyChanged(() => NazwiskoRodoweMatki);
                }
            }
        }
        public string Telefon
        {
            get
            {
                return Item.Telefon;
            }
            set
            {
                if (value != Item.Telefon)
                {
                    Item.Telefon = value;
                    base.OnPropertyChanged(() => Telefon);
                }
            }
        }
        public string Email
        {
            get
            {
                return Item.Email;
            }
            set
            {
                if (value != Item.Email)
                {
                    Item.Email = value;
                    base.OnPropertyChanged(() => Email);
                }
            }
        }
        public string Uwagi
        {
            get
            {
                return Item.Uwagi;
            }
            set
            {
                if (value != Item.Uwagi)
                {
                    Item.Uwagi = value;
                    base.OnPropertyChanged(() => Uwagi);
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
                    var adres = Db.Adresy.Where(n => n.AdresId == AdresId).FirstOrDefault();
                    Ulica = adres.Ulica;
                    NrDomu = adres.NrDomu;
                    NrLokalu = adres.NrLokalu;
                    KodPocztowy = adres.KodPocztowy;
                    Miejscowosc = adres.Miejscowosc;
                    Wojewodztwo = adres.Wojewodztwo;
                    Kraj = Db.Kraje.Where(n => n.KrajId == adres.KrajId).Select(n => n.Nazwa).FirstOrDefault();
                    Dodatkowe = adres.Dodatkowe;
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
        #endregion
        #region UmowyProperties
        private PracownicyUmowyForAllView _Selected;
        public PracownicyUmowyForAllView Selected
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
        private ObservableCollection<PracownicyUmowyForAllView> _UmowyList;
        public ObservableCollection<PracownicyUmowyForAllView> UmowyList
        {
            get
            {
                if (_UmowyList == null)
                    Load();
                return _UmowyList;
            }
            set
            {
                _UmowyList = value;
                OnPropertyChanged(() => UmowyList);
            }
        }
        protected override void Load()
        {
            UmowyList = new ObservableCollection<PracownicyUmowyForAllView>
                (
                    from pracownikUmowa in Db.PracownicyUmowy
                    where pracownikUmowa.CzyAktywny == true
                    && pracownikUmowa.PracownikId == Item.PracownikId
                    select new PracownicyUmowyForAllView
                    {
                        PracownikUmowaId = pracownikUmowa.PracownikUmowaId,
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
        protected override void delete()
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
            if (Item.DrugieImie == null)
                Item.DrugieImie = "";
            if (Item.NazwiskoRodowe == null)
                Item.NazwiskoRodowe = "";
            if (Item.PESEL == null)
                Item.PESEL = "";
            if (Item.Nip == null)
                Item.Nip = "";
            if (Item.ImieOjca == null)
                Item.ImieOjca = "";
            if (Item.ImieMatki == null)
                Item.ImieMatki = "";
            if (Item.NazwiskoRodoweMatki == null)
                Item.NazwiskoRodoweMatki = "";
            if (Item.Telefon == null)
                Item.Telefon = "";
            if (Item.Uwagi == null)
                Item.Uwagi = "";
            Db.Pracownicy.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        protected override void add()
        {
            Messenger.Default.Send(new NowyPracownikUmowaViewModel(Item));
        }
        private void getSelectedAdres(AdresAndIsKor adresAndIsKor)
        {
            AdresId = adresAndIsKor.AdresForAllView.AdresId;
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
                    case "Imie":
                        komunikat = StringValidator.CheckIsStartsWithUpper(Imie);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Imie);
                        break;
                    case "DrugieImie":
                        komunikat = StringValidator.CheckIsStartsWithUpper(DrugieImie);
                        break;
                    case "Nazwisko":
                        komunikat = StringValidator.CheckIsStartsWithUpper(Nazwisko);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Nazwisko);
                        break;
                    case "NazwiskoRodowe":
                        komunikat = StringValidator.CheckIsStartsWithUpper(NazwiskoRodowe);
                        break;
                    case "PESEL":
                        komunikat = BusinessValidator.CheckIsPesel(PESEL);
                        break;
                    case "Nip":
                        komunikat = BusinessValidator.CheckIsNip(Nip);
                        break;
                    case "MiejsceUrodzenia":
                        komunikat = StringValidator.CheckIsStartsWithUpper(MiejsceUrodzenia);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(MiejsceUrodzenia);
                        break;
                    case "ImieOjca":
                        komunikat = StringValidator.CheckIsStartsWithUpper(ImieOjca);
                        break;
                    case "ImieMatki":
                        komunikat = StringValidator.CheckIsStartsWithUpper(ImieMatki);
                        break;
                    case "NazwiskoRodoweMatki":
                        komunikat = StringValidator.CheckIsStartsWithUpper(NazwiskoRodoweMatki);
                        break;
                    case "Telefon":
                        komunikat = StringValidator.CheckIsNumeric(Telefon);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Telefon);
                        break;
                    case "Email":
                        komunikat = StringValidator.CheckIsEmail(Email);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Email);
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
            komunikat += this["Imie"] == null ? "" : "Imię: " + this["Imie"] + "\n";
            komunikat += this["DrugieImie"] == null ? "" : "Drugie imię: " + this["DrugieImie"] + "\n";
            komunikat += this["Nazwisko"] == null ? "" : "Nazwisko: " + this["Nazwisko"] + "\n";
            komunikat += this["NazwiskoRodowe"] == null ? "" : "Nazwisko rodowe: " + this["NazwiskoRodowe"] + "\n";
            komunikat += this["PESEL"] == null ? "" : "PESEL: " + this["PESEL"] + "\n";
            komunikat += this["Nip"] == null ? "" : "Nip: " + this["Nip"] + "\n";
            komunikat += this["MiejsceUrodzenia"] == null ? "" : "Miejsce urodzenia: " + this["MiejsceUrodzenia"] + "\n";
            komunikat += this["ImieOjca"] == null ? "" : "Imię ojca: " + this["ImieOjca"] + "\n";
            komunikat += this["ImieMatki"] == null ? "" : "Imię matki: " + this["ImieMatki"] + "\n";
            komunikat += this["NazwiskoRodoweMatki"] == null ? "" : "Nazwisko rodowe matki: " + this["NazwiskoRodoweMatki"] + "\n";
            komunikat += this["Telefon"] == null ? "" : "Telefon: " + this["Telefon"] + "\n";
            komunikat += this["Email"] == null ? "" : "Email: " + this["Email"] + "\n";
            komunikat += this["Adres"] == null ? "" : "Adres: " + this["Adres"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
