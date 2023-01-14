using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowaFakturaViewModel : JedenViewModel<Faktury>
    {
        #region Konstruktor
        public NowaFakturaViewModel()
            : base("Nowa faktura")
        {
            Item = new Faktury();
            DataWystawienia = DateTime.Now;
            DataSprzedazy= DateTime.Now;
            RodzajFakturyId = 1;
        }
        #endregion
        #region Properties
        public int RodzajFakturyId
        {
            get
            {
                return Item.RodzajFakturyId;
            }
            set
            {
                if (value != Item.RodzajFakturyId)
                {
                    Item.RodzajFakturyId = value;
                    Numer = Db.FakturyRodzaje.Where(n => n.RodzajFakturyId == RodzajFakturyId).Select(n => n.Kod).FirstOrDefault() + DataWystawienia.ToString("yyMMddHHmmss");
                    RodzajFakturyNazwa = Db.FakturyRodzaje.Where(n => n.RodzajFakturyId == RodzajFakturyId).Select(n => n.Nazwa).FirstOrDefault();
                    base.OnPropertyChanged(() => RodzajFakturyId);
                }
            }
        }
        public IQueryable<KeyAndValue> FakturyRodzajeComboBoxItems
        {
            get
            {
                return
                (
                    from rodzaj in Db.FakturyRodzaje
                    select new KeyAndValue
                    {
                        Key = rodzaj.RodzajFakturyId,
                        Value = rodzaj.Kod
                    }
                ).ToList().AsQueryable();
            }
        }

        private String _RodzajFakturyNazwa;
        public string RodzajFakturyNazwa
        {
            get
            {
                return _RodzajFakturyNazwa;
            }
            set
            {
                if (value != _RodzajFakturyNazwa)
                {
                    _RodzajFakturyNazwa = value;
                    base.OnPropertyChanged(() => RodzajFakturyNazwa);
                }
            }
        }
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
        public int KategoriaFakturyId
        {
            get
            {
                return Item.KategoriaFakturyId;
            }
            set
            {
                if (value != Item.KategoriaFakturyId)
                {
                    Item.KategoriaFakturyId = value;
                    KategoriaFakturyOpis = Db.FakturyKategorie.Where(n => n.KategoriaFakturyId == KategoriaFakturyId).Select(n=>n.Opis).FirstOrDefault();
                    base.OnPropertyChanged(() => KategoriaFakturyId);
                }
            }
        }
        public IQueryable<KeyAndValue> FakturyKategorieComboBoxItems
        {
            get
            {
                return
                (
                    from kategoria in Db.FakturyKategorie
                    select new KeyAndValue
                    {
                        Key = kategoria.KategoriaFakturyId,
                        Value = kategoria.Nazwa
                    }
                ).ToList().AsQueryable();
            }
        }

        private String _KategoriaFakturyOpis;
        public string KategoriaFakturyOpis
        {
            get
            {
                return _KategoriaFakturyOpis;
            }
            set
            {
                if (value != _KategoriaFakturyOpis)
                {
                    _KategoriaFakturyOpis = value;
                    base.OnPropertyChanged(() => KategoriaFakturyOpis);
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
        public DateTime DataSprzedazy
        {
            get
            {
                return Item.DataSprzedazy;
            }
            set
            {
                if (value != Item.DataSprzedazy)
                {
                    Item.DataSprzedazy = value;
                    base.OnPropertyChanged(() => DataSprzedazy);
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
        public int RodzajePlatnosciId
        {
            get
            {
                return Item.RodzajePlatnosciId;
            }
            set
            {
                if (value != Item.RodzajePlatnosciId)
                {
                    Item.RodzajePlatnosciId = value;
                    var IloscDniSplaty = Db.RodzajePlatnosci.Where(n => n.RodzajPlatnosciId == RodzajePlatnosciId).Select(n => n.IloscDniSplaty).FirstOrDefault();
                    TerminPlatnosci = DateTime.Now.AddDays(IloscDniSplaty);
                    base.OnPropertyChanged(() => RodzajePlatnosciId);
                }
            }
        }
        public IQueryable<KeyAndValue> RodzajePlatnosciComboBoxItems
        {
            get
            {
                return
                (
                    from rodzaj in Db.RodzajePlatnosci
                    select new KeyAndValue
                    {
                        Key = rodzaj.RodzajPlatnosciId,
                        Value = rodzaj.Nazwa
                    }
                ).ToList().AsQueryable();
            }
        }
        private DateTime _TerminPlatnosci;
        public DateTime TerminPlatnosci
        {
            get
            {
                return _TerminPlatnosci;
            }
            set
            {
                if (value != _TerminPlatnosci)
                {
                    _TerminPlatnosci = value;
                    base.OnPropertyChanged(() => TerminPlatnosci);
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
            Db.Faktury.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
