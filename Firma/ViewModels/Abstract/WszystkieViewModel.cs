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
        #region Commands
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
        private BaseCommand _EditCommand;
        public BaseCommand EditCommand
        {
            get
            {
                if (_EditCommand == null)
                {
                    _EditCommand = new BaseCommand(() => Edit());
                }
                return _EditCommand;
            }
        }
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
        private BaseCommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new BaseCommand(() => Delete());
                }
                return _DeleteCommand;
            }
        }
        #endregion
        #region Fields
        protected bool toClose;
        protected string token;
        private readonly JJFirmaEntities _Db;
        public JJFirmaEntities Db { get { return _Db; } }
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
            this._Db = new JJFirmaEntities();
            toClose = false;
            SortField = "Domyślne";
        }
        public WszystkieViewModel(string displayName, string token)
        {
            base.DisplayName = displayName;
            this._Db = new JJFirmaEntities();
            this.token = token;
            toClose = true;
            SortField = "Domyślne";
        }
        #endregion
        #region Properties
        protected T _Selected;
        public virtual T Selected
        {
            get
            {
                return _Selected;
            }
            set
            {
                _Selected = value;
            }
        }
        #endregion
        #region SortAndFind
        private BaseCommand _SortCommand;
        public ICommand SortCommand
        {
            get
            {
                if (_SortCommand == null)
                {
                    _SortCommand = new BaseCommand(() => Sort());
                }
                return _SortCommand;
            }
        }
        public abstract void Sort();
        public string SortField { get; set; }
        public List<string> SortComboBoxItems
        {
            get
            {
                return GetComboBoxSortList();
            }
        }
        public abstract List<string> GetComboBoxSortList();
        private BaseCommand _FindCommand;
        public ICommand FindCommand
        {
            get
            {
                if (_FindCommand == null)
                {
                    _FindCommand = new BaseCommand(() => Find());
                }
                return _FindCommand;
            }
        }
        public abstract void Find();
        public string FindField { get; set; }
        public string FindTextBox { get; set; }
        public List<string> FindComboBoxItems
        {
            get
            {
                return GetComboBoxFindList();
            }
        }
        public abstract List<string> GetComboBoxFindList();
        #endregion
        #region Helpers
        public abstract void Add();
        public abstract void Edit();
        public abstract void Load();
        public abstract void Delete();
        #endregion
    }
}
