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
    public class ZmianyCenyViewModel : WszystkieViewModel<ZmianaCenyForAllView>
    {
        #region Konstruktor
        public ZmianyCenyViewModel():base("Zmiany ceny")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<ZmianaCenyForAllView>
                (
                    from cena in JJFirmaEntities.ZmianyCeny
                    where cena.CzyAktywny == true
                    select new ZmianaCenyForAllView
                    {
                        ZmianaCenyId= cena.ZmianaCenyId,
                        Towar = cena.Towary.Nazwa,
                        JednostkaMiary = cena.JednostkiMiary.Skrot,
                        CenaNetto = cena.CenaNetto,
                        CenaBrutto = cena.CenaNetto * (100 + cena.Towary.TowaryStawkiVat.Stawka) / 100,
                        WartoscVat = cena.CenaNetto * cena.Towary.TowaryStawkiVat.Stawka / 100,
                        DataObowiazywaniaOd = cena.DataObowiazywaniaOd,
                        DataObowiazywaniaDo = cena.DataObowiazywaniaDo
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowaZmianaCenyViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = JJFirmaEntities.ZmianyCeny.Where(a => a.ZmianaCenyId == Selected.ZmianaCenyId).FirstOrDefault();
                Messenger.Default.Send(new NowaZmianaCenyViewModel(toEdit));
                Messenger.Default.Register<ZmianyCeny>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(ZmianyCeny edited)
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
                var toDelete = JJFirmaEntities.ZmianyCeny.Where(a => a.ZmianaCenyId == Selected.ZmianaCenyId).FirstOrDefault();
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
