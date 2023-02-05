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
    public class UmowyViewModel : WszystkieViewModel<UmowaForAllView>
    {
        #region Konstruktor
        public UmowyViewModel():base("Umowy")
        {
        }
        public UmowyViewModel(string token):base("Umowy", token)
        {
        }
        #endregion
        #region Properties
        public override UmowaForAllView Selected
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
            List = new ObservableCollection<UmowaForAllView>
                (
                    from umowa in Db.Umowy
                    where umowa.CzyAktywny == true
                    select new UmowaForAllView
                    {
                        UmowaId= umowa.UmowaId,
                        Numer = umowa.NrUmowy,
                        Rodzaj = umowa.UmowyRodzaje.Nazwa,
                        Stanowisko = umowa.UmowyStanowiska.Nazwa,
                        DataZawarcia = umowa.DataZawarcia,
                        DataOd = umowa.DataOd,
                        DataDo = umowa.DataDo,
                        StawkaMies = umowa.StawkaBruttoMies,
                        StawkaGodz = umowa.StawkaBruttoGodz,
                        CzasPracyMies = umowa.CzasPracyMies,
                        Opis = umowa.Opis,
                        JestEmerytalne = umowa.JestEmerytalne ? "Tak" : "Nie",
                        JestRentowe = umowa.JestRentowe ? "Tak" : "Nie",
                        JestChorobowe = umowa.JestChorobowe ? "Tak" : "Nie",
                        JestWypadkowe = umowa.JestWypadkowe ? "Tak" : "Nie"
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowaUmowaViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.Umowy.Where(a => a.UmowaId == Selected.UmowaId).FirstOrDefault();
                Messenger.Default.Send(new NowaUmowaViewModel(toEdit));
                Messenger.Default.Register<Umowy>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Umowy edited)
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
                var toDelete = Db.Umowy.Where(a => a.UmowaId == Selected.UmowaId).FirstOrDefault();
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
