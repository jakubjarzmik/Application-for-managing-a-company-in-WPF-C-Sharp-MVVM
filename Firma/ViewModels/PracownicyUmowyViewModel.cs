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
    public class PracownicyUmowyViewModel : WszystkieViewModel<PracownicyUmowyForAllView>
    {
        #region Konstruktor
        public PracownicyUmowyViewModel():base("Pracownicy umowy")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PracownicyUmowyForAllView>
                (
                    from pracownikUmowa in JJFirmaEntities.PracownicyUmowy
                    where pracownikUmowa.CzyAktywny == true
                    select new PracownicyUmowyForAllView
                    {
                        PracownikUmowaId = pracownikUmowa.PracownikUmowaId,
                        PracownikId = pracownikUmowa.PracownikId,
                        PracownikNazwa = pracownikUmowa.Pracownicy.Imie + " " + pracownikUmowa.Pracownicy.Nazwisko
                        + (pracownikUmowa.Pracownicy.NazwiskoRodowe.Equals("") ? "": " ("+pracownikUmowa.Pracownicy.NazwiskoRodowe+")"),
                        PracownikPESEL = pracownikUmowa.Pracownicy.PESEL,
                        UmowaId = pracownikUmowa.UmowaId,
                        UmowaNumer = pracownikUmowa.Umowy.NrUmowy,
                        UmowaRodzaj = pracownikUmowa.Umowy.UmowyRodzaje.Nazwa,
                        UmowaStanowisko = pracownikUmowa.Umowy.UmowyStanowiska.Nazwa,
                        UmowaDataOd = pracownikUmowa.Umowy.DataOd,
                        UmowaDataDo = pracownikUmowa.Umowy.DataDo,
                        UmowaStawkaMies = pracownikUmowa.Umowy.StawkaBruttoMies,
                        UmowaStawkaGodz = pracownikUmowa.Umowy.StawkaBruttoGodz,
                        UmowaCzasPracyMies = pracownikUmowa.Umowy.CzasPracyMies,
                        UmowaWartoscMies = pracownikUmowa.Umowy.StawkaBruttoMies + (pracownikUmowa.Umowy.StawkaBruttoGodz * pracownikUmowa.Umowy.CzasPracyMies)
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyPracownikUmowaViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.PracownicyUmowy.Where(a => a.PracownikUmowaId == Selected.PracownikUmowaId).FirstOrDefault();
                Messenger.Default.Send(new NowyPracownikUmowaViewModel(toEdit));
                Messenger.Default.Register<PracownicyUmowy>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(PracownicyUmowy edited)
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
                var toDelete = JJFirmaEntities.PracownicyUmowy.Where(a => a.PracownikUmowaId == Selected.PracownikUmowaId).FirstOrDefault();
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
