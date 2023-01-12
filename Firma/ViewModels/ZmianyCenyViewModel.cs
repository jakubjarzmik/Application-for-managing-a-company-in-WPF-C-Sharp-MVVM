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
        #endregion

    }
}
