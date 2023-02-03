using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class FakturyWydaniaZewnetrzneForAllView
    {
        #region Properties
        public int FakturaWZId { get; set; }
        public int FakturaId { get; set; }
        public string NumerFaktury { get; set; }
        public string NazwaKontrahenta { get; set; }
        public int WydanieZewnetrzneId { get; set; }
        public string NumerWZ { get; set; }
        #endregion
    }
}
