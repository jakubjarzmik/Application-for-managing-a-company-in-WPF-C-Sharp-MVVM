﻿using Firma.Models.Entities;
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
    public class PracownicyViewModel : WszystkieViewModel<PracownikForAllView>
    {
        #region Konstruktor
        public PracownicyViewModel():base("Pracownicy")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PracownikForAllView>
                (
                    from pracownik in JJFirmaEntities.Pracownicy
                    where pracownik.CzyAktywny == true
                    select new PracownikForAllView
                    {
                        PracownikId= pracownik.PracownikId,
                        Akronim = pracownik.Akronim,
                        Nazwisko = pracownik.Nazwisko,
                        Imie = pracownik.Imie,
                        PESEL= pracownik.PESEL,
                        Adres = pracownik.Adresy3.Ulica + " " + pracownik.Adresy3.NrDomu +
                        (pracownik.Adresy3.NrLokalu.Equals("") ? "":"/"+ pracownik.Adresy3.NrLokalu)+
                        "\n" + pracownik.Adresy3.KodPocztowy + " " + pracownik.Adresy3.Miejscowosc,
                        Telefon = pracownik.Telefon,
                        Email = pracownik.Email
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyPracownikViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.Pracownicy.Where(a => a.PracownikId == Selected.PracownikId).FirstOrDefault();
                Messenger.Default.Send(new NowyPracownikViewModel(toEdit));
                Messenger.Default.Register<Pracownicy>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(Pracownicy edited)
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
                var toDelete = JJFirmaEntities.Pracownicy.Where(a => a.PracownikId == Selected.PracownikId).FirstOrDefault();
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
