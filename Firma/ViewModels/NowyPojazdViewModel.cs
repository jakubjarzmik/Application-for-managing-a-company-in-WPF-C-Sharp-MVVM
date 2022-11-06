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
    public class NowyPojazdViewModel : JedenViewModel<Pojazdy>
    {
        #region Konstruktor
        public NowyPojazdViewModel():base("Nowy pojazd")
        {
            Item = new Pojazdy();
        }
        #endregion
        #region Properties
        public string RodzajPojazdu
        {
            get
            {
                return Item.RodzajPojazdu;
            }
            set
            {
                if (value != Item.RodzajPojazdu) 
                { 
                    Item.RodzajPojazdu = value;
                    base.OnPropertyChanged(() => RodzajPojazdu);
                }
            }
        }
        public string Marka
        {
            get
            {
                return Item.Marka;
            }
            set
            {
                if (value != Item.Marka)
                {
                    Item.Marka = value;
                    base.OnPropertyChanged(() => Marka);
                }
            }
        }
        public string Model
        {
            get
            {
                return Item.Model;
            }
            set
            {
                if (value != Item.Model)
                {
                    Item.Model = value;
                    base.OnPropertyChanged(() => Model);
                }
            }
        }
        public string Rocznik
        {
            get
            {
                return Item.Rocznik;
            }
            set
            {
                if (value != Item.Rocznik)
                {
                    Item.Rocznik = value;
                    base.OnPropertyChanged(() => Rocznik);
                }
            }
        }
        public string NrVIN
        {
            get
            {
                return Item.NrVIN;
            }
            set
            {
                if (value != Item.NrVIN)
                {
                    Item.NrVIN = value;
                    base.OnPropertyChanged(() => NrVIN);
                }
            }
        }
        public string KrajRejestracji
        {
            get
            {
                return Item.KrajRejestracji;
            }
            set
            {
                if (value != Item.KrajRejestracji)
                {
                    Item.KrajRejestracji = value;
                    base.OnPropertyChanged(() => KrajRejestracji);
                }
            }
        }
        public string NrRejestracyjny
        {
            get
            {
                return Item.NrRejestracyjny;
            }
            set
            {
                if (value != Item.NrRejestracyjny)
                {
                    Item.NrRejestracyjny = value;
                    base.OnPropertyChanged(() => NrRejestracyjny);
                }
            }
        }
        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.CzyAktywny = true;
            Db.Pojazdy.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
