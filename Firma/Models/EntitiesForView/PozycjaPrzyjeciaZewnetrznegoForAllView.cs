﻿namespace Firma.Models.EntitiesForView
{
    public class PozycjaPrzyjeciaZewnetrznegoForAllView
    {
        #region Properties
        public int PozycjaPZId { get; set; }
        public string NumerPrzyjeciaZewnetrznego { get; set; }
        public string NazwaKontrahenta { get; set; }
        public string NazwaTowaru { get; set; }
        public int Ilosc { get; set; }
        public string JednostkaMiary { get; set; }
        public decimal PierwotnaCenaZakupu { get; set; }
        public decimal Rabat { get; set; }
        public decimal CenaPoRabacieZaSzt { get; set; }
        public decimal Wartosc { get; set; }
        #endregion
    }
}
