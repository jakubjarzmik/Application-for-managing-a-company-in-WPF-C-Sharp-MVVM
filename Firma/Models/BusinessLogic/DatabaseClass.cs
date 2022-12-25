using Firma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.BusinessLogic
{
    public class DatabaseClass
    {
        #region Entities
        public JJFirmaEntities JJFirmaEntities { get; set; }
        #endregion

        #region Constructor
        public DatabaseClass(JJFirmaEntities jJFirmaEntities)
        {
            JJFirmaEntities = jJFirmaEntities;
        }
        #endregion
    }
}
