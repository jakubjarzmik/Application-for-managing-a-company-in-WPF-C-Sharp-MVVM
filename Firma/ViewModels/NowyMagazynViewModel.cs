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
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class NowyMagazynViewModel : JedenViewModel<Magazyny>, IDataErrorInfo
    {
        #region Commands
        public ICommand ClearAdresCommand
        {
            get
            {
                return new BaseCommand(() => ClearAdres());
            }
        }
        #endregion
        #region Konstruktor
        public NowyMagazynViewModel() : base("Nowy magazyn")
        {
            Item = new Magazyny();
            Messenger.Default.Register<AdresAndIsKor>(this, DisplayName, getSelectedAdres);
        }
        public NowyMagazynViewModel(Magazyny magazyn) : base("Edytuj magazyn")
        {
            Item = magazyn;
            isEditing = true;
            setAllFields();
            Messenger.Default.Register<AdresAndIsKor>(this, DisplayName, getSelectedAdres);
        }
        #endregion
        #region Properties
        public string Symbol
        {
            get
            {
                return Item.Symbol;
            }
            set
            {
                if (value != Item.Symbol)
                {
                    Item.Symbol = value;
                    base.OnPropertyChanged(() => Symbol);
                }
            }
        }
        public string Nazwa
        {
            get
            {
                return Item.Nazwa;
            }
            set
            {
                if (value != Item.Nazwa)
                {
                    Item.Nazwa = value;
                    base.OnPropertyChanged(() => Nazwa);
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
        public int TypMagazynuId
        {
            get
            {
                return Item.TypMagazynuId;
            }
            set
            {
                if (value != Item.TypMagazynuId)
                {
                    Item.TypMagazynuId = value;
                    base.OnPropertyChanged(() => TypMagazynuId);
                }
            }
        }
        public IQueryable<KeyAndValue> MagazynyTypyComboBoxItems
        {
            get
            {
                return
                (
                    from typ in Db.MagazynyTypy
                    where typ.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = typ.TypMagazynuId,
                        Value = typ.Nazwa
                    }
                ).ToList().AsQueryable();
            }
        }
        public string Opis
        {
            get
            {
                return Item.Opis;
            }
            set
            {
                if (value != Item.Opis)
                {
                    Item.Opis = value;
                    base.OnPropertyChanged(() => Opis);
                }
            }
        }
        public int? AdresId
        {
            get
            {
                return Item.AdresId;
            }
            set
            {
                if (value == null)
                {
                    Item.AdresId = value;
                    base.OnPropertyChanged(() => AdresId);
                }
                else if (value != Item.AdresId)
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
        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.KtoUtworzylId = 1;
            Item.CzyAktywny = true;
            if (Item.Opis == null)
                Item.Opis = "";
            if (Item.Telefon == null)
                Item.Telefon = "";
            Db.Magazyny.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        private void getSelectedAdres(AdresAndIsKor adresAndIsKor)
        {
            AdresId = adresAndIsKor.AdresForAllView.AdresId;
        }
        private void ClearAdres()
        {
            AdresId = null;
            Ulica = "";
            NrDomu = "";
            NrLokalu = "";
            KodPocztowy = "";
            Miejscowosc = "";
            Wojewodztwo = "";
            Kraj = "";
            Dodatkowe = "";
        }
        private void setAllFields()
        {
            setAdresFields();
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
                    case "Symbol":
                        komunikat = StringValidator.CheckIsAllUpper(Symbol);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Symbol);
                        break;
                    case "Nazwa":
                        komunikat = StringValidator.CheckIsStartsWithUpper(Nazwa);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Nazwa);
                        break;
                    case "Telefon":
                        komunikat = StringValidator.CheckIsNumeric(Telefon);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Telefon);
                        break;
                    case "TypMagazynu":
                        komunikat = BusinessValidator.CheckIsSet(TypMagazynuId);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["Symbol"] == null ? "" : "Symbol: " + this["Symbol"] + "\n";
            komunikat += this["Nazwa"] == null ? "" : "Nazwa: " + this["Nazwa"] + "\n";
            komunikat += this["Telefon"] == null ? "" : "Telefon: " + this["Telefon"] + "\n";
            komunikat += this["TypMagazynu"] == null ? "" : "Typ: " + this["TypMagazynu"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
