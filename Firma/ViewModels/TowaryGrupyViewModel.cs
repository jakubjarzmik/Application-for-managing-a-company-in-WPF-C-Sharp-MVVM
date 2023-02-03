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
    class TowaryGrupyViewModel : WszystkieViewModel<GrupaTowaruForAllView>
    {
        #region Konstruktor
        public TowaryGrupyViewModel() : base("Grupy towarów")
        {
        }
        public TowaryGrupyViewModel(string token) : base("Grupy towarów", token)
        {
        }
        #endregion
        #region Properties
        public override GrupaTowaruForAllView Selected
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
            List = new ObservableCollection<GrupaTowaruForAllView>
                (
                    from grupa in JJFirmaEntities.TowaryGrupy
                    where grupa.CzyAktywny == true
                    select new GrupaTowaruForAllView
                    {
                        GrupaTowaruId = grupa.GrupaTowaruId,
                        GrupaNadrzedna = grupa.TowaryGrupy2.Kod,
                        Kod = grupa.Kod,
                        Nazwa = grupa.Nazwa,
                    }
                );
        }

        public override void Add()
        {
            Messenger.Default.Send(new NowaGrupaTowarowViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.TowaryGrupy.Where(a => a.GrupaTowaruId == Selected.GrupaTowaruId).FirstOrDefault();
                Messenger.Default.Send(new NowaGrupaTowarowViewModel(toEdit));
                Messenger.Default.Register<TowaryGrupy>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(TowaryGrupy edited)
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
                var toDelete = JJFirmaEntities.TowaryGrupy.Where(a => a.GrupaTowaruId == Selected.GrupaTowaruId).FirstOrDefault();
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
