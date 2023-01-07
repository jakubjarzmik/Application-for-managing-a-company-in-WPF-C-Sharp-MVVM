using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowaGrupaTowarowViewModel : JedenViewModel<TowaryGrupy>
    {
        #region Konstruktor
        public NowaGrupaTowarowViewModel() 
            : base("Nowy rodzaj ceny")
        {
            Item = new TowaryGrupy();
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
                if(value != Item.Nazwa)
                {
                    Item.Nazwa = value;
                    base.OnPropertyChanged(() => Nazwa);
                }
            }
        }
        public string Kod
        {
            get
            {
                return Item.Kod;
            }
            set
            {
                if(value != Item.Kod)
                {
                    Item.Kod = value;
                    base.OnPropertyChanged(() => Kod);
                }
            }
        }
        public int? GrupaNadrzednaId
        {
            get
            {
                return Item.GrupaNadrzednaId;
            }
            set
            {
                if(value != Item.GrupaNadrzednaId)
                {
                    Item.GrupaNadrzednaId = value;
                    base.OnPropertyChanged(() => GrupaNadrzednaId);
                }
            }
        }
        public IQueryable<KeyAndValue> GrupyComboBoxItems
        {
            get
            {
                return
                (
                    from grupa in Db.TowaryGrupy
                    select new KeyAndValue
                    {
                        Key = grupa.GrupaTowaruId,
                        Value = grupa.Kod
                    }
                ).ToList().AsQueryable();
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
            Db.TowaryGrupy.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
    }
}
