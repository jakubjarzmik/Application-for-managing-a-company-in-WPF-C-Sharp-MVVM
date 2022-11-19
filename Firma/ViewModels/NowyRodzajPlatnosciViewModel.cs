using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowyRodzajPlatnosciViewModel : JedenViewModel<RodzajePlatnosci>
    {
        #region Konstruktor
        public NowyRodzajPlatnosciViewModel() 
            : base("Nowy rodzaj płatności")
        {
            Item = new RodzajePlatnosci();
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
        public int IloscDniSplaty
        {
            get
            {
                return Item.IloscDniSplaty;
            }
            set
            {
                if(value != Item.IloscDniSplaty)
                {
                    Item.IloscDniSplaty = value;
                    base.OnPropertyChanged(() => IloscDniSplaty);
                }
            }
        }
        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.CzyAktywny = true;
            Db.RodzajePlatnosci.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
