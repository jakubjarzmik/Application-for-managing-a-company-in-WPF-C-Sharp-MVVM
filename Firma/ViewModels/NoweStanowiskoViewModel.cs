using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NoweStanowiskoViewModel : JedenViewModel<UmowyStanowiska>
    {
        #region Konstruktor
        public NoweStanowiskoViewModel() 
            : base("Nowe stanowisko")
        {
            Item = new UmowyStanowiska();
        }
        #endregion
        #region Properties
        public string KodZawodu
        {
            get
            {
                return Item.KodZawodu;
            }
            set
            {
                if (value != Item.KodZawodu)
                {
                    Item.KodZawodu = value;
                    base.OnPropertyChanged(() => KodZawodu);
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
            Db.UmowyStanowiska.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
