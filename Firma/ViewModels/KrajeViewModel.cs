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
using System.Windows;

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
        public override void Add()
        {
            Messenger.Default.Send(new NowyKrajViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.Kraje.Where(a => a.KrajId == Selected.KrajId).FirstOrDefault();
                Messenger.Default.Send(new NowyKrajViewModel(toEdit));
                Messenger.Default.Register<Kraje>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Kraje edited)
        {
            edited.DataMod = DateTime.Now;
            edited.KtoModId = 1;
            JJFirmaEntities.SaveChanges();
            Load();
        }
        public override void Delete()
        {
            try
            {
                var toDelete = JJFirmaEntities.Kraje.Where(a => a.KrajId == Selected.KrajId).FirstOrDefault();
                if (toDelete != null)
                {
                    toDelete.CzyAktywny = false;
                    toDelete.DataUsuniecia = DateTime.Now;
                    toDelete.KtoUsunalId = 1;
                    JJFirmaEntities.SaveChanges();
                    Load();
                }
            }
            catch (Exception) 
            {
                MessageBox.Show("Wybierz rekord, który chcesz usunąć");
            }
        }
        #endregion

    }
}
