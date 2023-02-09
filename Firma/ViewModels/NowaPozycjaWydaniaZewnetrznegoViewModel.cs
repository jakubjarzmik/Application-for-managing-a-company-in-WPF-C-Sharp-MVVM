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
    public class NowaPozycjaWydaniaZewnetrznegoViewModel : JedenViewModel<PozycjeWydaniaZewnetrznego>, IDataErrorInfo
    {
        #region Konstruktor
        public NowaPozycjaWydaniaZewnetrznegoViewModel() : base("Nowa pozycja WZ")
        {
            Item = new PozycjeWydaniaZewnetrznego();
            IsEnabled= true;
            setMessengers();
        }
        public NowaPozycjaWydaniaZewnetrznegoViewModel(WydaniaZewnetrzne wydanieZewnetrzne) : base("Nowa pozycja WZ")
        {
            Item = new PozycjeWydaniaZewnetrznego();
            WydanieZewnetrzneId = wydanieZewnetrzne.WydanieZewnetrzneId;
            IsEnabled = false;
            setMessengers();
        }
        public NowaPozycjaWydaniaZewnetrznegoViewModel(PozycjeWydaniaZewnetrznego pozycjaWZ) : base("Edytuj pozycję WZ")
        {
            Item = pozycjaWZ;
            isEditing= true;
            IsEnabled= false;
            setMessengers();
        }
        private void setMessengers()
        {
            Messenger.Default.Register<WydanieZewnetrzneForAllView>(this, DisplayName, getSelectedWydanieZewnetrzne);
            Messenger.Default.Register<TowarForAllView>(this, DisplayName, getSelectedTowar);
        }
        #endregion
        #region Properties
        public int WydanieZewnetrzneId
        {
            get
            {
                return Item.WydanieZewnetrzneId;
            }
            set
            {
                if(value != Item.WydanieZewnetrzneId)
                {
                    Item.WydanieZewnetrzneId = value;
                    var wydanieZewnetrzne = Db.WydaniaZewnetrzne.Where(n => n.WydanieZewnetrzneId == WydanieZewnetrzneId).FirstOrDefault();
                    var kontrahent = Db.Kontrahenci.Where(n=>n.KontrahentId == wydanieZewnetrzne.KontrahentId).FirstOrDefault();
                    WZKontrahentNazwa = kontrahent.Nazwa1 + " " + kontrahent.Nazwa2 + " " + kontrahent.Nazwa3;
                    var magazyn = Db.Magazyny.Where(n => n.MagazynId == wydanieZewnetrzne.MagazynId).FirstOrDefault();
                    WZMagazynNazwa = magazyn.Nazwa;
                    WZDataWydania = wydanieZewnetrzne.DataWydania;
                    WZRabat = wydanieZewnetrzne.Rabat;
                    base.OnPropertyChanged(() => WydanieZewnetrzneId);
                }
            }
        }
        public IQueryable<KeyAndValue> WydaniaZewnetrzneComboBoxItems
        {
            get
            {
                return
                (
                    from wz in Db.WydaniaZewnetrzne
                    where wz.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = wz.WydanieZewnetrzneId,
                        Value = wz.Numer
                    }
                ).ToList().AsQueryable();
            }
        }
        private string _WZKontrahentNazwa;
        public string WZKontrahentNazwa
        {
            get
            {
                return _WZKontrahentNazwa;
            }
            set
            {
                if (value != _WZKontrahentNazwa)
                {
                    _WZKontrahentNazwa = value;
                    base.OnPropertyChanged(() => WZKontrahentNazwa);
                }
            }
        }
        private DateTime _WZDataPrzyjecia;
        public DateTime WZDataWydania
        {
            get
            {
                return _WZDataPrzyjecia;
            }
            set
            {
                if (value != _WZDataPrzyjecia)
                {
                    _WZDataPrzyjecia = value;
                    base.OnPropertyChanged(() => WZDataWydania);
                }
            }
        }
        private string _WZMagazynNazwa;
        public string WZMagazynNazwa
        {
            get
            {
                return _WZMagazynNazwa;
            }
            set
            {
                if (value != _WZMagazynNazwa)
                {
                    _WZMagazynNazwa = value;
                    base.OnPropertyChanged(() => WZMagazynNazwa);
                }
            }
        }
        private decimal _WZRabat;
        public decimal WZRabat
        {
            get
            {
                return _WZRabat;
            }
            set
            {
                if (value != _WZRabat)
                {
                    _WZRabat = value;
                    base.OnPropertyChanged(() => WZRabat);
                }
            }
        }
        public int TowarId
        {
            get
            {
                return Item.TowarId;
            }
            set
            {
                if(value != Item.TowarId)
                {
                    Item.TowarId = value;
                    var towar = Db.Towary.Where(n=>n.TowarId==TowarId).FirstOrDefault();
                    TowarNumerKatalogowy = towar.NumerKatalogowy;
                    TowarNazwa = towar.Nazwa;
                    var grupaTowaru = Db.TowaryGrupy.Where(n=>n.GrupaTowaruId == towar.GrupaTowaruId).FirstOrDefault();
                    TowarGrupaNazwa = grupaTowaru.Nazwa;
                    var producent = Db.Kontrahenci.Where(n=>n.KontrahentId == towar.ProducentId).FirstOrDefault();
                    TowarProducentNazwa = producent.Nazwa1 + " " + producent.Nazwa2 + " " + producent.Nazwa3;
                    var jednMiary = Db.JednostkiMiary.Where(n=>n.JednostkaId == towar.DomJednMiaryId).FirstOrDefault();
                    TowarDomJednMiaryNazwa = jednMiary.Nazwa;
                    base.OnPropertyChanged(() => TowarId);
                }
            }
        }
        public IQueryable<KeyAndValue> TowaryComboBoxItems
        {
            get
            {
                return
                (
                    from towary in Db.Towary
                    where towary.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = towary.TowarId,
                        Value = towary.Kod
                    }
                ).ToList().AsQueryable();
            }
        }
        private string _TowarNumerKatalogowy;
        public string TowarNumerKatalogowy
        {
            get
            {
                return _TowarNumerKatalogowy;
            }
            set
            {
                if (value != _TowarNumerKatalogowy)
                {
                    _TowarNumerKatalogowy = value;
                    base.OnPropertyChanged(() => TowarNumerKatalogowy);
                }
            }
        }
        private string _TowarNazwa;
        public string TowarNazwa
        {
            get
            {
                return _TowarNazwa;
            }
            set
            {
                if (value != _TowarNazwa)
                {
                    _TowarNazwa = value;
                    base.OnPropertyChanged(() => TowarNazwa);
                }
            }
        }
        private string _TowarProducentNazwa;
        public string TowarProducentNazwa
        {
            get
            {
                return _TowarProducentNazwa;
            }
            set
            {
                if (value != _TowarProducentNazwa)
                {
                    _TowarProducentNazwa = value;
                    base.OnPropertyChanged(() => TowarProducentNazwa);
                }
            }
        }
        private string _TowarDomJednMiaryNazwa;
        public string TowarDomJednMiaryNazwa
        {
            get
            {
                return _TowarDomJednMiaryNazwa;
            }
            set
            {
                if (value != _TowarDomJednMiaryNazwa)
                {
                    _TowarDomJednMiaryNazwa = value;
                    base.OnPropertyChanged(() => TowarDomJednMiaryNazwa);
                }
            }
        }
        private string _TowarGrupaNazwa;
        public string TowarGrupaNazwa
        {
            get
            {
                return _TowarGrupaNazwa;
            }
            set
            {
                if (value != _TowarGrupaNazwa)
                {
                    _TowarGrupaNazwa = value;
                    base.OnPropertyChanged(() => TowarGrupaNazwa);
                }
            }
        }
        public int JednMiaryId
        {
            get
            {
                return Item.JednMiaryId;
            }
            set
            {
                if (value != Item.JednMiaryId)
                {
                    Item.JednMiaryId = value;
                    JednMiaryNazwa = Db.JednostkiMiary.Where(n=>n.JednostkaId == JednMiaryId).Select(n=>n.Nazwa).FirstOrDefault();
                    base.OnPropertyChanged(() => JednMiaryId);
                }
            }
        }
        public IQueryable<KeyAndValue> JednostkiMiaryComboBoxItems
        {
            get
            {
                return
                (
                    from jednMiary in Db.JednostkiMiary
                    where jednMiary.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = jednMiary.JednostkaId,
                        Value = jednMiary.Skrot
                    }
                ).ToList().AsQueryable();
            }
        }
        private string _JednMiaryNazwa;
        public string JednMiaryNazwa
        {
            get
            {
                return _JednMiaryNazwa;
            }
            set
            {
                if (value != _JednMiaryNazwa)
                {
                    _JednMiaryNazwa = value;
                    base.OnPropertyChanged(() => JednMiaryNazwa);
                }
            }
        }
        public int Ilosc
        {
            get
            {
                return Item.Ilosc;
            }
            set
            {
                if (value != Item.Ilosc)
                {
                    Item.Ilosc = value;
                    base.OnPropertyChanged(() => Ilosc);
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
        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.KtoUtworzylId = 1;
            Item.CzyAktywny = true;
            Db.PozycjeWydaniaZewnetrznego.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        private void getSelectedWydanieZewnetrzne(WydanieZewnetrzneForAllView wz)
        {
            WydanieZewnetrzneId = wz.WydanieZewnetrzneId;
        }
        private void getSelectedTowar(TowarForAllView towar)
        {
            TowarId = towar.TowarId;
        }
        private void getSelectedJednMiary(JednostkiMiary jednMiary)
        {
            JednMiaryId = jednMiary.JednostkaId;
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
                    case "Towar":
                        komunikat = BusinessValidator.CheckIsSet(TowarId);
                        break;
                    case "Wydanie zewnetrzne":
                        komunikat = BusinessValidator.CheckIsSet(WydanieZewnetrzneId);
                        break;
                    case "Jednostka miary":
                        komunikat = BusinessValidator.CheckIsSet(JednMiaryId);
                        break;
                    case "Ilosc":
                        komunikat = BusinessValidator.CheckIsNotLessThanZero(Ilosc);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Ilosc);
                        break;
                    case "Rabat":
                        komunikat = BusinessValidator.CheckBetween0and100(Rabat);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Rabat);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["Towar"] == null ? "" : "Towar: " + this["Towar"] + "\n";
            komunikat += this["Wydanie zewnetrzne"] == null ? "" : "Wydanie zewnętrzne: " + this["Wydanie zewnetrzne"] + "\n";
            komunikat += this["Jednostka miary"] == null ? "" : "Jednostka miary: " + this["Jednostka miary"] + "\n";
            komunikat += this["Ilosc"] == null ? "" : "Ilość: " + this["Ilosc"] + "\n";
            komunikat += this["Rabat"] == null ? "" : "Rabat: " + this["Rabat"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
