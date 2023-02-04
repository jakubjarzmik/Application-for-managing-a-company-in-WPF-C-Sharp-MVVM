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
    class MagazynyTypyViewModel : WszystkieViewModel<MagazynyTypy>
    {
        #region Konstruktor
        public MagazynyTypyViewModel() 
            : base("Typy magazynów")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<MagazynyTypy>
                (
                    from typ in JJFirmaEntities.MagazynyTypy
                    where typ.CzyAktywny == true
                    select typ
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyTypMagazynuViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.MagazynyTypy.Where(a => a.TypMagazynuId == Selected.TypMagazynuId).FirstOrDefault();
                Messenger.Default.Send(new NowyTypMagazynuViewModel(toEdit));
                Messenger.Default.Register<MagazynyTypy>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(MagazynyTypy edited)
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
                var toDelete = JJFirmaEntities.MagazynyTypy.Where(a => a.TypMagazynuId == Selected.TypMagazynuId).FirstOrDefault();
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
