using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using System.Collections.Generic;
using System.Linq;

namespace Firma.Models.BusinessLogic
{
    public class StanowiskaB : DatabaseClass
    {
        #region Konstruktor
        public StanowiskaB(JJFirmaEntities jJFirmaEntities)
            : base(jJFirmaEntities) 
        {
        }
        #endregion

        #region FunkcjeBiznesowe
        public IQueryable<KeyAndValue> GetAktywneStanowiska()
        {
            List<KeyAndValue> stanowiska = new List<KeyAndValue>();
            stanowiska.Add(new KeyAndValue { Key = null, Value = "Wszystkie" });
            stanowiska.AddRange
                (
                    (
                        from stanowisko in Db.UmowyStanowiska 
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
