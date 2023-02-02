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
        public override Kraje Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                if (value != _Selected)
                {
                    _Selected = value;
                    Messenger.Default.Send(_Selected, token);
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
        public override void Delete()
        {
            try
            {
                var toDelete = JJFirmaEntities.Kraje.Where(a => a.KrajId == Selected.KrajId).FirstOrDefault();
                if (toDelete != null)
                {
                    toDelete.CzyAktywny = false;
                    JJFirmaEntities.SaveChanges();
                    Load();
                }
            }
            catch (Exception) { }
        }
        #endregion

    }
}
