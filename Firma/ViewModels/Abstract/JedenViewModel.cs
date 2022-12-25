using Firma.Helpers;
using Firma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firma.ViewModels.Abstract
{
    public abstract class JedenViewModel<T> : WorkspaceViewModel
    {
        #region Fields
        // baza danych
        public JJFirmaEntities Db { get; set; }
        // tu jest nasz dodawany item
        public T Item { get; set; }
        #endregion
        #region Konstruktor
        public JedenViewModel(string displayName)
        {
            base.DisplayName = displayName;//tu ustawiamy nazwę zakładki
            Db = new JJFirmaEntities();
        }
        #endregion
        #region Command
        // to jets komenda która zostanie podpięta (zbindowana) z przyciskiem zapisz i zamknij, komenda ta wywoła funkcję SaveAndClose
        private BaseCommand _SaveAndCloseCommand;
        public ICommand SaveAndCloseCommand
        {
            get
            {
                if (_SaveAndCloseCommand == null)
                {
                    _SaveAndCloseCommand = new BaseCommand(() => saveAndClose());
                }
                return _SaveAndCloseCommand;
            }
        }
        #endregion
        #region Save
        public abstract void Save();
        private void saveAndClose()
        {
            // zapisuje towar
            Save();
            // zamyka zakładkę
            OnRequestClose();
        }
        #endregion
    }
}
