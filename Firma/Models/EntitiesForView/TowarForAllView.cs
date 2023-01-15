using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class TowarForAllView
    {
        #region Properties
        public string Kod { get; set; }
        public string Nazwa { get; set; }
        public string Typ { get; set; }
        public string Grupa { get; set; }
        public string NumerKatalogowy { get; set; }
        public string EAN { get; set; }
        public string Producent { get; set; }
        public string KrajPochodzenia { get; set; }
        public int? Ilosc { get; set; }
        public string JednMiary { get; set; }
        #endregion


    }
}
