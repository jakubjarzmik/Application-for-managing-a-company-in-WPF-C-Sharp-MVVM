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
    class KontaktyViewModel : WszystkieViewModel<Kontakty>
    {
        #region Konstruktor
        public KontaktyViewModel() : base("Kontakty")
        {
        }
        public KontaktyViewModel(string token) : base("Kontakty", token)
        {
        }
        #endregion
        #region Properties
        public override Kontakty Selected
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
            List = new ObservableCollection<Kontakty>
                (
                    from kontakt in Db.Kontakty
                    where kontakt.CzyAktywny == true
                    select kontakt
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyKontaktViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.Kontakty.Where(a => a.KontaktId == Selected.KontaktId).FirstOrDefault();
                Messenger.Default.Send(new NowyKontaktViewModel(toEdit));
                Messenger.Default.Register<Kontakty>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Kontakty edited)
        {
            edited.DataMod = DateTime.Now;
            edited.KtoModId = 1;
            Db.SaveChanges();
            Load();
        }
        public override void Delete()
        {
            try
            {
                var toDelete = Db.Kontakty.Where(a => a.KontaktId == Selected.KontaktId).FirstOrDefault();
                if (toDelete != null)
                {
                    toDelete.CzyAktywny = false;
                    toDelete.DataUsuniecia = DateTime.Now;
                    toDelete.KtoUsunalId = 1;
                    Db.SaveChanges();
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
