using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma.Models.EntitiesForView
{
    //to jest klasa pomocnicza do wypełnienia np. ComboBoxów
    public class KeyAndValue
    {
        #region Properties
        public int? Key { get; set; } //np. Id towaru
        public string Value { get; set; } //np. nazwa towaru
        #endregion
    }
}
