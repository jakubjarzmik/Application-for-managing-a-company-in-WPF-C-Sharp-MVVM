using Firma.Helpers;
using Firma.Models.BusinessLogic;
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
    public class NowaZmianaCenyViewModel : JedenViewModel<ZmianyCeny>
    {
        #region Konstruktor
        public NowaZmianaCenyViewModel() : base("Nowa zmiana ceny")
        {
            Item = new ZmianyCeny();
            IsEnabled = true;
            setMessengers();
            DataObowiazywaniaOd = DateTime.Now;
        }
        public NowaZmianaCenyViewModel(Towary towar) : base("Nowa zmiana ceny")
        {
            Item = new ZmianyCeny();
            TowarId = towar.TowarId;
            IsEnabled = false;
            setMessengers();
            DataObowiazywaniaOd = DateTime.Now;
        }
        public NowaZmianaCenyViewModel(ZmianyCeny zmianaCeny) : base("Edytuj zmianę ceny")
        {
            Item = zmianaCeny;
            isEditing = true;
            IsEnabled = false;
            setMessengers();
        }
        private void setMessengers()
        {
            Messenger.Default.Register<TowarForAllView>(this, DisplayName, getSelectedTowar);
            Messenger.Default.Register<JednostkiMiary>(this, DisplayName, getJednostkaMiary);
        }
        #endregion
        #region Properties
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
                    TowarNumerKatalogowy = Db.Towary.Where(n => n.TowarId == TowarId).Select(n => n.NumerKatalogowy).FirstOrDefault();
                    TowarNazwa = Db.Towary.Where(n => n.TowarId == TowarId).Select(n => n.Nazwa).FirstOrDefault();
                    var currentPriceChange = (from cena in Db.ZmianyCeny
                             where
                             cena.CzyAktywny == true &&
                             cena.TowarId == TowarId &&
                             DateTime.Now >= cena.DataObowiazywaniaOd
                             &&
                             (DateTime.Now <= cena.DataObowiazywaniaDo
                             || cena.DataObowiazywaniaDo == null)
                             orderby cena.DataObowiazywaniaOd descending
                             select cena);
                    CenaNetto = currentPriceChange.Select(n => n.CenaNetto).FirstOrDefault();
                    JednMiaryId = currentPriceChange.Select(n => n.JednMiaryId).FirstOrDefault();
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
                    from towar in Db.Towary
                    where towar.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = towar.TowarId,
                        Value = towar.NazwaFiskalna
                    }
                ).ToList().AsQueryable();
            }
        }
        private String _TowarNazwa;
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
        private String _TowarNumerKatalogowy;
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
        public int JednMiaryId
        {
            get
            {
                return Item.JednMiaryId;
            }
            set
            {
                if(value != Item.JednMiaryId)
                {
                    Item.JednMiaryId = value;
                    JednMiaryNazwa = Db.JednostkiMiary.Where(n => n.JednostkaId == JednMiaryId).Select(n => n.Nazwa).FirstOrDefault();
                    base.OnPropertyChanged(() => JednMiaryId);
                }
            }
        }
        public IQueryable<KeyAndValue> JednMiaryComboBoxItems
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
        private String _JednMiaryNazwa;
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
        public decimal CenaNetto
        {
            get
            {
                return Item.CenaNetto;
            }
            set
            {
                if (value != Item.CenaNetto)
                {
                    Item.CenaNetto = value;
                    int VatSprzedazyId = Db.Towary.Where(n=>n.TowarId == TowarId).Select(n=>n.VatSprzId).FirstOrDefault();
                    CenaBrutto = CenaNetto * (100 + Db.TowaryStawkiVat.Where(n=>n.StawkiVatId == VatSprzedazyId).Select(n=>n.Stawka).FirstOrDefault())/100;
                    base.OnPropertyChanged(() => CenaNetto);
                }
            }
        }
        private decimal _CenaBrutto;
        public decimal CenaBrutto
        {
            get
            {
                return _CenaBrutto;
            }
            set
            {
                if (value != _CenaBrutto)
                {
                    _CenaBrutto = value;
                    base.OnPropertyChanged(() => CenaBrutto);
                }
            }
        }
        public DateTime DataObowiazywaniaOd
        {
            get
            {
                return Item.DataObowiazywaniaOd;
            }
            set
            {
                if (value != Item.DataObowiazywaniaOd)
                {
                    Item.DataObowiazywaniaOd = value;
                    base.OnPropertyChanged(() => DataObowiazywaniaOd);
                }
            }
        }
        public DateTime? DataObowiazywaniaDo
        {
            get
            {
                return Item.DataObowiazywaniaDo;
            }
            set
            {
                if (value != Item.DataObowiazywaniaDo)
                {
                    Item.DataObowiazywaniaDo = value;
                    base.OnPropertyChanged(() => DataObowiazywaniaDo);
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
            Db.ZmianyCeny.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        private void getSelectedTowar(TowarForAllView towar)
        {
            TowarId = towar.TowarId;
        }
        private void getJednostkaMiary(JednostkiMiary jednostkiMiary)
        {
            JednMiaryId = jednostkiMiary.JednostkaId;
        }
        #endregion
    }
}
