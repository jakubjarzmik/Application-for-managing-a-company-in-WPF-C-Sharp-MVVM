using Firma.Models.Entities;
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
    class UmowyStanowiskaViewModel : WszystkieViewModel<UmowyStanowiska>
    {
        #region Konstruktor
        public UmowyStanowiskaViewModel() : base("Stanowiska")
        {
        }
        public UmowyStanowiskaViewModel(string token) : base("Stanowiska", token)
        {
        }
        #endregion
        #region Properties
        private UmowyStanowiska _SelectedStanowisko;
        public UmowyStanowiska SelectedStanowisko
        {
            get
            {
                return _SelectedStanowisko;
            }
            set
            {
                if (value != _SelectedStanowisko)
                {
                    _SelectedStanowisko = value;
                    Messenger.Default.Send(_SelectedStanowisko, token);
                    if (toClose)
                        OnRequestClose();
                }
            }
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<UmowyStanowiska>
                (
                    from stanowisko in JJFirmaEntities.UmowyStanowiska
                    where stanowisko.CzyAktywny == true
                    select stanowisko
                );
        }
        #endregion

    }
}
