using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models
{
    public class TowarCeny
    {
        public int Priorytet { get; set; }
        public decimal Netto { get; set; }
        public decimal Brutto { get; set; }
        public string Jednostka { get; set; }
        public int Standardowe { get; set; }
        public int NarzutOdNabycia { get; set; }
        public int MarzaOdNabycia { get; set; }

    }
}
