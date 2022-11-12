using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
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
            : base("Nowa jedn. miary")
        {
            Item = new TowaryStawkiVat();
        }
        #endregion
        #region Properties
        public string KodKraju
        {
            get
            {
                return Item.KodKraju;
            }
            set
            {
                if (value != Item.KodKraju)
                {
                    Item.KodKraju = value;
                    base.OnPropertyChanged(() => KodKraju);
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
                if(value != Item.Stawka)
                {
                    Item.Stawka = value;
                    base.OnPropertyChanged(() => Stawka);
                }
            }
        }
        
        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.CzyAktywny = true;
            Db.TowaryStawkiVat.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
