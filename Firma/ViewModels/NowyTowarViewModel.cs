using Firma.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.ViewModels
{
    public class NowyTowarViewModel:WorkspaceViewModel
    {
        #region Konstruktor
        public NowyTowarViewModel()
        {
            base.DisplayName = "Towar";//tu ustawiamy nazwę zakładki
        }
        #endregion

        #region Własności
        private List<TowarCeny> _TowarCeny;
        public List<TowarCeny> TowarCeny
        {
            get
            {
                _TowarCeny = new List<TowarCeny>();
                _TowarCeny.Add(new TowarCeny
                {
                    Priorytet = 1,
                    Brutto = 0,
                    Netto = 0,
                    Jednostka = "szt",
                    Standardowe = 1,
                    MarzaOdNabycia = 0,
                    NarzutOdNabycia = 0
                });
                _TowarCeny.Add(new TowarCeny
                {
                    Priorytet = 2,
                    Brutto = 0,
                    Netto = 0,
                    Jednostka = "szt",
                    Standardowe = 1,
                    MarzaOdNabycia = 0,
                    NarzutOdNabycia = 0
                });
                return _TowarCeny;
            }
        }
        #endregion
    }
}
