using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class EwidencjaPlatnosciViewModel : WorkspaceViewModel //bo wszystkie zakładki dziedzicza po workspaceVM
    {
        #region Konstruktor
        public EwidencjaPlatnosciViewModel()
        {
            base.DisplayName = "Ewidencja Płatności";
        }
        #endregion
    }
}
