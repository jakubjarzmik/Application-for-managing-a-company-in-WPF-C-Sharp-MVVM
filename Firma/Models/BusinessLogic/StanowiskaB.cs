using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.BusinessLogic
{
    public class StanowiskaB : DatabaseClass // dziedziczy bo używa bazy danych
    {
        #region Konstruktor
        public StanowiskaB(JJFirmaEntities jJFirmaEntities)
            : base(jJFirmaEntities) 
        {
        }
        #endregion

        #region FunkcjeBiznesowe
        //ta funkcja pobiera wsystkie aktywne towary z bazy danych
        public IQueryable<KeyAndValue> GetAktywneStanowiska()//ta funkcja zwróci kolekjce KeyAndValue dla Comboboxa wyświetlającego wszystkie aktywne towary
        {
            List<KeyAndValue> stanowiska = new List<KeyAndValue>();
            stanowiska.Add(new KeyAndValue { Key = null, Value = "Wszystkie" });
            stanowiska.AddRange
                (
                    (
                        from stanowisko in JJFirmaEntities.UmowyStanowiska //dla kazdego towaru z bazy danych towarow
                        where stanowisko.CzyAktywny==true
                        select new KeyAndValue
                        {
                            Key= stanowisko.StanowiskoId,
                            Value= stanowisko.Nazwa
                        }
                    ).ToList()
                );
            return stanowiska.AsQueryable();
        }
 
        #endregion
    }
}
