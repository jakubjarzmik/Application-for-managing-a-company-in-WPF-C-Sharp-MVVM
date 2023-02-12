using Firma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Firma.Models.BusinessLogic
{
    public class UtargB : DatabaseClass
    {
        #region Konstruktor
        public UtargB(JJFirmaEntities jJFirmaEntities)
            : base(jJFirmaEntities)
        {
        }
        #endregion
        #region FunkcjeBiznesowe
        public decimal? UtargOkresTowar(int TowarId, DateTime dataOd, DateTime dataDo)
        {
            try
            {
                return
                    (
                        from pozycjaWydania in Db.PozycjeWydaniaZewnetrznego
                        where
                        pozycjaWydania.TowarId == TowarId &&
                        pozycjaWydania.CzyAktywny == true &&
                        pozycjaWydania.WydaniaZewnetrzne.DataWydania >= dataOd &&
                        pozycjaWydania.WydaniaZewnetrzne.DataWydania <= dataDo
                        select
                        pozycjaWydania.Ilosc *
                        (
                            from cena in Db.ZmianyCeny
                            where
                            cena.CzyAktywny == true &&
                            cena.TowarId == TowarId &&
                            pozycjaWydania.WydaniaZewnetrzne.DataWydania >= cena.DataObowiazywaniaOd
                            &&
                            (pozycjaWydania.WydaniaZewnetrzne.DataWydania <= cena.DataObowiazywaniaDo
                            || cena.DataObowiazywaniaDo == null)
                            orderby cena.DataObowiazywaniaOd descending
                            select cena.CenaNetto
                        ).FirstOrDefault() * (100 - pozycjaWydania.Rabat) / 100 * (100 - pozycjaWydania.WydaniaZewnetrzne.Rabat) / 100
                    ).Sum();
            }
            catch(InvalidOperationException)
            {
                return 0;
            }
        }
        #endregion

    }
}
