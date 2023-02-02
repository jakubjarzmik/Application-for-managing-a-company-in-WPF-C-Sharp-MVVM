using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class KontrahenciViewModel : WszystkieViewModel<KontrahentForAllView>
    {
        #region Konstruktor
        public KontrahenciViewModel():base("Kontrahenci")
        {
        }
        public KontrahenciViewModel(string token):base("Kontrahenci", token)
        {
        }
        #endregion
        #region Properties
        public override KontrahentForAllView Selected
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
            List = new ObservableCollection<KontrahentForAllView>
                (
                    from kontrahent in JJFirmaEntities.Kontrahenci
                    where kontrahent.CzyAktywny == true
                    select new KontrahentForAllView
                    {
                        KontrahentId = kontrahent.KontrahentId,
                        Kod = kontrahent.Kod,
                        Nazwa1 = kontrahent.Nazwa1,
                        RodzajKontrahenta = kontrahent.KontrahenciRodzaje.Nazwa,
                        Nip = kontrahent.Nip,
                        Regon = kontrahent.Regon,
                        Adres = kontrahent.Adresy.Ulica + " " + kontrahent.Adresy.NrDomu +
                        (kontrahent.Adresy.NrLokalu.Equals("") ? "":"/"+ kontrahent.Adresy.NrLokalu)+
                        "\n" + kontrahent.Adresy.KodPocztowy + " " + kontrahent.Adresy.Miejscowosc,
                        Url = kontrahent.URL,
                    }
                );
        }
        public override void Delete()
        {
            try
            {
                var toDelete = JJFirmaEntities.Kontrahenci.Where(a => a.KontrahentId == Selected.KontrahentId).FirstOrDefault();
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
