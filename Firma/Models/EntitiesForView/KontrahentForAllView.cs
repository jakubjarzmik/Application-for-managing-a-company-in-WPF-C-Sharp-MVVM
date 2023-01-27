using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    public class KontrahentForAllView
    {
        #region Properties
        public int KontrahentId { get; set; }
        public string Kod { get; set; }
        public string Nazwa1 { get; set; }
        public string RodzajKontrahenta { get; set; }
        public string Nip { get; set; }
        public string Regon { get; set; }
        public string Adres { get; set; }
        public string Url { get; set; }
        
        #endregion


    }
}
