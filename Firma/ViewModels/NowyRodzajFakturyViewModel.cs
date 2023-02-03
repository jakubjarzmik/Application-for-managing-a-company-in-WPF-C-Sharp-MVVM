using Firma.Helpers;
using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class NowyRodzajFakturyViewModel : JedenViewModel<FakturyRodzaje>
    {
        #region Konstruktor
        public NowyRodzajFakturyViewModel():base("Nowy rodzaj faktury")
        {
            Item = new FakturyRodzaje();
        }
        public NowyRodzajFakturyViewModel(FakturyRodzaje rodzajFaktury):base("Edytuj rodzaj faktury")
        {
            Item = rodzajFaktury;
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
            Db.FakturyRodzaje.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
