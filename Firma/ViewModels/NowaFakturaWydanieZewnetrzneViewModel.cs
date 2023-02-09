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
    public class NowaFakturaWydanieZewnetrzneViewModel : JedenViewModel<FakturyWydaniaZewnetrzne>, IDataErrorInfo
    {
        #region Konstruktor
        public NowaFakturaWydanieZewnetrzneViewModel() : base("Nowa faktura WZ")
        {
            Item = new FakturyWydaniaZewnetrzne();
            IsEnabled= true;
            setMessengers();
        }
        public NowaFakturaWydanieZewnetrzneViewModel(Faktury faktura) : base("Nowa faktura WZ")
        {
            Item = new FakturyWydaniaZewnetrzne();
            FakturaId = faktura.FakturaId;
            IsEnabled = false;
            setMessengers();
        }
        public NowaFakturaWydanieZewnetrzneViewModel(FakturyWydaniaZewnetrzne fakturaWZ) : base("Edytuj fakturę WZ")
        {
            Item = fakturaWZ;
            isEditing= true;
            IsEnabled= false;
            setMessengers();
        }
        private void setMessengers()
        {
            Messenger.Default.Register<FakturaForAllView>(this, DisplayName, getSelectedFaktura);
            Messenger.Default.Register<WydanieZewnetrzneForAllView>(this, DisplayName, getSelectedWydanieZewnetrzne);
        }
        #endregion
        #region Properties
        public int FakturaId
        {
            get
            {
                return Item.FakturaId;
            }
            set
            {
                if(value != Item.FakturaId)
                {
                    Item.FakturaId = value;
                    var faktura = Db.Faktury.Where(n => n.FakturaId == FakturaId).FirstOrDefault();
                    var kontrahent = Db.Kontrahenci.Where(n=>n.KontrahentId == faktura.KontrahentId).FirstOrDefault();
                    FakturaKontrahentNazwa = kontrahent.Nazwa1 + " " + kontrahent.Nazwa2 + " " + kontrahent.Nazwa3;
                    FakturaDataWystawienia = faktura.DataWystawienia;
                    var rodzajPlatnosci = Db.RodzajePlatnosci.Where(n => n.RodzajPlatnosciId == faktura.RodzajePlatnosciId).FirstOrDefault();
                    FakturaRodzajPlatnosci = rodzajPlatnosci.Nazwa;
                    base.OnPropertyChanged(() => FakturaId);
                }
            }
        }
        public IQueryable<KeyAndValue> FakturyComboBoxItems
        {
            get
            {
                return
                (
                    from faktura in Db.Faktury
                    where faktura.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = faktura.FakturaId,
                        Value = faktura.Numer
                    }
                ).ToList().AsQueryable();
            }
        }
        private string _FakturaKontrahentNazwa;
        public string FakturaKontrahentNazwa
        {
            get
            {
                return _FakturaKontrahentNazwa;
            }
            set
            {
                if (value != _FakturaKontrahentNazwa)
                {
                    _FakturaKontrahentNazwa = value;
                    base.OnPropertyChanged(() => FakturaKontrahentNazwa);
                }
            }
        }
        private DateTime _FakturaDataWystawienia;
        public DateTime FakturaDataWystawienia
        {
            get
            {
                return _FakturaDataWystawienia;
            }
            set
            {
                if (value != _FakturaDataWystawienia)
                {
                    _FakturaDataWystawienia = value;
                    base.OnPropertyChanged(() => FakturaDataWystawienia);
                }
            }
        }
        private string _FakturaRodzajPlatnosci;
        public string FakturaRodzajPlatnosci
        {
            get
            {
                return _FakturaRodzajPlatnosci;
            }
            set
            {
                if (value != _FakturaRodzajPlatnosci)
                {
                    _FakturaRodzajPlatnosci = value;
                    base.OnPropertyChanged(() => FakturaRodzajPlatnosci);
                }
            }
        }
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
                    var wz = Db.WydaniaZewnetrzne.Where(n => n.WydanieZewnetrzneId == WydanieZewnetrzneId).FirstOrDefault();
                    WZMagazyn = Db.Magazyny.Where(n => n.MagazynId == wz.MagazynId).Select(n => n.Nazwa).FirstOrDefault();
                    WZDataWydania = wz.DataWydania;
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
        private string _WZMagazyn;
        public string WZMagazyn
        {
            get
            {
                return _WZMagazyn;
            }
            set
            {
                if (value != _WZMagazyn)
                {
                    _WZMagazyn = value;
                    base.OnPropertyChanged(() => WZMagazyn);
                }
            }
        }
        private DateTime _WZDataWydania;
        public DateTime WZDataWydania
        {
            get
            {
                return _WZDataWydania;
            }
            set
            {
                if (value != _WZDataWydania)
                {
                    _WZDataWydania = value;
                    base.OnPropertyChanged(() => WZDataWydania);
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
            Db.FakturyWydaniaZewnetrzne.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        private void getSelectedFaktura(FakturaForAllView faktura)
        {
            FakturaId = faktura.FakturaId;
        }
        private void getSelectedWydanieZewnetrzne(WydanieZewnetrzneForAllView wz)
        {
            WydanieZewnetrzneId = wz.WydanieZewnetrzneId;
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
                    case "Faktura":
                        komunikat = BusinessValidator.CheckIsSet(FakturaId);
                        break;
                    case "Wydanie zewnetrzne":
                        komunikat = BusinessValidator.CheckIsSet(WydanieZewnetrzneId);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["Faktura"] == null ? "" : "Faktura: " + this["Faktura"] + "\n";
            komunikat += this["Wydanie zewnetrzne"] == null ? "" : "Wydanie zewnetrzne: " + this["Wydanie zewnetrzne"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
