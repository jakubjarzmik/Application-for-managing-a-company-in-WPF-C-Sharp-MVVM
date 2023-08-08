using Firma.Models.Entities;

namespace Firma.Models.BusinessLogic
{
    public class DatabaseClass
    {
        #region Entities
        public JJFirmaEntities Db { get; set; }
        #endregion

        #region Constructor
        public DatabaseClass(JJFirmaEntities Db)
        {
            this.Db = Db;
        }
        #endregion
    }
}
