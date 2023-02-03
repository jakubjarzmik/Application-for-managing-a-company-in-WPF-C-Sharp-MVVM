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
        public NowyRodzajPlatnosciViewModel() : base("Nowy rodzaj płatności")
        {
            Item = new RodzajePlatnosci();
        }
        public NowyRodzajPlatnosciViewModel(RodzajePlatnosci rodzajPlatnosci) : base("Edytuj rodzaj płatności")
        {
            Item = rodzajPlatnosci;
            isEditing= true;
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
            if (Item.Uwagi == null)
                Item.Uwagi = "";
            if (Item.Opis == null)
                Item.Opis = "";
            Db.RodzajePlatnosci.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
