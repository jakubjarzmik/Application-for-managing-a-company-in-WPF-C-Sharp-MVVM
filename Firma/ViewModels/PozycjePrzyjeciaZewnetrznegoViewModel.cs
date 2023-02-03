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
    public class PozycjePrzyjeciaZewnetrznegoViewModel : WszystkieViewModel<PozycjaPrzyjeciaZewnetrznegoForAllView>
    {
        #region Konstruktor
        public PozycjePrzyjeciaZewnetrznegoViewModel():base("Pozycje PZ")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<PozycjaPrzyjeciaZewnetrznegoForAllView>
                (
                    from pozycja in JJFirmaEntities.PozycjePrzyjeciaZewnetrznego
                    where pozycja.CzyAktywny == true
                    select new PozycjaPrzyjeciaZewnetrznegoForAllView
                    {
                        PozycjaPZId = pozycja.PozycjaPZId,
                        NumerPrzyjeciaZewnetrznego = pozycja.PrzyjeciaZewnetrzne.Numer,
                        NazwaKontrahenta = pozycja.PrzyjeciaZewnetrzne.Kontrahenci.Nazwa1,
                        NazwaTowaru = pozycja.Towary.Nazwa,
                        Ilosc = pozycja.Ilosc,
                        JednostkaMiary = pozycja.JednostkiMiary.Skrot,
                        PierwotnaCenaZakupu = pozycja.PierwotnaCenaZakupu,
                        Rabat = pozycja.Rabat,
                        CenaPoRabacieZaSzt = pozycja.PierwotnaCenaZakupu * (100 - pozycja.Rabat) / 100,
                        Wartosc = (pozycja.PierwotnaCenaZakupu * pozycja.Ilosc * (100 - pozycja.Rabat) / 100 ),
                    }
                );
        }
        public override void Add()
        {
            //Messenger.Default.Send(new NowaPozycjaPrzyjeciaZewnetrznegoViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.PozycjePrzyjeciaZewnetrznego.Where(a => a.PozycjaPZId == Selected.PozycjaPZId).FirstOrDefault();
                //Messenger.Default.Send(new NowaPozycjaPrzyjeciaZewnetrznegoViewModel(toEdit));
                Messenger.Default.Register<PozycjePrzyjeciaZewnetrznego>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(PozycjePrzyjeciaZewnetrznego edited)
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
                var toDelete = JJFirmaEntities.PozycjePrzyjeciaZewnetrznego.Where(a => a.PozycjaPZId == Selected.PozycjaPZId).FirstOrDefault();
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
