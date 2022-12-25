using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowyKrajViewModel : JedenViewModel<Kraje>
    {
        #region Konstruktor
        public NowyKrajViewModel() 
            : base("Nowy rodzaj kontr.")
        {
            Item = new Kraje();
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
        public string ISO
        {
            get
            {
                return Item.ISO;
            }
            set
            {
                if(value != Item.ISO)
                {
                    Item.ISO = value;
                    base.OnPropertyChanged(() => ISO);
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
            Db.Kraje.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
