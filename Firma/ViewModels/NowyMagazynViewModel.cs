using Firma.Helpers;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class NowyMagazynViewModel : JedenViewModel<Magazyny>
    {
        #region Konstruktor
        public NowyMagazynViewModel() 
            : base("Nowy magazyn")
        {
            Item = new Magazyny();
        }
        #endregion
        #region Properties
        public string Symbol
        {
            get
            {
                return Item.Symbol;
            }
            set
            {
                if (value != Item.Symbol)
                {
                    Item.Symbol = value;
                    base.OnPropertyChanged(() => Symbol);
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
                if(value != Item.Nazwa)
                {
                    Item.Nazwa = value;
                    base.OnPropertyChanged(() => Nazwa);
                }
            }
        }
        public string Telefon
        {
            get
            {
                return Item.Telefon;
            }
            set
            {
                if(value != Item.Telefon)
                {
                    Item.Telefon = value;
                    base.OnPropertyChanged(() => Telefon);
                }
            }
        }
        public int TypMagazynuId
        {
            get
            {
                return Item.TypMagazynuId;
            }
            set
            {
                if(value != Item.TypMagazynuId)
                {
                    Item.TypMagazynuId = value;
                    base.OnPropertyChanged(() => TypMagazynuId);
                }
            }
        }
        public IQueryable<KeyAndValue> MagazynyTypyComboBoxItems
        {
            get
            {
                return
                (
                    from typ in Db.MagazynyTypy
                    select new KeyAndValue
                    {
                        Key = typ.TypMagazynuId,
                        Value = typ.Nazwa
                    }
                ).ToList().AsQueryable();
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
        public ICommand ClearAdresCommand
        {
            get
            {
                return new BaseCommand(() => ClearAdres());
            }
        }
        private void ClearAdres()
        {
            AdresId = null;
        }
        public int? AdresId
        {
            get
            {
                return Item.AdresId;
            }
            set
            {
                if (value == null)
                {
                    Item.AdresId = value;
                    Ulica = "";
                    NrDomu = "";
                    NrLokalu = "";
                    KodPocztowy = "";
                    Miejscowosc = "";
                    Wojewodztwo = "";
                    Kraj = "";
                    Dodatkowe = "";
                    base.OnPropertyChanged(() => AdresId);
                }
                else if (value != Item.AdresId)
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
        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.KtoUtworzylId = 1;
            Item.CzyAktywny = true;
            if (Item.Opis == null)
                Item.Opis = "";
            if (Item.Telefon == null)
                Item.Telefon = "";
            Db.Magazyny.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
