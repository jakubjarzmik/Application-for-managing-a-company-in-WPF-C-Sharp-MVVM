using Firma.Helpers;
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
    class TowaryStawkiVatViewModel : WszystkieViewModel<StawkaVatTowaruForAllView>
    {
        #region Konstruktor
        public TowaryStawkiVatViewModel() : base("Stawki VAT")
        {
        }
        public TowaryStawkiVatViewModel(string token) : base("Stawki VAT", token)
        {
            isVatZak = false;
        }
        public TowaryStawkiVatViewModel(string token, bool isVatZak) : base("Stawki VAT", token)
        {
            this.isVatZak = isVatZak;
        }
        #endregion
        #region Properties
        private bool isVatZak;
        private StawkaVatTowaruForAllView _SelectedStawkaVat;
        public StawkaVatTowaruForAllView SelectedStawkaVat
        {
            get
            {
                return _SelectedStawkaVat;
            }
            set
            {
                if (value != _SelectedStawkaVat)
                {
                    _SelectedStawkaVat = value;
                    Messenger.Default.Send(new StawkaVatAndIsZak { StawkaVatTowaruForAllView = _SelectedStawkaVat, isVatZak = this.isVatZak }, token);
                    if (toClose)
                        OnRequestClose();
                }
            }
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<StawkaVatTowaruForAllView>
                (
                    from stawka in JJFirmaEntities.TowaryStawkiVat
                    where stawka.CzyAktywny == true
                    select new StawkaVatTowaruForAllView
                    {
                        StawkaVatId= stawka.StawkiVatId,
                        Kraj = stawka.Kraje.Nazwa,
                        Stawka = stawka.Stawka,
                        Opis = stawka.Opis
                    }
                );
        }
        #endregion

    }
}
