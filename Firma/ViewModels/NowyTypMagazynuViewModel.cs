using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowyTypMagazynuViewModel : JedenViewModel<MagazynyTypy>
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
    }
}
