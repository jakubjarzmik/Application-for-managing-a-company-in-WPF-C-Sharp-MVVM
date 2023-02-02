using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class FakturaForAllView
    {
        #region Properties
        public int FakturaId { get; set; }
        public string Numer { get; set; }
        public DateTime DataWystawienia { get; set; }
        public string KontrahentNazwa { get; set; }
        public string KontrahentNip { get; set; }
        public string RodzajePlatnosciNazwa { get; set; }
        
        #endregion


    }
}
