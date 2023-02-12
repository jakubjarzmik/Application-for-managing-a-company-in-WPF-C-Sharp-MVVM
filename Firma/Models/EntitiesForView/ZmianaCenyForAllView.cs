using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class ZmianaCenyForAllView
    {
        #region Properties
        public int ZmianaCenyId { get; set; }
        public string Towar { get; set; }
        public string JednostkaMiary { get; set; }
        public decimal CenaNetto { get; set; }
        public decimal CenaBrutto { get; set; }
        public decimal WartoscVat { get; set; }
        public DateTime DataObowiazywaniaOd { get; set; }
        public DateTime? DataObowiazywaniaDo { get; set; }
        #endregion
    }
}
