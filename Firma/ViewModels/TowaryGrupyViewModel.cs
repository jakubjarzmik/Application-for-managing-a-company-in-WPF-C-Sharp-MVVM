using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    class TowaryGrupyViewModel : WszystkieViewModel<GrupaTowaruForAllView>
    {
        #region Konstruktor
        public TowaryGrupyViewModel() : base("Grupy towarów")
        {
        }
        public TowaryGrupyViewModel(string token) : base("Grupy towarów", token)
        {
        }
        #endregion
        #region Properties
        private GrupaTowaruForAllView _SelectedGrupa;
        public GrupaTowaruForAllView SelectedGrupa
        {
            get
            {
                return _SelectedGrupa;
            }
            set
            {
                if (value != _SelectedGrupa)
                {
                    _SelectedGrupa = value;
                    Messenger.Default.Send(_SelectedGrupa, token);
                    if (toClose)
                        OnRequestClose();
                }
            }
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<GrupaTowaruForAllView>
                (
                    from grupa in JJFirmaEntities.TowaryGrupy
                    where grupa.CzyAktywny == true
                    select new GrupaTowaruForAllView
                    {
                        GrupaTowaruId = grupa.GrupaTowaruId,
                        GrupaNadrzedna = grupa.TowaryGrupy2.Kod,
                        Kod = grupa.Kod,
                        Nazwa = grupa.Nazwa,
                    }
                );
        }
        #endregion

    }
}
