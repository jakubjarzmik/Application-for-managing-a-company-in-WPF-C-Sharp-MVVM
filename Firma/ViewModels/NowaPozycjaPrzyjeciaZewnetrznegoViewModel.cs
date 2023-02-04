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

namespace Firma.ViewModels
{
    public class NowaPozycjaPrzyjeciaZewnetrznegoViewModel : JedenViewModel<PozycjePrzyjeciaZewnetrznego>
    {
        #region Konstruktor
        public NowaPozycjaPrzyjeciaZewnetrznegoViewModel() : base("Nowa pozycja PZ")
        {
            Item = new PozycjePrzyjeciaZewnetrznego();
            IsEnabled= true;
            setMessengers();
        }
        public NowaPozycjaPrzyjeciaZewnetrznegoViewModel(PrzyjeciaZewnetrzne przyjecieZewnetrzne) : base("Nowa pozycja PZ")
        {
            Item = new PozycjePrzyjeciaZewnetrznego();
            PrzyjecieZewnetrzneId = przyjecieZewnetrzne.PrzyjecieZewnetrzneId;
            IsEnabled = false;
            setMessengers();
        }
        public NowaPozycjaPrzyjeciaZewnetrznegoViewModel(PozycjePrzyjeciaZewnetrznego pozycjaPZ) : base("Edytuj pozycję PZ")
        {
            Item = pozycjaPZ;
            isEditing= true;
            IsEnabled= false;
            setMessengers();
        }
        private void setMessengers()
        {
            Messenger.Default.Register<PrzyjecieZewnetrzneForAllView>(this, DisplayName, getSelectedPrzyjecieZewnetrzne);
            Messenger.Default.Register<TowarForAllView>(this, DisplayName, getSelectedTowar);
            Messenger.Default.Register<JednostkiMiary>(this, DisplayName, getSelectedJednMiary);
        }
        #endregion
        #region Properties
        public int PrzyjecieZewnetrzneId
        {
            get
            {
                return Item.PrzyjecieZewnetrzneId;
            }
            set
            {
                if(value != Item.PrzyjecieZewnetrzneId)
                {
                    Item.PrzyjecieZewnetrzneId = value;
                    var przyjecieZewnetrzne = Db.PrzyjeciaZewnetrzne.Where(n => n.PrzyjecieZewnetrzneId == PrzyjecieZewnetrzneId).FirstOrDefault();
                    var kontrahent = Db.Kontrahenci.Where(n=>n.KontrahentId == przyjecieZewnetrzne.KontrahentId).FirstOrDefault();
                    PZKontrahentNazwa = kontrahent.Nazwa1 + " " + kontrahent.Nazwa2 + " " + kontrahent.Nazwa3;
                    var magazyn = Db.Magazyny.Where(n => n.MagazynId == przyjecieZewnetrzne.MagazynId).FirstOrDefault();
                    PZMagazynNazwa = magazyn.Nazwa;
                    PZDataPrzyjecia = przyjecieZewnetrzne.DataPrzyjecia;
                    PZRabat = przyjecieZewnetrzne.Rabat;
                    base.OnPropertyChanged(() => PrzyjecieZewnetrzneId);
                }
            }
        }
        public IQueryable<KeyAndValue> PrzyjeciaZewnetrzneComboBoxItems
        {
            get
            {
                return
                (
                    from pz in Db.PrzyjeciaZewnetrzne
                    where pz.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = pz.PrzyjecieZewnetrzneId,
                        Value = pz.Numer
                    }
                ).ToList().AsQueryable();
            }
        }
        private string _PZKontrahentNazwa;
        public string PZKontrahentNazwa
        {
            get
            {
                return _PZKontrahentNazwa;
            }
            set
            {
                if (value != _PZKontrahentNazwa)
                {
                    _PZKontrahentNazwa = value;
                    base.OnPropertyChanged(() => PZKontrahentNazwa);
                }
            }
        }
        private DateTime _PZDataPrzyjecia;
        public DateTime PZDataPrzyjecia
        {
            get
            {
                return _PZDataPrzyjecia;
            }
            set
            {
                if (value != _PZDataPrzyjecia)
                {
                    _PZDataPrzyjecia = value;
                    base.OnPropertyChanged(() => PZDataPrzyjecia);
                }
            }
        }
        private string _PZMagazynNazwa;
        public string PZMagazynNazwa
        {
            get
            {
                return _PZMagazynNazwa;
            }
            set
            {
                if (value != _PZMagazynNazwa)
                {
                    _PZMagazynNazwa = value;
                    base.OnPropertyChanged(() => PZMagazynNazwa);
                }
            }
        }
        private decimal _PZRabat;
        public decimal PZRabat
        {
            get
            {
                return _PZRabat;
            }
            set
            {
                if (value != _PZRabat)
                {
                    _PZRabat = value;
                    base.OnPropertyChanged(() => PZRabat);
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
        public decimal PierwotnaCenaZakupu
        {
            get
            {
                return Item.PierwotnaCenaZakupu;
            }
            set
            {
                if (value != Item.PierwotnaCenaZakupu)
                {
                    Item.PierwotnaCenaZakupu = value;
                    base.OnPropertyChanged(() => PierwotnaCenaZakupu);
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
            Db.PozycjePrzyjeciaZewnetrznego.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        private void getSelectedPrzyjecieZewnetrzne(PrzyjecieZewnetrzneForAllView pz)
        {
            PrzyjecieZewnetrzneId = pz.PrzyjecieZewnetrzneId;
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
    }
}
