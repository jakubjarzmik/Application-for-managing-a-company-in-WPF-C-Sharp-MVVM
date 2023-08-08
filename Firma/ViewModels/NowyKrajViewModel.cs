using Firma.Models.Entities;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using System;
using System.ComponentModel;

namespace Firma.ViewModels
{
    public class NowyKrajViewModel : JedenViewModel<Kraje>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyKrajViewModel() : base("Nowy kraj")
        {
            Item = new Kraje();
        }
        public NowyKrajViewModel(Kraje kraj) : base("Edytuj kraj")
        {
            Item = kraj;
            isEditing = true;
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
                if (value != Item.Nazwa)
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
                if (value != Item.ISO)
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
                    case "ISO":
                        komunikat = StringValidator.CheckIsAllUpper(ISO);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(ISO);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["Nazwa"] == null ? "" : "Nazwa: " + this["Nazwa"] + "\n";
            komunikat += this["ISO"] == null ? "" : "ISO: " + this["ISO"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
