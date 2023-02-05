using Firma.Helpers;
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
    class TowaryStawkiVatViewModel : WszystkieViewModel<StawkaVatTowaruForAllView>
    {
        #region Konstruktor
        public TowaryStawkiVatViewModel() : base("Stawki VAT")
        {
        }
        public TowaryStawkiVatViewModel(string token) : base("Stawki VAT", token)
        {
            isVatZak = false;
        }
        public TowaryStawkiVatViewModel(string token, bool isVatZak) : base("Stawki VAT", token)
        {
            this.isVatZak = isVatZak;
        }
        #endregion
        #region Properties
        private bool isVatZak;
        public override StawkaVatTowaruForAllView Selected
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
                    Messenger.Default.Send(new StawkaVatAndIsZak { StawkaVatTowaruForAllView = _Selected, isVatZak = this.isVatZak }, token);
                    if (toClose)
                        OnRequestClose();
                }
            }
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<StawkaVatTowaruForAllView>
                (
                    from stawka in Db.TowaryStawkiVat
                    where stawka.CzyAktywny == true
                    select new StawkaVatTowaruForAllView
                    {
                        StawkaVatId= stawka.StawkiVatId,
                        Kraj = stawka.Kraje.Nazwa,
                        Stawka = stawka.Stawka,
                        Opis = stawka.Opis
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowaStawkaVatTowarowViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.TowaryStawkiVat.Where(a => a.StawkiVatId == Selected.StawkaVatId).FirstOrDefault();
                Messenger.Default.Send(new NowaStawkaVatTowarowViewModel(toEdit));
                Messenger.Default.Register<TowaryStawkiVat>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(TowaryStawkiVat edited)
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
                var toDelete = Db.TowaryStawkiVat.Where(a => a.StawkiVatId == Selected.StawkaVatId).FirstOrDefault();
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
