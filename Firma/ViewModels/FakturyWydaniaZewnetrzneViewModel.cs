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
    public class FakturyWydaniaZewnetrzneViewModel : WszystkieViewModel<FakturyWydaniaZewnetrzneForAllView>
    {
        #region Konstruktor
        public FakturyWydaniaZewnetrzneViewModel():base("Faktury WZ")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<FakturyWydaniaZewnetrzneForAllView>
                (
                    from fakturawz in JJFirmaEntities.FakturyWydaniaZewnetrzne
                    where fakturawz.CzyAktywny == true
                    select new FakturyWydaniaZewnetrzneForAllView
                    {
                        FakturaWZId = fakturawz.FakturaWZId,
                        FakturaId = fakturawz.FakturaId,
                        NumerFaktury = fakturawz.Faktury.Numer,
                        NazwaKontrahenta = fakturawz.Faktury.Kontrahenci.Nazwa1,
                        WydanieZewnetrzneId = fakturawz.WydanieZewnetrzneId,
                        NumerWZ = fakturawz.WydaniaZewnetrzne.Numer,
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowaFakturaWydanieZewnetrzneViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.FakturyWydaniaZewnetrzne.Where(a => a.FakturaWZId == Selected.FakturaWZId).FirstOrDefault();
                Messenger.Default.Send(new NowaFakturaWydanieZewnetrzneViewModel(toEdit));
                Messenger.Default.Register<FakturyWydaniaZewnetrzne>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(FakturyWydaniaZewnetrzne edited)
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
                var toDelete = JJFirmaEntities.FakturyWydaniaZewnetrzne.Where(a => a.FakturaWZId == Selected.FakturaWZId).FirstOrDefault();
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
