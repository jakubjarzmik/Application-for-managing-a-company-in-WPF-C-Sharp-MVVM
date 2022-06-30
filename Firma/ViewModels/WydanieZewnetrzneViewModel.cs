using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class WydanieZewnetrzneViewModel:WorkspaceViewModel //bo wszystkie zakładki dziedzicza po workspaceVM
    {
        #region Konstruktor
        public WydanieZewnetrzneViewModel()
        {
            base.DisplayName = "Wydanie zewnętrzne";
        }
        #endregion
    }
}
