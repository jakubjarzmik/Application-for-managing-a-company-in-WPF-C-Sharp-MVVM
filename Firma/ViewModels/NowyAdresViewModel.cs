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
    public class NowyAdresViewModel : JedenViewModel<Adresy>
    {
        #region Konstruktor
        public NowyAdresViewModel()
            : base("Nowy adres")
        {
            Item = new Adresy();
            Messenger.Default.Register<Kraje>(this, DisplayName, getSelectedKraj);
        }
        #endregion
        #region Properties
        public string Ulica
        {
            get
            {
                return Item.Ulica;
            }
            set
            {
                if (value != Item.Ulica)
                {
                    Item.Ulica = value;
                    base.OnPropertyChanged(() => Ulica);
                }
            }
        }
        public string NrDomu
        {
            get
            {
                return Item.NrDomu;
            }
            set
            {
                if (value != Item.NrDomu)
                {
                    Item.NrDomu = value;
                    base.OnPropertyChanged(() => NrDomu);
                }
            }
        }
        public string NrLokalu
        {
            get
            {
                return Item.NrLokalu;
            }
            set
            {
                if (value != Item.NrLokalu)
                {
                    Item.NrLokalu = value;
                    base.OnPropertyChanged(() => NrLokalu);
                }
            }
        }
        public string KodPocztowy
        {
            get
            {
                return Item.KodPocztowy;
            }
            set
            {
                if (value != Item.KodPocztowy)
                {
                    Item.KodPocztowy = value;
                    base.OnPropertyChanged(() => KodPocztowy);
                }
            }
        }
        public string Miejscowosc
        {
            get
            {
                return Item.Miejscowosc;
            }
            set
            {
                if (value != Item.Miejscowosc)
                {
                    Item.Miejscowosc = value;
                    base.OnPropertyChanged(() => Miejscowosc);
                }
            }
        }
        public string Wojewodztwo
        {
            get
            {
                return Item.Wojewodztwo;
            }
            set
            {
                if (value != Item.Wojewodztwo)
                {
                    Item.Wojewodztwo = value;
                    base.OnPropertyChanged(() => Wojewodztwo);
                }
            }
        }
        public int KrajId
        {
            get
            {
                return Item.KrajId;
            }
            set
            {
                if (value != Item.KrajId)
                {
                    Item.KrajId = value;
                    KrajNazwa = Db.Kraje.Where(n => n.KrajId == KrajId).Select(n => n.Nazwa).FirstOrDefault();
                    base.OnPropertyChanged(() => KrajId);
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

        private string _KrajNazwa;
        public string KrajNazwa
        {
            get
            {
                return _KrajNazwa;
            }
            set
            {
                if (value != _KrajNazwa)
                {
                    _KrajNazwa = value;
                    base.OnPropertyChanged(() => KrajNazwa);
                }
            }
        }
        public string Dodatkowe
        {
            get
            {
                return Item.Dodatkowe;
            }
            set
            {
                if (value != Item.Dodatkowe)
                {
                    Item.Dodatkowe = value;
                    base.OnPropertyChanged(() => Dodatkowe);
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
            if (Item.NrLokalu == null)
                Item.NrLokalu = "";
            if (Item.Uwagi == null)
                Item.Uwagi = "";
            if (Item.Dodatkowe == null)
                Item.Dodatkowe = "";
            Db.Adresy.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        private void getSelectedKraj(Kraje kraj)
        {
            KrajId = kraj.KrajId;
        }
        #endregion
    }
}
