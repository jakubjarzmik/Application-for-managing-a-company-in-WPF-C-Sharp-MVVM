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
    public class NowyPracownikViewModel : JedenViewModel<Pracownicy>
    {
        #region Konstruktor
        public NowyPracownikViewModel() 
            : base("Nowy magazyn")
        {
            Item = new Pracownicy();
            DataUrodzenia = DateTime.Today;
        }
        #endregion
        #region Properties
        public string Imie
        {
            get
            {
                return Item.Imie;
            }
            set
            {
                if (value != Item.Imie)
                {
                    Item.Imie = value;
                    base.OnPropertyChanged(() => Imie);
                }
            }
        }
        public string DrugieImie
        {
            get
            {
                return Item.DrugieImie;
            }
            set
            {
                if(value != Item.DrugieImie)
                {
                    Item.DrugieImie = value;
                    base.OnPropertyChanged(() => DrugieImie);
                }
            }
        }
        public string Nazwisko
        {
            get
            {
                return Item.Nazwisko;
            }
            set
            {
                if(value != Item.Nazwisko)
                {
                    Item.Nazwisko = value;
                    base.OnPropertyChanged(() => Nazwisko);
                }
            }
        }
        public string NazwiskoRodowe
        {
            get
            {
                return Item.NazwiskoRodowe;
            }
            set
            {
                if(value != Item.NazwiskoRodowe)
                {
                    Item.NazwiskoRodowe = value;
                    base.OnPropertyChanged(() => NazwiskoRodowe);
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
                if(value != Item.PESEL)
                {
                    Item.PESEL = value;
                    base.OnPropertyChanged(() => PESEL);
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
                if(value != Item.Nip)
                {
                    Item.Nip = value;
                    base.OnPropertyChanged(() => Nip);
                }
            }
        }
        public DateTime DataUrodzenia
        {
            get
            {
                return Item.DataUrodzenia;
            }
            set
            {
                if(value != Item.DataUrodzenia)
                {
                    Item.DataUrodzenia = value;
                    base.OnPropertyChanged(() => DataUrodzenia);
                }
            }
        }
        public string MiejsceUrodzenia
        {
            get
            {
                return Item.MiejsceUrodzenia;
            }
            set
            {
                if (value != Item.MiejsceUrodzenia)
                {
                    Item.MiejsceUrodzenia = value;
                    base.OnPropertyChanged(() => MiejsceUrodzenia);
                }
            }
        }
        public string ImieOjca
        {
            get
            {
                return Item.ImieOjca;
            }
            set
            {
                if (value != Item.ImieOjca)
                {
                    Item.ImieOjca = value;
                    base.OnPropertyChanged(() => ImieOjca);
                }
            }
        }
        public string ImieMatki
        {
            get
            {
                return Item.ImieMatki;
            }
            set
            {
                if (value != Item.ImieMatki)
                {
                    Item.ImieMatki = value;
                    base.OnPropertyChanged(() => ImieMatki);
                }
            }
        }
        public string NazwiskoRodoweMatki
        {
            get
            {
                return Item.NazwiskoRodoweMatki;
            }
            set
            {
                if (value != Item.NazwiskoRodoweMatki)
                {
                    Item.NazwiskoRodoweMatki = value;
                    base.OnPropertyChanged(() => NazwiskoRodoweMatki);
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
                if (value != Item.Telefon)
                {
                    Item.Telefon = value;
                    base.OnPropertyChanged(() => Telefon);
                }
            }
        }
        public string Email
        {
            get
            {
                return Item.Email;
            }
            set
            {
                if (value != Item.Email)
                {
                    Item.Email = value;
                    base.OnPropertyChanged(() => Email);
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
        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.KtoUtworzylId = 1;
            Item.CzyAktywny = true;
            if (Item.DrugieImie == null)
                Item.DrugieImie = "";
            if (Item.NazwiskoRodowe == null)
                Item.NazwiskoRodowe = "";
            if (Item.PESEL == null)
                Item.PESEL = "";
            if (Item.Nip == null)
                Item.Nip = "";
            if (Item.ImieOjca == null)
                Item.ImieOjca = "";
            if (Item.ImieMatki == null)
                Item.ImieMatki = "";
            if (Item.NazwiskoRodoweMatki == null)
                Item.NazwiskoRodoweMatki = "";
            if (Item.Telefon == null)
                Item.Telefon = "";
            Db.Pracownicy.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
