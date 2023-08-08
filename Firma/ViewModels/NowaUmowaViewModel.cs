using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Linq;

namespace Firma.ViewModels
{
    public class NowaUmowaViewModel : JedenViewModel<Umowy>, IDataErrorInfo
    {
        #region Konstruktor
        public NowaUmowaViewModel() : base("Nowa umowa")
        {
            Item = new Umowy();
            Messenger.Default.Register<UmowyStanowiska>(this, DisplayName, getSelectedStanowisko);
            DataZawarcia = DateTime.Now;
            DataOd = DateTime.Now;
            DataDo = DateTime.Now.AddYears(2);
            JestChorobowe = true;
            JestEmerytalne = true;
            JestRentowe = true;
            JestWypadkowe = true;
            RodzajUmowyId = 1;
        }
        public NowaUmowaViewModel(Umowy umowa) : base("Edytuj umowę")
        {
            Item = umowa;
            isEditing = true;
            setAllFields();
            Messenger.Default.Register<UmowyStanowiska>(this, DisplayName, getSelectedStanowisko);
        }
        #endregion
        #region Properties
        public string NrUmowy
        {
            get
            {
                return Item.NrUmowy;
            }
            set
            {
                if(value != Item.NrUmowy)
                {
                    Item.NrUmowy = value;
                    base.OnPropertyChanged(() => NrUmowy);
                }
            }
        }
        public int RodzajUmowyId
        {
            get
            {
                return Item.RodzajUmowyId;
            }
            set
            {
                if(value != Item.RodzajUmowyId)
                {
                    Item.RodzajUmowyId = value;
                    setRodzajUmowyFields();
                    base.OnPropertyChanged(() => RodzajUmowyId);
                }
            }
        }
        public IQueryable<KeyAndValue> RodzajeUmowyComboBoxItems
        {
            get
            {
                return
                (
                    from rodzaj in Db.UmowyRodzaje
                    where rodzaj.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = rodzaj.RodzajUmowyId,
                        Value = rodzaj.Kod
                    }
                ).ToList().AsQueryable();
            }
        }
        private String _RodzajUmowyNazwa;
        public string RodzajUmowyNazwa
        {
            get
            {
                return _RodzajUmowyNazwa;
            }
            set
            {
                if (value != _RodzajUmowyNazwa)
                {
                    _RodzajUmowyNazwa = value;
                    base.OnPropertyChanged(() => RodzajUmowyNazwa);
                }
            }
        }
        public int StanowiskoId
        {
            get
            {
                return Item.StanowiskoId;
            }
            set
            {
                if(value != Item.StanowiskoId)
                {
                    Item.StanowiskoId = value;
                    setStanowiskoFields();
                    base.OnPropertyChanged(() => StanowiskoId);
                }
            }
        }
        public IQueryable<KeyAndValue> StanowiskaComboBoxItems
        {
            get
            {
                return
                (
                    from stanowisko in Db.UmowyStanowiska
                    where stanowisko.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = stanowisko.StanowiskoId,
                        Value = stanowisko.Nazwa
                    }
                ).ToList().AsQueryable();
            }
        }
        private String _StanowiskoKodZawodu;
        public string StanowiskoKodZawodu
        {
            get
            {
                return _StanowiskoKodZawodu;
            }
            set
            {
                if (value != _StanowiskoKodZawodu)
                {
                    _StanowiskoKodZawodu = value;
                    base.OnPropertyChanged(() => StanowiskoKodZawodu);
                }
            }
        }
        public DateTime DataZawarcia
        {
            get
            {
                return Item.DataZawarcia;
            }
            set
            {
                if (value != Item.DataZawarcia)
                {
                    Item.DataZawarcia = value;
                    base.OnPropertyChanged(() => DataZawarcia);
                }
            }
        }
        public DateTime DataOd
        {
            get
            {
                return Item.DataOd;
            }
            set
            {
                if (value != Item.DataOd)
                {
                    Item.DataOd = value;
                    base.OnPropertyChanged(() => DataOd);
                }
            }
        }
        public DateTime DataDo
        {
            get
            {
                return Item.DataDo;
            }
            set
            {
                if (value != Item.DataDo)
                {
                    Item.DataDo = value;
                    base.OnPropertyChanged(() => DataDo);
                }
            }
        }
        public decimal StawkaBruttoMies
        {
            get
            {
                return Item.StawkaBruttoMies;
            }
            set
            {
                if(value != Item.StawkaBruttoMies)
                {
                    Item.StawkaBruttoMies = value;
                    base.OnPropertyChanged(()=>StawkaBruttoMies);
                }
            }
        }
        public decimal StawkaBruttoGodz
        {
            get
            {
                return Item.StawkaBruttoGodz;
            }
            set
            {
                if(value != Item.StawkaBruttoGodz)
                {
                    Item.StawkaBruttoGodz = value;
                    base.OnPropertyChanged(()=> StawkaBruttoGodz);
                }
            }
        }
        public int CzasPracyMies
        {
            get
            {
                return Item.CzasPracyMies;
            }
            set
            {
                if(value != Item.CzasPracyMies)
                {
                    Item.CzasPracyMies = value;
                    base.OnPropertyChanged(()=> CzasPracyMies);
                }
            }
        }
        public bool JestEmerytalne
        {
            get
            {
                return Item.JestEmerytalne;
            }
            set
            {
                if(value != Item.JestEmerytalne)
                {
                    Item.JestEmerytalne = value;
                    base.OnPropertyChanged(()=> JestEmerytalne);
                }
            }
        }
        public bool JestRentowe
        {
            get
            {
                return Item.JestRentowe;
            }
            set
            {
                if(value != Item.JestRentowe)
                {
                    Item.JestRentowe = value;
                    base.OnPropertyChanged(()=> JestRentowe);
                }
            }
        }
        public bool JestChorobowe
        {
            get
            {
                return Item.JestChorobowe;
            }
            set
            {
                if(value != Item.JestChorobowe)
                {
                    Item.JestChorobowe = value;
                    base.OnPropertyChanged(()=> JestChorobowe);
                }
            }
        }
        public bool JestWypadkowe
        {
            get
            {
                return Item.JestWypadkowe;
            }
            set
            {
                if(value != Item.JestWypadkowe)
                {
                    Item.JestWypadkowe = value;
                    base.OnPropertyChanged(()=> JestWypadkowe);
                }
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
        #endregion
        #region Save
        public override void Save()
        {
            
            Item.DataUtworzenia = DateTime.Now;
            Item.KtoUtworzylId = 1;
            Item.CzyAktywny = true;
            if (Item.Opis == null)
                Item.Opis = "";
            Db.Umowy.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        private void getSelectedStanowisko(UmowyStanowiska stanowisko)
        {
            StanowiskoId = stanowisko.StanowiskoId;
        }
        private void setAllFields()
        {
            setRodzajUmowyFields();
            setStanowiskoFields();
        }
        private void setRodzajUmowyFields()
        {
            NrUmowy = Db.UmowyRodzaje.Where(n => n.RodzajUmowyId == RodzajUmowyId).Select(n => n.Kod).FirstOrDefault() + DataZawarcia.ToString("yyMMddHHmmss");
            RodzajUmowyNazwa = Db.UmowyRodzaje.Where(n => n.RodzajUmowyId == RodzajUmowyId).Select(n => n.Nazwa).FirstOrDefault();
        }
        private void setStanowiskoFields()
        {
            StanowiskoKodZawodu = Db.UmowyStanowiska.Where(n => n.StanowiskoId == StanowiskoId).Select(n => n.KodZawodu).FirstOrDefault();
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
                    case "Rodzaj":
                        komunikat = BusinessValidator.CheckIsSet(RodzajUmowyId);
                        break;
                    case "Stanowisko":
                        komunikat = BusinessValidator.CheckIsSet(StanowiskoId);
                        break;
                    case "StawkaBruttoMies":
                        komunikat = BusinessValidator.CheckIsNotLessThanZero(StawkaBruttoMies);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(StawkaBruttoMies);
                        break;
                    case "StawkaBruttoGodz":
                        komunikat = BusinessValidator.CheckIsNotLessThanZero(StawkaBruttoGodz);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(StawkaBruttoGodz);
                        break;
                    case "CzasPracyMies":
                        komunikat = BusinessValidator.CheckIsNotLessThanZero(CzasPracyMies);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(CzasPracyMies);
                        break;
                    case "DataDo":
                        komunikat = BusinessValidator.CheckDateIsNotEarlier(DataOd, DataDo);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(DataOd);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(DataDo);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["Rodzaj"] == null ? "" : "Rodzaj: " + this["Rodzaj"] + "\n";
            komunikat += this["Stanowisko"] == null ? "" : "Stanowisko: " + this["Stanowisko"] + "\n";
            komunikat += this["StawkaBruttoMies"] == null ? "" : "Stawka brutto (mies): " + this["StawkaBruttoMies"] + "\n";
            komunikat += this["StawkaBruttoGodz"] == null ? "" : "Stawka brutto (godz): " + this["StawkaBruttoGodz"] + "\n";
            komunikat += this["CzasPracyMies"] == null ? "" : "Miesięczny czas pracy (godz): " + this["CzasPracyMies"] + "\n";
            komunikat += this["DataDo"] == null ? "" : "Obowiązuje do: " + this["DataDo"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
