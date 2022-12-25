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
        public decimal? UtargOkresTowar(int IdTowaru, DateTime dataOd, DateTime dataDo)
        {
            //zapytanie liczy za jaką kwotę sprzedano wybrany towar w wybranym okresie
            return
                (
                    from pozycjaWydania in JJFirmaEntities.PozycjeWydaniaZewnetrznego
                    where
                    pozycjaWydania.TowarId == IdTowaru &&
                    pozycjaWydania.CzyAktywny == true &&
                    pozycjaWydania.WydaniaZewnetrzne.DataWydania >= dataOd &&
                    pozycjaWydania.WydaniaZewnetrzne.DataWydania <= dataDo
                    select
                    pozycjaWydania.Ilosc *
                    (
                        from cena in JJFirmaEntities.ZmianyCeny
                        where 
                        cena.CzyAktywny == true &&
                        cena.TowarId == IdTowaru  &&
                        pozycjaWydania.WydaniaZewnetrzne.DataWydania >= cena.DataObowiazywaniaOd 
                        &&
                        (pozycjaWydania.WydaniaZewnetrzne.DataWydania >= cena.DataObowiazywaniaDo
                        || cena.DataObowiazywaniaDo == null)
                        select cena.CenaNetto
                    ).FirstOrDefault() * (100 - pozycjaWydania.Rabat)/100
                ).Sum();
        }
        #endregion

    }
}
