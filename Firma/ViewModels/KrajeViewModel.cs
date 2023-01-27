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
    class KrajeViewModel : WszystkieViewModel<Kraje>
    {
        #region Konstruktor
        public KrajeViewModel() : base("Kraje")
        {
        }
        public KrajeViewModel(string token) : base("Kraje", token)
        {
        }
        #endregion
        #region Properties
        private Kraje _SelectedKraj;
        public Kraje SelectedKraj
        {
            get
            {
                return _SelectedKraj;
            }
            set
            {
                if (value != _SelectedKraj)
                {
                    _SelectedKraj = value;
                    Messenger.Default.Send(_SelectedKraj, token);
                    if (toClose)
                        OnRequestClose();
                }
            }
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Kraje>
                (
                    from kraj in JJFirmaEntities.Kraje
                    where kraj.CzyAktywny == true
                    select kraj
                );
        }
        #endregion

    }
}
