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
    public class WydaniaZewnetrzneViewModel : WszystkieViewModel<WydanieZewnetrzneForAllView>
    {
        #region Konstruktor
        public WydaniaZewnetrzneViewModel():base("Wydania Zewnetrzne")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<WydanieZewnetrzneForAllView>
                (
                    from wz in JJFirmaEntities.WydaniaZewnetrzne
                    where wz.CzyAktywny == true
                    select new WydanieZewnetrzneForAllView
                    {
                        WydanieZewnetrzneId= wz.WydanieZewnetrzneId,
                        Numer = wz.Numer,
                        DataWydania = wz.DataWydania,
                        NazwaMagazynu = wz.Magazyny.Nazwa,
                        NazwaKontrahenta = wz.Kontrahenci.Nazwa1,
                        NipKontrahenta = wz.Kontrahenci.Nip
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NoweWydanieZewnetrzneViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.WydaniaZewnetrzne.Where(a => a.WydanieZewnetrzneId == Selected.WydanieZewnetrzneId).FirstOrDefault();
                Messenger.Default.Send(new NoweWydanieZewnetrzneViewModel(toEdit));
                Messenger.Default.Register<WydaniaZewnetrzne>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(WydaniaZewnetrzne edited)
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
                var toDelete = JJFirmaEntities.WydaniaZewnetrzne.Where(a => a.WydanieZewnetrzneId == Selected.WydanieZewnetrzneId).FirstOrDefault();
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
