using Firma.Models.BusinessLogic;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using Firma.Views;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Metadata.Edm;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Firma.ViewModels
{
    public class TowaryViewModel : WszystkieViewModel<TowarForAllView>
    {
        #region Konstruktor
        public TowaryViewModel() : base("Towary")
        {
        }
        public TowaryViewModel(string token) : base("Towary", token)
        {
        }
        #endregion
        #region Properties
        public override TowarForAllView Selected
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
            List = new ObservableCollection<TowarForAllView>
                (
                    from towar in JJFirmaEntities.Towary
                    where towar.CzyAktywny == true
                    select new TowarForAllView
                    {
                        TowarId = towar.TowarId,
                        Kod = towar.Kod,
                        Nazwa = towar.Nazwa,
                        Typ = towar.TowaryTypy.Nazwa,
                        Grupa = towar.TowaryGrupy.Nazwa,
                        NumerKatalogowy = towar.NumerKatalogowy,
                        EAN = towar.EAN,
                        Producent = towar.Kontrahenci.Nazwa1,
                        KrajPochodzenia = towar.Kraje.ISO,
                        Ilosc = (
                                from pozycjaPZ in JJFirmaEntities.PozycjePrzyjeciaZewnetrznego
                                where pozycjaPZ.CzyAktywny == true && pozycjaPZ.Towary.TowarId == towar.TowarId
                                select pozycjaPZ.Ilosc
                            ).Sum() -
                            (
                                from pozycjaWZ in JJFirmaEntities.PozycjeWydaniaZewnetrznego
                                where pozycjaWZ.CzyAktywny == true && pozycjaWZ.Towary.TowarId == towar.TowarId
                                select pozycjaWZ.Ilosc
                            ).Sum(),
                        JednMiary = towar.JednostkiMiary.Skrot
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyTowarViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.Towary.Where(a => a.TowarId == Selected.TowarId).FirstOrDefault();
                Messenger.Default.Send(new NowyTowarViewModel(toEdit));
                Messenger.Default.Register<Towary>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Towary edited)
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
                var toDelete = JJFirmaEntities.Towary.Where(a => a.TowarId == Selected.TowarId).FirstOrDefault();
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
