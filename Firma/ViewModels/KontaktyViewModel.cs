﻿using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    class KontaktyViewModel : WszystkieViewModel<Kontakty>
    {
        #region Konstruktor
        public KontaktyViewModel() 
            : base("Kontakty")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<Kontakty>
                (
                    from kontakt in JJFirmaEntities.Kontakty
                    where kontakt.CzyAktywny == true
                    select kontakt
                );
        }
        public override void Delete()
        {
            try
            {
                var toDelete = JJFirmaEntities.Kontakty.Where(a => a.KontaktId == Selected.KontaktId).FirstOrDefault();
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
