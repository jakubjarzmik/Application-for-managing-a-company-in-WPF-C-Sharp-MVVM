using System;

namespace Firma.Models.EntitiesForView
{
    public class PrzyjecieZewnetrzneForAllView
    {
        #region Properties
        public int PrzyjecieZewnetrzneId { get; set; }
        public string Numer { get; set; }
        public DateTime DataPrzyjecia { get; set; }
        public string NazwaMagazynu { get; set; }
        public string NazwaKontrahenta { get; set; }
        public string NipKontrahenta { get; set; }
        #endregion
    }
}
