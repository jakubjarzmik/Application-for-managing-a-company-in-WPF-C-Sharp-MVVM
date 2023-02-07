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
    class RodzajePlatnosciViewModel : WszystkieViewModel<RodzajePlatnosci>
    {
        #region Konstruktor
        public RodzajePlatnosciViewModel() 
            : base("Rodzaje płatności")
        {
        }
        #endregion
        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<RodzajePlatnosci>
                (
                    from rodzaj in Db.RodzajePlatnosci
                    where rodzaj.CzyAktywny == true
                    select rodzaj
                );
        }
        public override void Add()
        {
            Messenger.Default.Send(new NowyRodzajPlatnosciViewModel());
        }
        public override void Edit()
        {
            try
            {
                var toEdit = Db.RodzajePlatnosci.Where(a => a.RodzajPlatnosciId == Selected.RodzajPlatnosciId).FirstOrDefault();
                Messenger.Default.Send(new NowyRodzajPlatnosciViewModel(toEdit));
                Messenger.Default.Register<RodzajePlatnosci>(this, toEdit, saveEdit);
            }
            catch (Exception)
            {
                MessageBox.Show("Wybierz rekord, który chcesz edytować");
            }
        }
        private void saveEdit(RodzajePlatnosci edited)
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
                var toDelete = Db.RodzajePlatnosci.Where(a => a.RodzajPlatnosciId == Selected.RodzajPlatnosciId).FirstOrDefault();
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
        #region SortAndFind
        public override void Sort()
        {
            switch (SortField)
            {
                case "Domyślne":
                    List = new ObservableCollection<RodzajePlatnosci>(List.OrderBy(Item => Item.RodzajPlatnosciId));
                    break;
                case "Nazwa":
                    List = new ObservableCollection<RodzajePlatnosci>(List.OrderBy(Item => Item.Nazwa));
                    break;
                case "Ilość dni do spłaty":
                    List = new ObservableCollection<RodzajePlatnosci>(List.OrderBy(Item => Item.IloscDniSplaty));
                    break;
            }
        }
        public override List<string> GetComboBoxSortList()
        {
            return new List<string> { "Domyślne", "Nazwa", "Ilość dni do spłaty" };
        }
        public override void Find()
        {
            try
            {
                switch (FindField)
                {
                    case "Nazwa":
                        List = new ObservableCollection<RodzajePlatnosci>(List.Where(i => i.Nazwa != null && i.Nazwa.StartsWith(FindTextBox)));
                        break;  
                    case "Ilość dni do spłaty":
                        List = new ObservableCollection<RodzajePlatnosci>(List.Where(i => i.IloscDniSplaty == int.Parse(FindTextBox)));
                        break;
                }
            }
            catch (Exception) { }
        }
        public override List<string> GetComboBoxFindList()
        {
            return new List<string> { "Nazwa", "Ilość dni do spłaty" };
        }
        #endregion
    }
}
