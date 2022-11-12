using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowaGrupaTowarowViewModel : JedenViewModel<TowaryGrupy>
    {
        #region Konstruktor
        public NowaGrupaTowarowViewModel() 
            : base("Nowy rodzaj ceny")
        {
            Item = new TowaryGrupy();
        }
        #endregion
        #region Properties
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
        public string Kod
        {
            get
            {
                return Item.Kod;
            }
            set
            {
                if(value != Item.Kod)
                {
                    Item.Kod = value;
                    base.OnPropertyChanged(() => Kod);
                }
            }
        }
        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.CzyAktywny = true;
            Db.TowaryGrupy.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
