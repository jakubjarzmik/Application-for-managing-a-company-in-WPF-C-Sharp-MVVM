using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class WydanieZewnetrzneForAllView
    {
        #region Properties
        public string Numer { get; set; }
        public DateTime DataWydania { get; set; }
        public string NazwaMagazynu { get; set; }
        public string NazwaKontrahenta { get; set; }
        public string NipKontrahenta { get; set; }
        
        #endregion


    }
}
