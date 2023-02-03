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
    public class MagazynyViewModel : WszystkieViewModel<MagazynForAllView>
    {
        #region Konstruktor
        public MagazynyViewModel():base("Magazyny")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<MagazynForAllView>
                (
                    from magazyn in JJFirmaEntities.Magazyny
                    where magazyn.CzyAktywny == true
                    select new MagazynForAllView
                    {
                        Symbol = magazyn.Symbol,
                        Nazwa = magazyn.Nazwa,
                        Opis = magazyn.Opis,
                        Typ = magazyn.MagazynyTypy.Nazwa,
                        Adres = magazyn.Adresy.Ulica + " " + magazyn.Adresy.NrDomu +
                        (magazyn.Adresy.NrLokalu.Equals("") ? "":"/"+ magazyn.Adresy.NrLokalu)+
                        "\n" + magazyn.Adresy.KodPocztowy + " " + magazyn.Adresy.Miejscowosc,
                        Telefon = magazyn.Telefon
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyMagazynViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.Magazyny.Where(a => a.MagazynId == Selected.MagazynId).FirstOrDefault();
                Messenger.Default.Send(new NowyMagazynViewModel(toEdit));
                Messenger.Default.Register<Magazyny>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Magazyny edited)
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
                var toDelete = JJFirmaEntities.Magazyny.Where(a => a.MagazynId == Selected.MagazynId).FirstOrDefault();
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
