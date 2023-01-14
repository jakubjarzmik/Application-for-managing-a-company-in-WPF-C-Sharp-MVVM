﻿using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowyTowarViewModel : JedenViewModel<Towary>
    {
        #region Konstruktor
        public NowyTowarViewModel()
            : base("Nowy towar")
        {
            Item = new Towary();
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
                    from stawka in Db.TowaryStawkiVat
                    select new KeyAndValue
                    {
                        Key = stawka.StawkiVatId,
                        Value = stawka.Stawka.ToString("F2") + " | " + 
                            Db.Kraje.Where(n => n.KrajId == stawka.KrajId).Select(n=>n.ISO).FirstOrDefault()
                    }
                ).ToList().AsQueryable();
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
                    KrajPochodzeniaNazwa = Db.Kraje.Where(n => n.KrajId == KrajPochodzeniaId).Select(n => n.Nazwa).FirstOrDefault();
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
                    DomJednMiaryNazwa = Db.JednostkiMiary.Where(n => n.JednostkaId == DomJednMiaryId).Select(n => n.Nazwa).FirstOrDefault();
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
                    var kontrahent = Db.Kontrahenci.Where(n => n.KontrahentId == ProducentId).FirstOrDefault();
                    ProducentPelnaNazwa = kontrahent.Nazwa1 + " " + kontrahent.Nazwa2 + " " + kontrahent.Nazwa3;
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
                    where kontrahent.RodzajKontrahentaId == 1
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
    }
}
