using Firma.Models.Entities;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using System;
using System.ComponentModel;

namespace Firma.ViewModels
{
    public class NowyRodzajUmowyViewModel : JedenViewModel<UmowyRodzaje>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyRodzajUmowyViewModel() : base("Nowy rodzaj umowy")
        {
            Item = new UmowyRodzaje();
        }
        public NowyRodzajUmowyViewModel(UmowyRodzaje rodzajUmowy) : base("Edytuj rodzaj umowy")
        {
            Item = rodzajUmowy;
            isEditing= true;
        }
        #endregion
        #region Properties
        public string Kod
        {
            get
            {
                return Item.Kod;
            }
            set
            {
                if (value != Item.Kod)
                {
                    Item.Kod = value;
                    base.OnPropertyChanged(() => Kod);
                }
            }
        }
        public string Nazwa
        {
            get
            {
                return Item.Nazwa;
            }
            set
            {
                if (value != Item.Nazwa)
                {
                    Item.Nazwa = value;
                    base.OnPropertyChanged(() => Nazwa);
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
            Db.UmowyRodzaje.AddObject(Item);
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
                    case "Kod":
                        komunikat = StringValidator.CheckIsAllUpper(Kod);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Kod);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["Nazwa"] == null ? "" : "Nazwa: " + this["Nazwa"] + "\n";
            komunikat += this["Kod"] == null ? "" : "Kod: " + this["Kod"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
