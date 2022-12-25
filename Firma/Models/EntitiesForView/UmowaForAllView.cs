using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class UmowaForAllView
    {
        #region Properties
        public string Numer { get; set; }
        public string Rodzaj { get; set; }
        public string Stanowisko { get; set; }
        public DateTime DataZawarcia { get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public decimal StawkaMies { get; set; }
        public decimal StawkaGodz { get; set; }
        public int CzasPracyMies { get; set; }
        public string Opis { get; set; }
        public string JestEmerytalne { get; set; }
        public string JestRentowe { get; set; }
        public string JestChorobowe { get; set; }
        public string JestWypadkowe { get; set; }
        
        #endregion


    }
}
