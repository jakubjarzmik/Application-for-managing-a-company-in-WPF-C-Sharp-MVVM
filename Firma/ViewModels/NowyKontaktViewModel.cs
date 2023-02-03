using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowyKontaktViewModel : JedenViewModel<Kontakty>
    {
        #region Konstruktor
        public NowyKontaktViewModel() : base("Nowy kontakt")
        {
            Item = new Kontakty();
        }
        public NowyKontaktViewModel(Kontakty kontakt) : base("Edytuj kontakt")
        {
            Item = kontakt;
            isEditing= true;
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
                if(value != Item.NazwaDzialu)
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
                if(value != Item.OpisOsoby)
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
    }
}
