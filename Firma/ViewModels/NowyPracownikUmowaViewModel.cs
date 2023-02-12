using Firma.Helpers;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowyPracownikUmowaViewModel : JedenViewModel<PracownicyUmowy>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyPracownikUmowaViewModel() : base("Nowa umowa pracownika")
        {
            Item = new PracownicyUmowy();
            IsEnabled = true;
            setMessengers();
        }
        public NowyPracownikUmowaViewModel(Pracownicy pracownik) : base("Nowa umowa pracownika")
        {
            Item = new PracownicyUmowy();
            PracownikId = pracownik.PracownikId;
            IsEnabled = false;
            setMessengers();
        }
        public NowyPracownikUmowaViewModel(PracownicyUmowy pracownikUmowa) : base("Edytuj umowę pracownika")
        {
            Item = pracownikUmowa;
            isEditing = true;
            IsEnabled = false;
            setAllFields();
            setMessengers();
        }
        private void setMessengers()
        {
            Messenger.Default.Register<PracownikForAllView>(this, DisplayName, getSelectedPracownik);
            Messenger.Default.Register<UmowaForAllView>(this, DisplayName, getSelectedUmowa);
        }
        #endregion
        #region Properties
        public int PracownikId
        {
            get
            {
                return Item.PracownikId;
            }
            set
            {
                if (value != Item.PracownikId)
                {
                    Item.PracownikId = value;
                    setPracownikFields();
                    base.OnPropertyChanged(() => PracownikId);
                }
            }
        }
        public IQueryable<KeyAndValue> PracownicyComboBoxItems
        {
            get
            {
                return
                (
                    from pracownik in Db.Pracownicy
                    where pracownik.CzyAktywny == true
                    let rodowe = pracownik.NazwiskoRodowe
                    select new KeyAndValue
                    {
                        Key = pracownik.PracownikId,
                        Value = pracownik.Imie + " " + pracownik.Nazwisko + (rodowe.Equals("")?" ("+rodowe+")":"")
                    }
                ).ToList().AsQueryable();
            }
        }
        private string _PracownikPESEL;
        public string PracownikPESEL
        {
            get
            {
                return _PracownikPESEL;
            }
            set
            {
                if (value != _PracownikPESEL)
                {
                    _PracownikPESEL = value;
                    base.OnPropertyChanged(() => PracownikPESEL);
                }
            }
        }
        private string _PracownikDataMiejsceUr;
        public string PracownikDataMiejsceUr
        {
            get
            {
                return _PracownikDataMiejsceUr;
            }
            set
            {
                if (value != _PracownikDataMiejsceUr)
                {
                    _PracownikDataMiejsceUr = value;
                    base.OnPropertyChanged(() => PracownikDataMiejsceUr);
                }
            }
        }
        private string _PracownikAdres;
        public string PracownikAdres
        {
            get
            {
                return _PracownikAdres;
            }
            set
            {
                if (value != _PracownikAdres)
                {
                    _PracownikAdres = value;
                    base.OnPropertyChanged(() => PracownikAdres);
                }
            }
        }
        private string _PracownikTelefon;
        public string PracownikTelefon
        {
            get
            {
                return _PracownikTelefon;
            }
            set
            {
                if (value != _PracownikTelefon)
                {
                    _PracownikTelefon = value;
                    base.OnPropertyChanged(() => PracownikTelefon);
                }
            }
        }
        private string _PracownikEmail;
        public string PracownikEmail
        {
            get
            {
                return _PracownikEmail;
            }
            set
            {
                if (value != _PracownikEmail)
                {
                    _PracownikEmail = value;
                    base.OnPropertyChanged(() => PracownikEmail);
                }
            }
        }
        public int UmowaId
        {
            get
            {
                return Item.UmowaId;
            }
            set
            {
                if (value != Item.UmowaId)
                {
                    Item.UmowaId = value;
                    setUmowaFields();
                    base.OnPropertyChanged(() => UmowaId);
                }
            }
        }
        public IQueryable<KeyAndValue> UmowyComboBoxItems
        {
            get
            {
                return
                (
                    from umowy in Db.Umowy
                    where umowy.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = umowy.UmowaId,
                        Value = umowy.NrUmowy
                    }
                ).ToList().AsQueryable();
            }
        }
        private string _UmowaRodzajNazwa;
        public string UmowaRodzajNazwa
        {
            get
            {
                return _UmowaRodzajNazwa;
            }
            set
            {
                if (value != _UmowaRodzajNazwa)
                {
                    _UmowaRodzajNazwa = value;
                    base.OnPropertyChanged(() => UmowaRodzajNazwa);
                }
            }
        }
        private string _UmowaStanowisko;
        public string UmowaStanowisko
        {
            get
            {
                return _UmowaStanowisko;
            }
            set
            {
                if (value != _UmowaStanowisko)
                {
                    _UmowaStanowisko = value;
                    base.OnPropertyChanged(() => UmowaStanowisko);
                }
            }
        }
        private DateTime _UmowaDataZawarcia;
        public DateTime UmowaDataZawarcia
        {
            get
            {
                return _UmowaDataZawarcia;
            }
            set
            {
                if (value != _UmowaDataZawarcia)
                {
                    _UmowaDataZawarcia = value;
                    base.OnPropertyChanged(() => UmowaDataZawarcia);
                }
            }
        }
        private DateTime _UmowaDataOd;
        public DateTime UmowaDataOd
        {
            get
            {
                return _UmowaDataOd;
            }
            set
            {
                if (value != _UmowaDataOd)
                {
                    _UmowaDataOd = value;
                    base.OnPropertyChanged(() => UmowaDataOd);
                }
            }
        }
        private DateTime _UmowaDataDo;
        public DateTime UmowaDataDo
        {
            get
            {
                return _UmowaDataDo;
            }
            set
            {
                if (value != _UmowaDataDo)
                {
                    _UmowaDataDo = value;
                    base.OnPropertyChanged(() => UmowaDataDo);
                }
            }
        }
        private int _UmowaCzasPracyMies;
        public int UmowaCzasPracyMies
        {
            get
            {
                return _UmowaCzasPracyMies;
            }
            set
            {
                if (value != _UmowaCzasPracyMies)
                {
                    _UmowaCzasPracyMies = value;
                    base.OnPropertyChanged(() => UmowaCzasPracyMies);
                }
            }
        }
        private decimal _UmowaWartosc;
        public decimal UmowaWartosc
        {
            get
            {
                return _UmowaWartosc;
            }
            set
            {
                if (value != _UmowaWartosc)
                {
                    _UmowaWartosc = value;
                    base.OnPropertyChanged(() => UmowaWartosc);
                }
            }
        }
        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.KtoUtworzylId = 1;
            Item.CzyAktywny = true;
            Db.PracownicyUmowy.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        private void getSelectedPracownik(PracownikForAllView pracownik)
        {
            PracownikId = pracownik.PracownikId;
        }
        private void getSelectedUmowa(UmowaForAllView umowa)
        {
            UmowaId = umowa.UmowaId;
        }
        private void setAllFields()
        {
            setPracownikFields();
            setUmowaFields();
        }
        private void setPracownikFields()
        {
            var pracownik = Db.Pracownicy.Where(n => n.PracownikId == PracownikId).FirstOrDefault();
            var adres = Db.Adresy.Where(n => n.AdresId == pracownik.AdresId).FirstOrDefault();
            PracownikAdres = adres.Ulica + " " + adres.NrDomu + (adres.NrLokalu.Equals("") ? "" : "/" + adres.NrLokalu) +
                ", " + adres.KodPocztowy + " " + adres.Miejscowosc;
            PracownikPESEL = pracownik.PESEL;
            PracownikDataMiejsceUr = pracownik.DataUrodzenia + ", " + pracownik.MiejsceUrodzenia;
            PracownikTelefon = pracownik.Telefon;
            PracownikEmail = pracownik.Email;
        }
        private void setUmowaFields()
        {
            var umowa = Db.Umowy.Where(n => n.UmowaId == UmowaId).FirstOrDefault();
            var rodzaj = Db.UmowyRodzaje.Where(n => n.RodzajUmowyId == umowa.RodzajUmowyId).FirstOrDefault();
            UmowaRodzajNazwa = rodzaj.Nazwa;
            var stanowisko = Db.UmowyStanowiska.Where(n => n.StanowiskoId == umowa.StanowiskoId).FirstOrDefault();
            UmowaStanowisko = stanowisko.Nazwa;
            UmowaDataZawarcia = umowa.DataZawarcia;
            UmowaDataOd = umowa.DataOd;
            UmowaDataDo = umowa.DataDo;
            UmowaCzasPracyMies = umowa.CzasPracyMies;
            UmowaWartosc = umowa.StawkaBruttoMies + umowa.StawkaBruttoGodz * umowa.CzasPracyMies;
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
                    case "Pracownik":
                        komunikat = BusinessValidator.CheckIsSet(PracownikId);
                        break;
                    case "Umowa":
                        komunikat = BusinessValidator.CheckIsSet(UmowaId);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["Pracownik"] == null ? "" : "Pracownik: " + this["Pracownik"] + "\n";
            komunikat += this["Umowa"] == null ? "" : "Umowa: " + this["Umowa"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
