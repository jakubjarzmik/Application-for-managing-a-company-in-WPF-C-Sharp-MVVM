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
    public class NoweWydanieZewnetrzneViewModel : JedenViewModel<WydaniaZewnetrzne>
    {
        #region Konstruktor
        public NoweWydanieZewnetrzneViewModel() : base("Nowe WZ")
        {
            Item = new WydaniaZewnetrzne();
            IsEnabled = false;
            Messenger.Default.Register<KontrahentForAllView>(this, DisplayName, getSelectedKontrahent);
            DataWystawienia = DateTime.Now;
            DataWydania = DateTime.Now;
            MagazynId = 1;
            Numer = "WZ" + DataWystawienia.ToString("yyMMddHHmmss");
        }
        public NoweWydanieZewnetrzneViewModel(WydaniaZewnetrzne wydanieZewnetrzne) : base("Edytuj WZ")
        {
            Item = wydanieZewnetrzne;
            isEditing= true;
            IsEnabled = true;
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
        public DateTime DataWydania
        {
            get
            {
                return Item.DataWydania;
            }
            set
            {
                if (value != Item.DataWydania)
                {
                    Item.DataWydania = value;
                    base.OnPropertyChanged(() => DataWydania);
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
        #region PozycjeWZProperties
        private ObservableCollection<PozycjaWydaniaZewnetrznegoForAllView> _PozycjeWZList;
        public ObservableCollection<PozycjaWydaniaZewnetrznegoForAllView> PozycjeWZList
        {
            get
            {
                if (_PozycjeWZList == null)
                    Load();
                return _PozycjeWZList;
            }
            set
            {
                _PozycjeWZList = value;
                OnPropertyChanged(() => PozycjeWZList);
            }
        }
        public void Load()
        {
            PozycjeWZList = new ObservableCollection<PozycjaWydaniaZewnetrznegoForAllView>
                (
                    from pozycja in Db.PozycjeWydaniaZewnetrznego
                    where pozycja.CzyAktywny == true
                    && pozycja.WydanieZewnetrzneId == Item.WydanieZewnetrzneId
                    select new PozycjaWydaniaZewnetrznegoForAllView
                    {
                        NumerWydaniaZewnetrznego = pozycja.WydaniaZewnetrzne.Numer,
                        NazwaTowaru = pozycja.Towary.Nazwa,
                        Ilosc = pozycja.Ilosc,
                        JednostkaMiary = pozycja.JednostkiMiary.Skrot,
                        Rabat = pozycja.Rabat
                    }
                );
        }
        #endregion
        #region Save
        protected override void add()
        {
            Messenger.Default.Send(new NowaPozycjaWydaniaZewnetrznegoViewModel(Item));
        }
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.KtoUtworzylId = 1;
            Item.CzyAktywny = true;
            Db.WydaniaZewnetrzne.AddObject(Item);
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
