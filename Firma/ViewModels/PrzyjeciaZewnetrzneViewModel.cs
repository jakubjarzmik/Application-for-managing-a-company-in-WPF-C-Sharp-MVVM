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
    public class PrzyjeciaZewnetrzneViewModel : WszystkieViewModel<PrzyjecieZewnetrzneForAllView>
    {
        #region Konstruktor
        public PrzyjeciaZewnetrzneViewModel():base("Przyjecia Zewnetrzne")
        {
        }
        public PrzyjeciaZewnetrzneViewModel(string token):base("Przyjecia Zewnetrzne", token)
        {
        }
        #endregion
        #region Properties
        public override PrzyjecieZewnetrzneForAllView Selected
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
            List = new ObservableCollection<PrzyjecieZewnetrzneForAllView>
                (
                    from pz in JJFirmaEntities.PrzyjeciaZewnetrzne
                    where pz.CzyAktywny == true
                    select new PrzyjecieZewnetrzneForAllView
                    {
                        PrzyjecieZewnetrzneId= pz.PrzyjecieZewnetrzneId,
                        Numer = pz.Numer,
                        DataPrzyjecia = pz.DataPrzyjecia,
                        NazwaMagazynu = pz.Magazyny.Nazwa,
                        NazwaKontrahenta = pz.Kontrahenci.Nazwa1,
                        NipKontrahenta = pz.Kontrahenci.Nip
                    }
                );
        }

        public override void Add()
        {
            Messenger.Default.Send(new NowePrzyjecieZewnetrzneViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.PrzyjeciaZewnetrzne.Where(a => a.PrzyjecieZewnetrzneId == Selected.PrzyjecieZewnetrzneId).FirstOrDefault();
                Messenger.Default.Send(new NowePrzyjecieZewnetrzneViewModel(toEdit));
                Messenger.Default.Register<PrzyjeciaZewnetrzne>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(PrzyjeciaZewnetrzne edited)
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
                var toDelete = JJFirmaEntities.PrzyjeciaZewnetrzne.Where(a => a.PrzyjecieZewnetrzneId == Selected.PrzyjecieZewnetrzneId).FirstOrDefault();
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
