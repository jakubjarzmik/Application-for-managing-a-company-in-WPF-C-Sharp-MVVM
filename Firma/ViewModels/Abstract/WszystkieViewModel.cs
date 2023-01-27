using Firma.Helpers;
using Firma.Models.Entities;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firma.ViewModels.Abstract
{
    public abstract class WszystkieViewModel<T> : WorkspaceViewModel
    {
        #region Fields
        protected bool toClose;
        protected string token;
        private BaseCommand _AddCommand;
        public BaseCommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new BaseCommand(() => Add());
                }
                return _AddCommand;
            }
        }
        private readonly JJFirmaEntities jJFirmaEntities;
        public JJFirmaEntities JJFirmaEntities { get { return jJFirmaEntities; } }
        private BaseCommand _LoadCommand;
        public ICommand LoadCommand
        {
            get
            {
                if (_LoadCommand == null)
                {
                    _LoadCommand = new BaseCommand(() => Load());
                }
                return _LoadCommand;
            }
        }
        private ObservableCollection<T> _List;
        public ObservableCollection<T> List
        {
            get
            {
                if (_List == null)
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
            base.DisplayName = displayName;
            this.jJFirmaEntities = new JJFirmaEntities();
            toClose = false;
        }
        public WszystkieViewModel(string displayName, string token)
        {
            base.DisplayName = displayName;
            this.jJFirmaEntities = new JJFirmaEntities();
            this.token = token;
            toClose = true;
        }
        #endregion
        #region Helpers
        public abstract void Load();
        public void Add()
        {
            Messenger.Default.Send(DisplayName + "Add");
        }
        #endregion
    }
}
