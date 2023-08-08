using System;
using System.Windows.Input;

namespace Firma.ViewModels
{
    //to jest klasa 
    public class CommandViewModel:BaseViewModel
    {
        #region Własciwosci
        public string DisplayName { get; set; } //to jest nazwa przycisku w menu z lewej strony
        public ICommand Command { get; set; } //każdy przycisk zawiera komende, ktora wywoluje funkcje, ktora otwiera zakladke
        public string IconUrl { get; set; }
        public string IconHoverUrl { get; set; }
        #endregion
        #region Konstruktor
        public CommandViewModel(string displayName, ICommand command, string iconUrl, string iconHoverUrl)
        {
            if (command == null)
                throw new ArgumentNullException("Command");
            this.DisplayName = displayName;
            this.Command = command;
            this.IconUrl = iconUrl;
            this.IconHoverUrl = iconHoverUrl;
        }
        #endregion
    }
}
