using Firma.Models.Entities;
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
    class RodzajePlatnosciViewModel : WszystkieViewModel<RodzajePlatnosci>
    {
        #region Konstruktor
        public RodzajePlatnosciViewModel() 
            : base("Rodzaje płatności")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<RodzajePlatnosci>
                (
                    from rodzaj in JJFirmaEntities.RodzajePlatnosci
                    where rodzaj.CzyAktywny == true
                    select rodzaj
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyRodzajPlatnosciViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.RodzajePlatnosci.Where(a => a.RodzajPlatnosciId == Selected.RodzajPlatnosciId).FirstOrDefault();
                Messenger.Default.Send(new NowyRodzajPlatnosciViewModel(toEdit));
                Messenger.Default.Register<RodzajePlatnosci>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(RodzajePlatnosci edited)
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
                var toDelete = JJFirmaEntities.RodzajePlatnosci.Where(a => a.RodzajPlatnosciId == Selected.RodzajPlatnosciId).FirstOrDefault();
                if (toDelete != null)
                {
                    toDelete.CzyAktywny = false;
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
