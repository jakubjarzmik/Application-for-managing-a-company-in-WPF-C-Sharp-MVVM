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
    public class NowyKontrahentKontaktViewModel : JedenViewModel<KontrahenciKontakty>
    {
        #region Konstruktor
        public NowyKontrahentKontaktViewModel() : base("Nowy kontrahent kontakt")
        {
            Item = new KontrahenciKontakty();
            setMessengers();
        }
        public NowyKontrahentKontaktViewModel(KontrahenciKontakty kontrahentKontakt) : base("Edytuj kontrahent kontakt")
        {
            Item = kontrahentKontakt;
            isEditing = true;
            setMessengers();
        }
        private void setMessengers()
        {
            Messenger.Default.Register<KontrahentForAllView>(this, DisplayName, getSelectedKontrahent);
            Messenger.Default.Register<Kontakty>(this, DisplayName, getSelectedKontakt);
        }
        #endregion
        #region Properties
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
                    KontrahentNazwa = kontrahent.Nazwa1 + " " + kontrahent.Nazwa2 + " " + kontrahent.Nazwa3;
                    KontrahentNIP = kontrahent.Nip;
                    var adres = Db.Adresy.Where(n => n.AdresId == kontrahent.AdresId).FirstOrDefault();
                    KontrahentAdres = adres.Ulica + " " + adres.NrDomu + (adres.NrLokalu.Equals("") ? "" : "/" + adres.NrLokalu) +
                        ", " + adres.KodPocztowy + " " + adres.Miejscowosc;
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
        private string _KontrahentNazwa;
        public string KontrahentNazwa
        {
            get
            {
                return _KontrahentNazwa;
            }
            set
            {
                if (value != _KontrahentNazwa)
                {
                    _KontrahentNazwa = value;
                    base.OnPropertyChanged(() => KontrahentNazwa);
                }
            }
        }
        private string _KontrahentNIP;
        public string KontrahentNIP
        {
            get
            {
                return _KontrahentNIP;
            }
            set
            {
                if (value != _KontrahentNIP)
                {
                    _KontrahentNIP = value;
                    base.OnPropertyChanged(() => KontrahentNIP);
                }
            }
        }
        private string _KontrahentAdres;
        public string KontrahentAdres
        {
            get
            {
                return _KontrahentAdres;
            }
            set
            {
                if (value != _KontrahentAdres)
                {
                    _KontrahentAdres = value;
                    base.OnPropertyChanged(() => KontrahentAdres);
                }
            }
        }
        public int KontaktId
        {
            get
            {
                return Item.KontaktId;
            }
            set
            {
                if (value != Item.KontaktId)
                {
                    Item.KontaktId = value;
                    var kontakt = Db.Kontakty.Where(n => n.KontaktId == KontaktId).FirstOrDefault();
                    KontaktNazwaDzialu = kontakt.NazwaDzialu;
                    KontaktTelefon1= kontakt.Telefon1;
                    KontaktTelefon2 = kontakt.Telefon2;
                    KontaktEmail1= kontakt.Email1;
                    KontaktEmail2= kontakt.Email2;
                    base.OnPropertyChanged(() => KontaktId);
                }
            }
        }
        public IQueryable<KeyAndValue> KontaktyComboBoxItems
        {
            get
            {
                return
                (
                    from kontakt in Db.Kontakty
                    where kontakt.CzyAktywny == true
                    select new KeyAndValue
                    {
                        Key = kontakt.KontaktId,
                        Value = kontakt.OpisOsoby
                    }
                ).ToList().AsQueryable();
            }
        }
        private string _KontaktNazwaDzialu;
        public string KontaktNazwaDzialu
        {
            get
            {
                return _KontaktNazwaDzialu;
            }
            set
            {
                if (value != _KontaktNazwaDzialu)
                {
                    _KontaktNazwaDzialu = value;
                    base.OnPropertyChanged(() => KontaktNazwaDzialu);
                }
            }
        }
        private string _KontaktTelefon1;
        public string KontaktTelefon1
        {
            get
            {
                return _KontaktTelefon1;
            }
            set
            {
                if (value != _KontaktTelefon1)
                {
                    _KontaktTelefon1 = value;
                    base.OnPropertyChanged(() => KontaktTelefon1);
                }
            }
        }
        private string _KontaktTelefon2;
        public string KontaktTelefon2
        {
            get
            {
                return _KontaktTelefon2;
            }
            set
            {
                if (value != _KontaktTelefon2)
                {
                    _KontaktTelefon2 = value;
                    base.OnPropertyChanged(() => KontaktTelefon2);
                }
            }
        }
        private string _KontaktEmail1;
        public string KontaktEmail1
        {
            get
            {
                return _KontaktEmail1;
            }
            set
            {
                if (value != _KontaktEmail1)
                {
                    _KontaktEmail1 = value;
                    base.OnPropertyChanged(() => KontaktEmail1);
                }
            }
        }
        private string _KontaktEmail2;
        public string KontaktEmail2
        {
            get
            {
                return _KontaktEmail2;
            }
            set
            {
                if (value != _KontaktEmail2)
                {
                    _KontaktEmail2 = value;
                    base.OnPropertyChanged(() => KontaktEmail2);
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
            Db.KontrahenciKontakty.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        private void getSelectedKontrahent(KontrahentForAllView kontrahent)
        {
            KontrahentId = kontrahent.KontrahentId;
        }
        private void getSelectedKontakt(Kontakty kontakt)
        {
            KontaktId = kontakt.KontaktId;
        }
        #endregion
    }
}
