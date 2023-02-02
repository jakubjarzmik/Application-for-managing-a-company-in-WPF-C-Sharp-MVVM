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

namespace Firma.ViewModels
{
    public class AdresyViewModel : WszystkieViewModel<AdresForAllView>
    {
        #region Konstruktor
        public AdresyViewModel():base("Adresy")
        {
        }
        public AdresyViewModel(string token) : base("Adresy", token)
        {
            isAdresKor = false;
        }
        public AdresyViewModel(string token, bool isAdresKor) : base("Adresy", token)
        {
            this.isAdresKor = isAdresKor;
        }
        #endregion
        #region Properties
        private bool isAdresKor;
        public override AdresForAllView Selected
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
                    Messenger.Default.Send(new AdresAndIsKor { AdresForAllView = _Selected, isAdresKor = this.isAdresKor}, token);
                    if (toClose)
                        OnRequestClose();
                }
            }
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<AdresForAllView>
                (
                    from adres in JJFirmaEntities.Adresy
                    where adres.CzyAktywny == true
                    select new AdresForAllView
                    {
                        AdresId = adres.AdresId,
                        Kraj = adres.Kraje.Nazwa,
                        Wojewodztwo = adres.Wojewodztwo,
                        KodPocztowy = adres.KodPocztowy,
                        Miejscowosc= adres.Miejscowosc,
                        Ulica=adres.Ulica,
                        NrDomu= adres.NrDomu,
                        NrLokalu=adres.NrLokalu
                    }
                );
        }
        public override void Delete()
        {
            try
            {
                var toDelete = JJFirmaEntities.Adresy.Where(a => a.AdresId == Selected.AdresId).FirstOrDefault();
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
