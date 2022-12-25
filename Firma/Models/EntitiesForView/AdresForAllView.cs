using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class AdresForAllView
    {
        #region Properties
        public int AdresId { get; set; }
        public string Kraj { get; set; }
        public string Wojewodztwo { get; set; }
        public string KodPocztowy { get; set; }
        public string Poczta { get; set; }
        public string Ulica { get; set; }
        public string NrDomu { get; set; }
        public string NrLokalu { get; set; }
        
        #endregion


    }
}
