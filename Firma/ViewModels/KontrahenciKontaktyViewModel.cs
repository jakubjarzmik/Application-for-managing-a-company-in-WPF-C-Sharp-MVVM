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
    public class KontrahenciKontaktyViewModel : WszystkieViewModel<KontrahenciKontaktyForAllView>
    {
        #region Konstruktor
        public KontrahenciKontaktyViewModel():base("Kontrahenci kontakty")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<KontrahenciKontaktyForAllView>
                (
                    from kontrahentKontakt in Db.KontrahenciKontakty
                    where kontrahentKontakt.CzyAktywny == true
                    select new KontrahenciKontaktyForAllView
                    {
                        KontrahentKontaktId = kontrahentKontakt.KontrahentKontaktId,
                        KontrahentId = kontrahentKontakt.KontrahentId,
                        KontrahentKod = kontrahentKontakt.Kontrahenci.Kod,
                        KontrahentNazwa = kontrahentKontakt.Kontrahenci.Nazwa1,
                        KontrahentNip = kontrahentKontakt.Kontrahenci.Nip,
                        KontaktId = kontrahentKontakt.KontaktId,
                        KontaktNazwaDzialu = kontrahentKontakt.Kontakty.NazwaDzialu,
                        KontaktOpisOsoby = kontrahentKontakt.Kontakty.OpisOsoby,
                        KontaktTelefon1 = kontrahentKontakt.Kontakty.Telefon1,
                        KontaktTelefon2 = kontrahentKontakt.Kontakty.Telefon2,
                        KontaktEmail1 = kontrahentKontakt.Kontakty.Email1,
                        KontaktEmail2 = kontrahentKontakt.Kontakty.Email2,
                    }
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyKontrahentKontaktViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.KontrahenciKontakty.Where(a => a.KontrahentKontaktId == Selected.KontrahentKontaktId).FirstOrDefault();
                Messenger.Default.Send(new NowyKontrahentKontaktViewModel(toEdit));
                Messenger.Default.Register<KontrahenciKontakty>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(KontrahenciKontakty edited)
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
                var toDelete = Db.KontrahenciKontakty.Where(a => a.KontrahentKontaktId == Selected.KontrahentKontaktId).FirstOrDefault();
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
