using Firma.Helpers;
using Firma.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firma.ViewModels.Abstract
{
    public abstract class WszystkieViewModel<T>:WorkspaceViewModel
    {
        #region Fields
        // to jest obiekt który będzie służył do operacji na bazie danych
        private readonly JJFirmaEntities jJFirmaEntities;
        public JJFirmaEntities JJFirmaEntities { get { return jJFirmaEntities; } }
        // to jest komenda do załadowania towarów
        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                {
                    _LoadCommand = new BaseCommand(() => Load()); // pusta wywołuje load
                }
                return _LoadCommand;
            }
        }
        // w tym obiekcie będą wszystkie towary
        private ObservableCollection<T> _List;
        public ObservableCollection<T> List
        {
            get
            {
                if (_List == null) // jak lista jest pusta wywołujemy load żeby załadować towary
                    Load();
                return _List;
            }
            set
            {
                _List = value;
                OnPropertyChanged(() => List);
            }
        }
        #endregion
        #region Konstruktor
        public WszystkieViewModel(string displayName)
        {
            base.DisplayName = displayName;//tu ustawiamy nazwę zakładki
            this.jJFirmaEntities = new JJFirmaEntities();
        }
        #endregion
        #region Helpers
        public abstract void Load();
        #endregion
    }
}
