using Firma.Helpers;
using System;
using System.Windows.Input;

namespace Firma.ViewModels
{
    //to jest klasa z której będą dziedziczyć wszystkie ViewModele zakładek
    public class WorkspaceViewModel:BaseViewModel
    {
        #region Pola i Komendy
        //każda zkaładka ma minim nazwę i zamknij
        public string DisplayName { get; set; } //w tym properties będzie przychowywana nazwa zakładki
        private BaseCommand _CloseCommand; //to jest komenda do obsługi zamykania okna
        public ICommand CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                    _CloseCommand = new BaseCommand(() => this.OnRequestClose()); //ta komenda wyoła funkcje zamykającą zakładkę 
                return _CloseCommand;
            }
        }
        #endregion
        #region RequestClose [event]
        public event EventHandler RequestClose;
        public void OnRequestClose()
        {
            EventHandler handler = this.RequestClose;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        public static explicit operator WorkspaceViewModel(Type v)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}
