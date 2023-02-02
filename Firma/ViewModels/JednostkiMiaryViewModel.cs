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
    class JednostkiMiaryViewModel : WszystkieViewModel<JednostkiMiary>
    {
        #region Konstruktor
        public JednostkiMiaryViewModel() : base("Jednostki miary")
        {
        }
        public JednostkiMiaryViewModel(string token) : base("Jednostki miary", token)
        {
        }
        #endregion
        #region Properties
        public override JednostkiMiary Selected
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
            List = new ObservableCollection<JednostkiMiary>
                (
                    from jednmiary in JJFirmaEntities.JednostkiMiary
                    where jednmiary.CzyAktywny == true
                    select jednmiary
                );
        }
        public override void Delete()
        {
            try
            {
                var toDelete = JJFirmaEntities.JednostkiMiary.Where(a => a.JednostkaId == Selected.JednostkaId).FirstOrDefault();
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
