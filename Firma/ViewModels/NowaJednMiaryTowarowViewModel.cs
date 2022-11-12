using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowaJednMiaryTowarowViewModel : JedenViewModel<TowaryJednMiary>
    {
        #region Konstruktor
        public NowaJednMiaryTowarowViewModel() 
            : base("Nowa jedn. miary")
        {
            Item = new TowaryJednMiary();
        }
        #endregion
        #region Properties
        public string Skrot
        {
            get
            {
                return Item.Skrot;
            }
            set
            {
                if (value != Item.Skrot)
                {
                    Item.Skrot = value;
                    base.OnPropertyChanged(() => Skrot);
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
        
        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.CzyAktywny = true;
            Db.TowaryJednMiary.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
