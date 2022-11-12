using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowyTypTowarowViewModel : JedenViewModel<TowaryTypy>
    {
        #region Konstruktor
        public NowyTypTowarowViewModel() 
            : base("Nowy typ towarów")
        {
            Item = new TowaryTypy();
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

        #endregion
        #region Save
        public override void Save()
        {
            Item.DataUtworzenia = DateTime.Now;
            Item.CzyAktywny = true;
            Db.TowaryTypy.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
