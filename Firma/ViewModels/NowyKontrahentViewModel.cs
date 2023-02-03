using Firma.Helpers;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class NowyKontrahentViewModel : JedenViewModel<Kontrahenci>
    {
        #region Konstruktor
        public NowyKontrahentViewModel()
            : base("Nowy kontrahent")
        {
            Item = new Kontrahenci();
            Messenger.Default.Register<KontrahentForAllView>(this, DisplayName, getSelectedKontrahent);
            Messenger.Default.Register<AdresAndIsKor>(this, DisplayName, getSelectedAdres);
        }
        public NowyKontrahentViewModel(Kontrahenci kontrahent)
            : base("Edytuj kontrahenta")
        {
            Item = kontrahent;
            isEditing= true;
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
                    var kontrahent = Db.Kontrahenci.Where(n => n.KontrahentId == JednostkaPodlegaPodId).FirstOrDefault();
                    JednostkaPodlegaPodNazwa = kontrahent.Nazwa1 + " " + kontrahent.Nazwa2 + " " + kontrahent.Nazwa3;
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
        public ICommand ClearKorCommand
        {
            get
            {
                return new BaseCommand(() => ClearKor());
            }
        }
        private void ClearKor()
        {
            AdresKorId = null;
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
                    UlicaKor = "";
                    NrDomuKor = "";
                    NrLokaluKor = "";
                    KodPocztowyKor = "";
                    MiejscowoscKor = "";
                    WojewodztwoKor = "";
                    KrajKor = "";
                    DodatkoweKor = "";
                    base.OnPropertyChanged(() => AdresKorId);
                }
                else if (value != Item.AdresKorId)
                {
                    Item.AdresKorId = value;
                    var adres = Db.Adresy.Where(n => n.AdresId == AdresKorId).FirstOrDefault();
                    UlicaKor = adres.Ulica;
                    NrDomuKor = adres.NrDomu;
                    NrLokaluKor = adres.NrLokalu;
                    KodPocztowyKor = adres.KodPocztowy;
                    MiejscowoscKor = adres.Miejscowosc;
                    WojewodztwoKor = adres.Wojewodztwo;
                    KrajKor = Db.Kraje.Where(n => n.KrajId == adres.KrajId).Select(n => n.Nazwa).FirstOrDefault();
                    DodatkoweKor = adres.Dodatkowe;
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
        #endregion
    }
}
