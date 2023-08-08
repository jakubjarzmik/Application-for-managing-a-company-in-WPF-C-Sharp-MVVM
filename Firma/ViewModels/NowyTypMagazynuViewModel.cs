using Firma.Models.Entities;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using System;
using System.ComponentModel;

namespace Firma.ViewModels
{
    public class NowyTypMagazynuViewModel : JedenViewModel<MagazynyTypy>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyTypMagazynuViewModel() : base("Nowy typ magazynu")
        {
            Item = new MagazynyTypy();
        }
        public NowyTypMagazynuViewModel(MagazynyTypy typMagazynu) : base("Edytuj typ magazynu")
        {
            Item = typMagazynu;
            isEditing= true;
        }
        #endregion
        #region Properties
        public string Symbol
        {
            get
            {
                return Item.Symbol;
            }
            set
            {
                if(value != Item.Symbol)
                {
                    Item.Symbol = value;
                    base.OnPropertyChanged(() => Symbol);
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
                if(value != Item.Nazwa)
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
                if(value != Item.Opis)
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
                if(value != Item.Uwagi)
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
            Db.MagazynyTypy.AddObject(Item);
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
                    case "Symbol":
                        komunikat = StringValidator.CheckIsAllUpper(Symbol);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Symbol);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["Nazwa"] == null ? "" : "Nazwa: " + this["Nazwa"] + "\n";
            komunikat += this["Symbol"] == null ? "" : "Symbol: " + this["Symbol"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
