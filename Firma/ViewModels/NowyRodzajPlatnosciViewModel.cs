using Firma.Models.Entities;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowyRodzajPlatnosciViewModel : JedenViewModel<RodzajePlatnosci>, IDataErrorInfo
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
        #region Validation
        public string Error
        {
            get
            {
                return null;
            }
        }
        public string this[string name]
        {
            get
            {
                string komunikat = null;
                switch (name)
                {
                    case "Nazwa":
                        komunikat = StringValidator.CheckIsStartsWithUpper(Nazwa);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Nazwa);
                        break;
                    case "IloscDniSplaty":
                        komunikat = BusinessValidator.CheckIsNotLessThanZero(IloscDniSplaty);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(IloscDniSplaty);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["Nazwa"] == null ? "" : "Nazwa: " + this["Nazwa"] + "\n";
            komunikat += this["IloscDniSplaty"] == null ? "" : "Ilość dni do spłaty: " + this["IloscDniSplaty"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
