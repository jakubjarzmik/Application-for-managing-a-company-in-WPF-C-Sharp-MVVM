using System;

namespace Firma.Models.EntitiesForView
{
    public class WydanieZewnetrzneForAllView
    {
        #region Properties
        public int WydanieZewnetrzneId { get; set; }
        public string Numer { get; set; }
        public DateTime DataWydania { get; set; }
        public string NazwaMagazynu { get; set; }
        public string NazwaKontrahenta { get; set; }
        public string NipKontrahenta { get; set; }
        public decimal Rabat { get; set; }
        #endregion
    }
}
