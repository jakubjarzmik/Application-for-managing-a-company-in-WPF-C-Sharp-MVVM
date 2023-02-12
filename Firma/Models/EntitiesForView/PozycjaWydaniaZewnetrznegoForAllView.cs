using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class PozycjaWydaniaZewnetrznegoForAllView
    {
        #region Properties
        public int PozycjaWZId { get; set; }
        public string NumerWydaniaZewnetrznego { get; set; }
        public string NazwaKontrahenta { get; set; }
        public string NazwaTowaru { get; set; }
        public int Ilosc { get; set; }
        public string JednostkaMiary { get; set; }
        public decimal Rabat { get; set; }
        #endregion
    }
}
