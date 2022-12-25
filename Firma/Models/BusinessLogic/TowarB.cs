using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.BusinessLogic
{
    public class TowarB : DatabaseClass // dziedziczy bo używa bazy danych
    {
        #region Konstruktor
        public TowarB(JJFirmaEntities jJFirmaEntities)
            : base(jJFirmaEntities) 
        {
        }
        #endregion

        #region FunkcjeBiznesowe
        //ta funkcja pobiera wsystkie aktywne towary z bazy danych
        public IQueryable<KeyAndValue> GetAktywneTowary()//ta funkcja zwróci kolekjce KeyAndValue dla Comboboxa wyświetlającego wszystkie aktywne towary
        {
            return
                (
                    from towar in JJFirmaEntities.Towary //dla kazdego towaru z bazy danych towarow
                    where towar.CzyAktywny==true
                    select new KeyAndValue
                    {
                        Key=towar.TowarId,
                        Value=towar.Nazwa
                    }
                ).ToList().AsQueryable();
        }
 
        #endregion
    }
}
