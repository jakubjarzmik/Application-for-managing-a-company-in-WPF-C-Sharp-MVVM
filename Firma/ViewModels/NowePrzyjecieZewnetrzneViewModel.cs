using Firma.Helpers;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowePrzyjecieZewnetrzneViewModel : JedenViewModel<PrzyjeciaZewnetrzne>
    {
        #region Konstruktor
        public NowePrzyjecieZewnetrzneViewModel() : base("Nowe PZ")
        {
            Item = new PrzyjeciaZewnetrzne();
            Messenger.Default.Register<KontrahentForAllView>(this, DisplayName, getSelectedKontrahent);
            DataWystawienia = DateTime.Now;
            DataPrzyjecia= DateTime.Now;
            MagazynId = 1;
            Numer = "PZ" + DataWystawienia.ToString("yyMMddHHmmss");
        }
        public NowePrzyjecieZewnetrzneViewModel(PrzyjeciaZewnetrzne przyjecieZewnetrzne) : base("Edytuj PZ")
        {
            Item = przyjecieZewnetrzne;
            isEditing = true;
            Messenger.Default.Register<KontrahentForAllView>(this, DisplayName, getSelectedKontrahent);
        }
        #endregion
        #region Properties
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
        public int MagazynId
        {
            get
            {
                return Item.MagazynId;
            }
            set
            {
                if (value != Item.MagazynId)
                {
                    Item.MagazynId = value;
                    MagazynNazwa = Db.Magazyny.Where(n => n.MagazynId == MagazynId).Select(n => n.Nazwa).FirstOrDefault();
                    base.OnPropertyChanged(() => MagazynId);
                }
            }
        }
        public IQueryable<KeyAndValue> MagazynyComboBoxItems
        {
            get
            {
                return
                (
                    from magazyn in Db.Magazyny
                    where magazyn.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = magazyn.MagazynId,
                        Value = magazyn.Symbol
                    }
                ).ToList().AsQueryable();
            }
        }

        private String _MagazynNazwa;
        public string MagazynNazwa
        {
            get
            {
                return _MagazynNazwa;
            }
            set
            {
                if (value != _MagazynNazwa)
                {
                    _MagazynNazwa = value;
                    base.OnPropertyChanged(() => MagazynNazwa);
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
        public DateTime DataPrzyjecia
        {
            get
            {
                return Item.DataPrzyjecia;
            }
            set
            {
                if (value != Item.DataPrzyjecia)
                {
                    Item.DataPrzyjecia = value;
                    base.OnPropertyChanged(() => DataPrzyjecia);
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
        #region PozycjePZProperties
        private ObservableCollection<PozycjaPrzyjeciaZewnetrznegoForAllView> _PozycjePZList;
        public ObservableCollection<PozycjaPrzyjeciaZewnetrznegoForAllView> PozycjePZList
        {
            get
            {
                if (_PozycjePZList == null)
                    Load();
                return _PozycjePZList;
            }
            set
            {
                _PozycjePZList = value;
                OnPropertyChanged(() => PozycjePZList);
            }
        }
        public void Load()
        {
            PozycjePZList = new ObservableCollection<PozycjaPrzyjeciaZewnetrznegoForAllView>
                (
                    from pozycja in Db.PozycjePrzyjeciaZewnetrznego
                    where pozycja.CzyAktywny == true
                    && pozycja.PrzyjecieZewnetrzneId == Item.PrzyjecieZewnetrzneId
                    select new PozycjaPrzyjeciaZewnetrznegoForAllView
                    {
                        NumerPrzyjeciaZewnetrznego = pozycja.PrzyjeciaZewnetrzne.Numer,
                        NazwaTowaru = pozycja.Towary.Nazwa,
                        Ilosc = pozycja.Ilosc,
                        JednostkaMiary = pozycja.JednostkiMiary.Skrot,
                        PierwotnaCenaZakupu = pozycja.PierwotnaCenaZakupu,
                        Rabat = pozycja.Rabat,
                        CenaPoRabacieZaSzt = pozycja.PierwotnaCenaZakupu * (100 - pozycja.Rabat) / 100,
                        Wartosc = (pozycja.PierwotnaCenaZakupu * pozycja.Ilosc * (100 - pozycja.Rabat) / 100),
                    }
                );
        }
        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.KtoUtworzylId = 1;
            Item.CzyAktywny = true;
            Db.PrzyjeciaZewnetrzne.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        private void getSelectedKontrahent(KontrahentForAllView kontrahentForAllView)
        {
            KontrahentId = kontrahentForAllView.KontrahentId;
        }
        #endregion
    }
}
