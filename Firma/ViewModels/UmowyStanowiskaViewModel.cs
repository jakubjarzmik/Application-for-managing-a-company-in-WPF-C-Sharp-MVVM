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
        public override UmowyStanowiska Selected
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
            List = new ObservableCollection<UmowyStanowiska>
                (
                    from stanowisko in JJFirmaEntities.UmowyStanowiska
                    where stanowisko.CzyAktywny == true
                    select stanowisko
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NoweStanowiskoViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.UmowyStanowiska.Where(a => a.StanowiskoId == Selected.StanowiskoId).FirstOrDefault();
                Messenger.Default.Send(new NoweStanowiskoViewModel(toEdit));
                Messenger.Default.Register<UmowyStanowiska>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(UmowyStanowiska edited)
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
                var toDelete = JJFirmaEntities.UmowyStanowiska.Where(a => a.StanowiskoId == Selected.StanowiskoId).FirstOrDefault();
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
