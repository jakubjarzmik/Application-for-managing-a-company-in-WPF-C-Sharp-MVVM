using System;

namespace Firma.Models.EntitiesForView
{
    public class PracownicyUmowyForAllView
    {
        #region Properties
        public int PracownikUmowaId { get; set; }
        public int PracownikId { get; set; }
        public string PracownikNazwa { get; set; }
        public string PracownikPESEL { get; set; }
        public int UmowaId { get; set; }
        public string UmowaNumer { get; set; }
        public string UmowaRodzaj { get; set; }
        public string UmowaStanowisko { get; set; }
        public DateTime UmowaDataOd { get; set; }
        public DateTime UmowaDataDo { get; set; }
        public decimal UmowaStawkaMies { get; set; }
        public decimal UmowaStawkaGodz { get; set; }
        public int UmowaCzasPracyMies { get; set; }
        public decimal UmowaWartoscMies { get; set; }
        #endregion
    }
}
