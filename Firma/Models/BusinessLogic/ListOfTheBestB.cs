using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using System.Collections.Generic;
using System.Linq;

namespace Firma.Models.BusinessLogic
{
    public class ListOfTheBestB : DatabaseClass 
    {
        #region Konstruktor
        public ListOfTheBestB(JJFirmaEntities jJFirmaEntities)
            : base(jJFirmaEntities) 
        {
        }
        #endregion

        #region FunkcjeBiznesowe
        public IQueryable<KeyAndValue> GetListOfTheBest()
        {
            var List = new List<KeyAndValue>() 
            { 
                new KeyAndValue { Key = 0, Value = "Najpopularniejszy towar" },
                new KeyAndValue { Key = 1, Value = "Najlepiej zarabiający pracownik" },
                new KeyAndValue { Key = 2, Value = "Najwięcej pracowników na stanowisku" },
            };
            return List.AsQueryable();
        }
 
        #endregion
    }
}
