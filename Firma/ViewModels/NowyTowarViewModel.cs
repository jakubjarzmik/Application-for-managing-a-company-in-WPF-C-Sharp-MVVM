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

namespace Firma.ViewModels
{
    public class NowyTowarViewModel : JedenViewModel<Towary>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyTowarViewModel() : base("Nowy towar")
        {
            Item = new Towary();
            IsEnabled = false;
            setMessengers();
        }
        public NowyTowarViewModel(Towary towar) : base("Edytuj towar")
        {
            Item = towar;
            isEditing = true;
            IsEnabled = true;
            setAllFields();
            setMessengers();
        }
        private void setMessengers()
        {
            Messenger.Default.Register<GrupaTowaruForAllView>(this, DisplayName, getSelectedGrupa);
            Messenger.Default.Register<StawkaVatAndIsZak>(this, DisplayName, getSelectedStawkaVat);
            Messenger.Default.Register<Kraje>(this, DisplayName, getSelectedKraj);
            Messenger.Default.Register<JednostkiMiary>(this, DisplayName, getJednostkaMiary);
            Messenger.Default.Register<KontrahentForAllView>(this, DisplayName, getSelectedKontrahent);
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
        public string NumerKatalogowy
        {
            get
            {
                return Item.NumerKatalogowy;
            }
            set
            {
                if (value != Item.NumerKatalogowy)
                {
                    Item.NumerKatalogowy = value;
                    base.OnPropertyChanged(() => NumerKatalogowy);
                }
            }
        }
        public int GrupaTowaruId
        {
            get
            {
                return Item.GrupaTowaruId;
            }
            set
            {
                if (value != Item.GrupaTowaruId)
                {
                    Item.GrupaTowaruId = value;
                    base.OnPropertyChanged(() => GrupaTowaruId);
                }
            }
        }
        public IQueryable<KeyAndValue> TowaryGrupyComboBoxItems
        {
            get
            {
                return
                (
                    from grupa in Db.TowaryGrupy
                    where grupa.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = grupa.GrupaTowaruId,
                        Value = grupa.Nazwa
                    }
                ).ToList().AsQueryable();
            }
        }
        public int TypTowaruId
        {
            get
            {
                return Item.TypTowaruId;
            }
            set
            {
                if (value != Item.TypTowaruId)
                {
                    Item.TypTowaruId = value;
                    base.OnPropertyChanged(() => TypTowaruId);
                }
            }
        }
        public IQueryable<KeyAndValue> TowaryTypyComboBoxItems
        {
            get
            {
                return
                (
                    from typ in Db.TowaryTypy
                    where typ.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = typ.TypTowaruId,
                        Value = typ.Nazwa
                    }
                ).ToList().AsQueryable();
            }
        }
        public string EAN
        {
            get
            {
                return Item.EAN;
            }
            set
            {
                if (value != Item.EAN)
                {
                    Item.EAN = value;
                    base.OnPropertyChanged(() => EAN);
                }
            }
        }
        public string SWW
        {
            get
            {
                return Item.SWW;
            }
            set
            {
                if (value != Item.SWW)
                {
                    Item.SWW = value;
                    base.OnPropertyChanged(() => SWW);
                }
            }
        }
        public int VatSprzId
        {
            get
            {
                return Item.VatSprzId;
            }
            set
            {
                if (value != Item.VatSprzId)
                {
                    Item.VatSprzId = value;
                    base.OnPropertyChanged(() => VatSprzId);
                }
            }
        }
        public int VatZakId
        {
            get
            {
                return Item.VatZakId;
            }
            set
            {
                if (value != Item.VatZakId)
                {
                    Item.VatZakId = value;
                    base.OnPropertyChanged(() => VatZakId);
                }
            }
        }
        public IQueryable<KeyAndValue> TowaryStawkiVatComboBoxItems
        {
            get
            {
                return
                (
                    from stawka in Db.TowaryStawkiVat.AsEnumerable()
                    where stawka.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = stawka.StawkiVatId,
                        Value = stawka.Stawka.ToString() + "%\t|\t" +
                            Db.Kraje.Where(n => n.KrajId == stawka.KrajId).Select(n => n.ISO).FirstOrDefault()
                    }
                ).AsQueryable();
            }
        }
        public bool Mpp
        {
            get
            {
                return Item.Mpp;
            }
            set
            {
                if (value != Item.Mpp)
                {
                    Item.Mpp = value;
                    base.OnPropertyChanged(() => Mpp);
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
        public string NazwaFiskalna
        {
            get
            {
                return Item.NazwaFiskalna;
            }
            set
            {
                if (value != Item.NazwaFiskalna)
                {
                    Item.NazwaFiskalna = value;
                    base.OnPropertyChanged(() => NazwaFiskalna);
                }
            }
        }
        public int KrajPochodzeniaId
        {
            get
            {
                return Item.KrajPochodzeniaId;
            }
            set
            {
                if (value != Item.KrajPochodzeniaId)
                {
                    Item.KrajPochodzeniaId = value;
                    setKrajPochodzeniaFields();
                    base.OnPropertyChanged(() => KrajPochodzeniaId);
                }
            }
        }
        public IQueryable<KeyAndValue> KrajeComboBoxItems
        {
            get
            {
                return
                (
                    from kraj in Db.Kraje
                    where kraj.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = kraj.KrajId,
                        Value = kraj.ISO
                    }
                ).ToList().AsQueryable();
            }
        }

        private string _KrajPochodzeniaNazwa;
        public string KrajPochodzeniaNazwa
        {
            get
            {
                return _KrajPochodzeniaNazwa;
            }
            set
            {
                if (value != _KrajPochodzeniaNazwa)
                {
                    _KrajPochodzeniaNazwa = value;
                    base.OnPropertyChanged(() => KrajPochodzeniaNazwa);
                }
            }
        }
        public int? DomJednMiaryId
        {
            get
            {
                return Item.DomJednMiaryId;
            }
            set
            {
                if (value != Item.DomJednMiaryId)
                {
                    Item.DomJednMiaryId = value;
                    setDomJednostkaMiaryFields();
                    base.OnPropertyChanged(() => DomJednMiaryId);
                }
            }
        }
        public IQueryable<KeyAndValue> JednostkiMiaryComboBoxItems
        {
            get
            {
                return
                (
                    from jednostka in Db.JednostkiMiary
                    where jednostka.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = jednostka.JednostkaId,
                        Value = jednostka.Skrot
                    }
                ).ToList().AsQueryable();
            }
        }

        private string _DomJednMiaryNazwa;
        public string DomJednMiaryNazwa
        {
            get
            {
                return _DomJednMiaryNazwa;
            }
            set
            {
                if (value != _DomJednMiaryNazwa)
                {
                    _DomJednMiaryNazwa = value;
                    base.OnPropertyChanged(() => DomJednMiaryNazwa);
                }
            }
        }
        public int ProducentId
        {
            get
            {
                return Item.ProducentId;
            }
            set
            {
                if (value != Item.ProducentId)
                {
                    Item.ProducentId = value;
                    setProducentFields();
                    base.OnPropertyChanged(() => ProducentId);
                }
            }
        }
        public IQueryable<KeyAndValue> ProducenciComboBoxItems
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

        private String _ProducentPelnaNazwa;
        public string ProducentPelnaNazwa
        {
            get
            {
                return _ProducentPelnaNazwa;
            }
            set
            {
                if (value != _ProducentPelnaNazwa)
                {
                    _ProducentPelnaNazwa = value;
                    base.OnPropertyChanged(() => ProducentPelnaNazwa);
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


        #endregion
        #region CenyProperties
        private ZmianaCenyForAllView _Selected;
        public ZmianaCenyForAllView Selected
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
        private ObservableCollection<ZmianaCenyForAllView> _CenyList;
        public ObservableCollection<ZmianaCenyForAllView> CenyList
        {
            get
            {
                if (_CenyList == null)
                    Load();
                return _CenyList;
            }
            set
            {
                _CenyList = value;
                OnPropertyChanged(() => CenyList);
            }
        }
        protected override void Load()
        {
            CenyList = new ObservableCollection<ZmianaCenyForAllView>
                (
                    from cena in Db.ZmianyCeny
                    where cena.CzyAktywny == true
                    && cena.TowarId == Item.TowarId
                    select new ZmianaCenyForAllView
                    {
                        ZmianaCenyId= cena.ZmianaCenyId,
                        JednostkaMiary = cena.JednostkiMiary.Skrot,
                        CenaNetto = cena.CenaNetto,
                        CenaBrutto = cena.CenaNetto * (100 + cena.Towary.TowaryStawkiVat.Stawka) / 100,
                        WartoscVat = cena.CenaNetto * cena.Towary.TowaryStawkiVat.Stawka / 100,
                        DataObowiazywaniaOd = cena.DataObowiazywaniaOd,
                        DataObowiazywaniaDo = cena.DataObowiazywaniaDo
                    }
                );
        }
        protected override void delete()
        {
            try
            {
                var toDelete = Db.ZmianyCeny.Where(a => a.ZmianaCenyId == Selected.ZmianaCenyId).FirstOrDefault();
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
            if (Item.EAN == null)
                Item.EAN = "";
            if (Item.SWW == null)
                Item.SWW = "";
            if (Item.NazwaFiskalna == null)
                Item.NazwaFiskalna = Item.Nazwa;
            if (Item.URL == null)
                Item.URL = "";
            if (Item.Uwagi == null)
                Item.Uwagi = "";
            if (Item.Opis == null)
                Item.Opis = "";
            Db.Towary.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        protected override void add()
        {
            Messenger.Default.Send(new NowaZmianaCenyViewModel(Item));
        }
        private void getSelectedGrupa(GrupaTowaruForAllView grupaTowaruForAllView)
        {
            GrupaTowaruId = grupaTowaruForAllView.GrupaTowaruId;
        }
        private void getSelectedStawkaVat(StawkaVatAndIsZak stawkaVatAndIsZak)
        {
            if (!stawkaVatAndIsZak.isVatZak)
                VatSprzId = stawkaVatAndIsZak.StawkaVatTowaruForAllView.StawkaVatId;
            else
                VatZakId = stawkaVatAndIsZak.StawkaVatTowaruForAllView.StawkaVatId;
        }
        private void getSelectedKraj(Kraje kraj)
        {
            KrajPochodzeniaId = kraj.KrajId;
        }
        private void getJednostkaMiary(JednostkiMiary jednostkiMiary)
        {
            DomJednMiaryId = jednostkiMiary.JednostkaId;
        }
        private void getSelectedKontrahent(KontrahentForAllView kontrahentForAllView)
        {
           ProducentId = kontrahentForAllView.KontrahentId;
        }
        private void setAllFields()
        {
            setKrajPochodzeniaFields();
            setDomJednostkaMiaryFields();
            setProducentFields();
        }
        private void setKrajPochodzeniaFields()
        {
            KrajPochodzeniaNazwa = Db.Kraje.Where(n => n.KrajId == KrajPochodzeniaId).Select(n => n.Nazwa).FirstOrDefault();
        }
        private void setDomJednostkaMiaryFields()
        {
            DomJednMiaryNazwa = Db.JednostkiMiary.Where(n => n.JednostkaId == DomJednMiaryId).Select(n => n.Nazwa).FirstOrDefault();
        }
        private void setProducentFields()
        {
            var kontrahent = Db.Kontrahenci.Where(n => n.KontrahentId == ProducentId).FirstOrDefault();
            ProducentPelnaNazwa = kontrahent.Nazwa1 + " " + kontrahent.Nazwa2 + " " + kontrahent.Nazwa3;
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
                    case "NumerKatalogowy":
                        komunikat = StringValidator.CheckIsAllUpper(NumerKatalogowy);
                        break;
                    case "GrupaTowaru":
                        komunikat = BusinessValidator.CheckIsSet(GrupaTowaruId);
                        break;
                    case "TypTowaru":
                        komunikat = BusinessValidator.CheckIsSet(TypTowaruId);
                        break;
                    case "EAN":
                        komunikat = StringValidator.CheckIsNumeric(EAN);
                        break;
                    case "SWW":
                        komunikat = StringValidator.CheckIsAllUpper(SWW);
                        break;
                    case "VatSprz":
                        komunikat = BusinessValidator.CheckIsSet(VatSprzId);
                        break;
                    case "VatZak":
                        komunikat = BusinessValidator.CheckIsSet(VatZakId);
                        break;
                    case "Nazwa":
                        komunikat = StringValidator.CheckIsStartsWithUpper(Nazwa);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Nazwa);
                        break;
                    case "NazwaFiskalna":
                        komunikat = StringValidator.CheckIsAllUpper(NazwaFiskalna);
                        break;
                    case "KrajPochodzenia":
                        komunikat = BusinessValidator.CheckIsSet(KrajPochodzeniaId);
                        break;
                    case "Producent":
                        komunikat = BusinessValidator.CheckIsSet(ProducentId);
                        break;
                    case "URL":
                        komunikat = BusinessValidator.CheckIsUrl(URL);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["Kod"] == null ? "" : "Kod: " + this["Kod"] + "\n";
            komunikat += this["NumerKatalogowy"] == null ? "" : "Numer katalogowy: " + this["NumerKatalogowy"] + "\n";
            komunikat += this["GrupaTowaru"] == null ? "" : "Grupa: " + this["GrupaTowaru"] + "\n";
            komunikat += this["TypTowaru"] == null ? "" : "Typ: " + this["TypTowaru"] + "\n";
            komunikat += this["EAN"] == null ? "" : "EAN: " + this["EAN"] + "\n";
            komunikat += this["SWW"] == null ? "" : "SWW: " + this["SWW"] + "\n";
            komunikat += this["VatSprz"] == null ? "" : "Vat sprzedaży: " + this["VatSprz"] + "\n";
            komunikat += this["VatZak"] == null ? "" : "Vat zakupu: " + this["VatZak"] + "\n";
            komunikat += this["Nazwa"] == null ? "" : "Nazwa: " + this["Nazwa"] + "\n";
            komunikat += this["NazwaFiskalna"] == null ? "" : "Nazwa fiskalna: " + this["NazwaFiskalna"] + "\n";
            komunikat += this["KrajPochodzenia"] == null ? "" : "Kraj pochodzenia: " + this["KrajPochodzenia"] + "\n";
            komunikat += this["Producent"] == null ? "" : "Producent: " + this["Producent"] + "\n";
            komunikat += this["URL"] == null ? "" : "URL: " + this["URL"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
