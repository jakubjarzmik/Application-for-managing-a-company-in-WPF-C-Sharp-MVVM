﻿using Firma.Models.Entities;
using System;
using System.Linq;

namespace Firma.Models.BusinessLogic
{
    public class SrednieWynagrodzenieB : DatabaseClass
    {
        #region Konstruktor
        public SrednieWynagrodzenieB(JJFirmaEntities jJFirmaEntities)
            : base(jJFirmaEntities)
        {
        }
        #endregion
        #region FunkcjeBiznesowe
        public decimal? SrednieWynagrodzenie(DateTime data, int? IdStanowiska)
        {
            try
            {
                if (IdStanowiska != null)
                {
                    return
                        (
                            from umowa in Db.Umowy
                            where
                            umowa.StanowiskoId == IdStanowiska &&
                            umowa.CzyAktywny == true &&
                            umowa.DataOd <= data &&
                            umowa.DataDo >= data
                            select
                            umowa.StawkaBruttoMies + (umowa.StawkaBruttoGodz * umowa.CzasPracyMies)
                        ).Average();
                }
                else
                {
                    return
                    (
                        from umowa in Db.Umowy
                        where
                        umowa.CzyAktywny == true &&
                        umowa.DataOd <= data &&
                        umowa.DataDo >= data
                        select
                        umowa.StawkaBruttoMies + (umowa.StawkaBruttoGodz * umowa.CzasPracyMies)
                    ).Average();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        #endregion

    }
}
