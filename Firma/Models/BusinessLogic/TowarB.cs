using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.BusinessLogic
{
    public class TowarB : DatabaseClass
    {
        #region Konstruktor
        public TowarB(JJFirmaEntities jJFirmaEntities)
            : base(jJFirmaEntities) 
        {
        }
        #endregion

        #region FunkcjeBiznesowe
        public IQueryable<KeyAndValue> GetAktywneTowary()
        {
            return
                (
                    from towar in Db.Towary
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
