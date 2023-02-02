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
    public class NowaStawkaVatTowarowViewModel : JedenViewModel<TowaryStawkiVat>
    {
        #region Konstruktor
        public NowaStawkaVatTowarowViewModel() 
            : base("Nowa stawka VAT")
        {
            Item = new TowaryStawkiVat();
            Messenger.Default.Register<Kraje>(this, DisplayName, getSelectedKraj);
        }
        #endregion
        #region Properties
        public int KrajId
        {
            get
            {
                return Item.KrajId;
            }
            set
            {
                if(value != Item.KrajId)
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
        private String _KrajNazwa;
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
        public decimal Stawka
        {
            get
            {
                return Item.Stawka;
            }
            set
            {
                if (value != Item.Stawka)
                {
                    Item.Stawka = value;
                    base.OnPropertyChanged(() => Stawka);
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
        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.KtoUtworzylId = 1;
            Item.CzyAktywny = true;
            if (Item.Opis == null)
                Item.Opis = "";
            Db.TowaryStawkiVat.AddObject(Item);
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
