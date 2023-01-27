using Firma.Helpers;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowaGrupaTowarowViewModel : JedenViewModel<TowaryGrupy>
    {
        #region Commands
        private BaseCommand _ShowGrupyTowaruCommand;
        public BaseCommand ShowGrupyTowaruCommand
        {
            get
            {
                if (_ShowGrupyTowaruCommand == null)
                {
                    _ShowGrupyTowaruCommand = new BaseCommand(() => showGrupyTowaru());
                }
                return _ShowGrupyTowaruCommand;
            }
        }
        private void showGrupyTowaru()
        {
            Messenger.Default.Send("GrupyTowaruAll;" + DisplayName);
        }
        #endregion
        #region Konstruktor
        public NowaGrupaTowarowViewModel() 
            : base("Nowa grupa towarów")
        {
            Item = new TowaryGrupy();
            Messenger.Default.Register<GrupaTowaruForAllView>(this, DisplayName, getSelectedGrupa);
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
                    GrupaNadrzednaNazwa = Db.TowaryGrupy.Where(n => n.GrupaTowaruId == GrupaNadrzednaId).Select(n => n.Nazwa).FirstOrDefault();
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
        private String _GrupaNadrzednaNazwa;
        public string GrupaNadrzednaNazwa
        {
            get
            {
                return _GrupaNadrzednaNazwa;
            }
            set
            {
                if (value != _GrupaNadrzednaNazwa)
                {
                    _GrupaNadrzednaNazwa = value;
                    base.OnPropertyChanged(() => GrupaNadrzednaNazwa);
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
            Db.TowaryGrupy.AddObject(Item);
            Db.SaveChanges();
        }
        #endregion
        #region Helpers
        private void getSelectedGrupa(GrupaTowaruForAllView grupaTowaruForAllView)
        {
            GrupaNadrzednaId = grupaTowaruForAllView.GrupaTowaruId;
        }
        #endregion
    }
}
