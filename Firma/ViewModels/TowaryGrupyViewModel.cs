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
    class TowaryGrupyViewModel : WszystkieViewModel<GrupaTowaruForAllView>
    {
        #region Konstruktor
        public TowaryGrupyViewModel() 
            : base("Grupy towarów")
        {
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
                        GrupaNadrzedna = grupa.TowaryGrupy2.Kod,
                        Kod = grupa.Kod,
                        Nazwa = grupa.Nazwa,
                    }
                );
        }
        #endregion

    }
}
