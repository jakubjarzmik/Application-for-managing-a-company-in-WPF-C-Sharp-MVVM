using Firma.Models.Entities;
using Firma.ViewModels.Abstract;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    class UmowyStanowiskaViewModel : WszystkieViewModel<UmowyStanowiska>
    {
        #region Konstruktor
        public UmowyStanowiskaViewModel() 
            : base("Stanowiska")
        {
        }
        #endregion

        #region Helpers
        public override void Load()
        {
            List = new ObservableCollection<UmowyStanowiska>
                (
                    from stanowisko in JJFirmaEntities.UmowyStanowiska
                    where stanowisko.CzyAktywny == true
                    select stanowisko
                );
        }
        #endregion

    }
}
