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
    public class NowyKontaktViewModel : JedenViewModel<Kontakty>, IDataErrorInfo
    {
        #region Konstruktor
        public NowyKontaktViewModel() : base("Nowy kontakt")
        {
            Item = new Kontakty();
        }
        public NowyKontaktViewModel(Kontakty kontakt) : base("Edytuj kontakt")
        {
            Item = kontakt;
            isEditing = true;
        }
        #endregion
        #region Properties
        public string NazwaDzialu
        {
            get
            {
                return Item.NazwaDzialu;
            }
            set
            {
                if (value != Item.NazwaDzialu)
                {
                    Item.NazwaDzialu = value;
                    base.OnPropertyChanged(() => NazwaDzialu);
                }
            }
        }
        public string OpisOsoby
        {
            get
            {
                return Item.OpisOsoby;
            }
            set
            {
                if (value != Item.OpisOsoby)
                {
                    Item.OpisOsoby = value;
                    base.OnPropertyChanged(() => OpisOsoby);
                }
            }
        }

        public string Telefon1
        {
            get
            {
                return Item.Telefon1;
            }
            set
            {
                if (value != Item.Telefon1)
                {
                    Item.Telefon1 = value;
                    base.OnPropertyChanged(() => Telefon1);
                }
            }
        }

        public string Telefon2
        {
            get
            {
                return Item.Telefon2;
            }
            set
            {
                if (value != Item.Telefon2)
                {
                    Item.Telefon2 = value;
                    base.OnPropertyChanged(() => Telefon2);
                }
            }
        }

        public string Fax
        {
            get
            {
                return Item.Fax;
            }
            set
            {
                if (value != Item.Fax)
                {
                    Item.Fax = value;
                    base.OnPropertyChanged(() => Fax);
                }
            }
        }

        public string Email1
        {
            get
            {
                return Item.Email1;
            }
            set
            {
                if (value != Item.Email1)
                {
                    Item.Email1 = value;
                    base.OnPropertyChanged(() => Email1);
                }
            }
        }

        public string Email2
        {
            get
            {
                return Item.Email2;
            }
            set
            {
                if (value != Item.Email2)
                {
                    Item.Email2 = value;
                    base.OnPropertyChanged(() => Email2);
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
            Item.CzyAktywny = true;
            setInformationFields();
            checkNulls();
            Db.Kontakty.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers

        private void setInformationFields()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.KtoUtworzylId = 1;
        }
        private void checkNulls()
        {
            if (Item.NazwaDzialu == null)
                Item.NazwaDzialu = "";
            if (Item.OpisOsoby == null)
                Item.OpisOsoby = "";
            if (Item.Telefon1 == null)
                Item.Telefon1 = "";
            if (Item.Telefon2 == null)
                Item.Telefon2 = "";
            if (Item.Fax == null)
                Item.Fax = "";
            if (Item.Email1 == null)
                Item.Email1 = "";
            if (Item.Email2 == null)
                Item.Email2 = "";
            if (Item.Uwagi == null)
                Item.Uwagi = "";
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
                    case "NazwaDzialu":
                        komunikat = StringValidator.CheckIsStartsWithUpper(NazwaDzialu);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(NazwaDzialu);
                        break;
                    case "OpisOsoby":
                        komunikat = StringValidator.CheckIsStartsWithUpper(OpisOsoby);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(OpisOsoby);
                        break;
                    case "Telefon1":
                        komunikat = StringValidator.CheckIsNumeric(Telefon1);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Telefon1);
                        break;
                    case "Telefon2":
                        komunikat = StringValidator.CheckIsNumeric(Telefon2);
                        break;
                    case "Email1":
                        komunikat = StringValidator.CheckIsEmail(Email1);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Email1);
                        break;
                    case "Email2":
                        komunikat = StringValidator.CheckIsEmail(Email2);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["NazwaDzialu"] == null ? "" : "Nazwa działu: " + this["NazwaDzialu"] + "\n";
            komunikat += this["OpisOsoby"] == null ? "" : "Opis osoby: " + this["OpisOsoby"] + "\n";
            komunikat += this["Telefon1"] == null ? "" : "Telefon (1): " + this["Telefon1"] + "\n";
            komunikat += this["Telefon2"] == null ? "" : "Telefon (2): " + this["Telefon2"] + "\n";
            komunikat += this["Email1"] == null ? "" : "Email (1): " + this["Email1"] + "\n";
            komunikat += this["Email2"] == null ? "" : "Email (2): " + this["Email2"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
