namespace Firma.Models.EntitiesForView
{
    public class KontrahenciKontaktyForAllView
    {
        #region Properties
        public int KontrahentKontaktId { get; set; }
        public int KontrahentId { get; set; }
        public string KontrahentKod { get; set; }
        public string KontrahentNazwa { get; set; }
        public string KontrahentNip { get; set; }
        public int KontaktId { get; set; }
        public string KontaktNazwaDzialu { get; set; }
        public string KontaktOpisOsoby { get; set; }
        public string KontaktTelefon1 { get; set; }
        public string KontaktTelefon2 { get; set; }
        public string KontaktEmail1 { get; set; }
        public string KontaktEmail2 { get; set; }
        #endregion
    }
}
