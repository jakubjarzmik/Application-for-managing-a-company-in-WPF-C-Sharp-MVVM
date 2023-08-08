using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.Models.Validators;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Linq;

namespace Firma.ViewModels
{
    public class NowaGrupaTowarowViewModel : JedenViewModel<TowaryGrupy>, IDataErrorInfo
    {
        #region Konstruktor
        public NowaGrupaTowarowViewModel() : base("Nowa grupa towarów")
        {
            Item = new TowaryGrupy();
            GrupaNadrzednaId= 1;
            Messenger.Default.Register<GrupaTowaruForAllView>(this, DisplayName, getSelectedGrupa);
        }
        public NowaGrupaTowarowViewModel(TowaryGrupy grupaTowaru) : base("Edytuj grupę towarów")
        {
            Item = grupaTowaru;
            isEditing = true;
            setAllFields();
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
                if (value != Item.Nazwa)
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
                if (value != Item.Kod)
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
                if (value != Item.GrupaNadrzednaId)
                {
                    Item.GrupaNadrzednaId = value;
                    setGrupaNadrzednaFields();
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
                    where grupa.CzyAktywny == true
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
        private void setAllFields()
        {
            setGrupaNadrzednaFields();
        }
        private void setGrupaNadrzednaFields()
        {
            GrupaNadrzednaNazwa = Db.TowaryGrupy.Where(n => n.GrupaTowaruId == GrupaNadrzednaId).Select(n => n.Nazwa).FirstOrDefault();
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
                    case "Nazwa":
                        komunikat = StringValidator.CheckIsStartsWithUpper(Nazwa);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Nazwa);
                        break;
                    case "Kod":
                        komunikat = StringValidator.CheckIsAllUpper(Kod);
                        if (komunikat != null)
                            break;
                        komunikat = BusinessValidator.CheckIsNotNull(Kod);
                        break;
                }
                return komunikat;
            }
        }
        public override string IsValid()
        {
            string komunikat = null;
            komunikat += this["Nazwa"] == null ? "" : "Nazwa: " + this["Nazwa"] + "\n";
            komunikat += this["Kod"] == null ? "" : "Kod: " + this["Kod"] + "\n";

            return komunikat;
        }
        #endregion
    }
}
