using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class TowaryViewModel : WszystkieViewModel<TowarForAllView>
    {
        #region Konstruktor
        public TowaryViewModel():base("Towary")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<TowarForAllView>
                (
                    from towar in JJFirmaEntities.Towary
                    where towar.CzyAktywny == true
                    select new TowarForAllView
                    {
                        Kod = towar.Kod,
                        Nazwa = towar.Nazwa,
                        Typ = towar.TowaryTypy.Nazwa,
                        Grupa = towar.TowaryGrupy.Nazwa,
                        NumerKatalogowy = towar.NumerKatalogowy,
                        EAN = towar.EAN,
                        Producent = towar.Kontrahenci.Nazwa1,
                        KrajPochodzenia = towar.Kraje.Nazwa
                    }
                );
        }
        #endregion

    }
}
