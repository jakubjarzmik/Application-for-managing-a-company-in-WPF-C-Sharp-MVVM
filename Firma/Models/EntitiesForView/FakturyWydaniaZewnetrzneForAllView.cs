namespace Firma.Models.EntitiesForView
{
    public class FakturyWydaniaZewnetrzneForAllView
    {
        #region Properties
        public int FakturaWZId { get; set; }
        public int FakturaId { get; set; }
        public string FakturaNumer { get; set; }
        public string FakturaKontrahentNazwa { get; set; }
        public int WydanieZewnetrzneId { get; set; }
        public string WZNumer { get; set; }
        public string WZMagazynNazwa { get; set; }
        public decimal WZRabat { get; set; }
        #endregion
    }
}
