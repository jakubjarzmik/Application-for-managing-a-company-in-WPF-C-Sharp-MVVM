using Firma.Helpers;
using Firma.Models.Entities;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Firma.ViewModels.Abstract
{
    public abstract class JedenViewModel<T> : WorkspaceViewModel
    {
        #region Commands
        private BaseCommand _AddCommand;
        public BaseCommand AddCommand
        {
            get
            {
                if (_AddCommand == null)
                {
                    _AddCommand = new BaseCommand(() => add());
                }
                return _AddCommand;
            }
        }
        private BaseCommand _DeleteCommand;
        public BaseCommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new BaseCommand(() => delete());
                }
                return _DeleteCommand;
            }
        }
        private BaseCommand _RefreshCommand;
        public BaseCommand RefreshCommand
        {
            get
            {
                if (_RefreshCommand == null)
                {
                    _RefreshCommand = new BaseCommand(() => Load());
                }
                return _RefreshCommand;
            }
        }
        private BaseCommand _ShowFakturyCommand;
        public BaseCommand ShowFakturyCommand
        {
            get
            {
                if (_ShowFakturyCommand == null)
                {
                    _ShowFakturyCommand = new BaseCommand(() => showFaktury());
                }
                return _ShowFakturyCommand;
            }
        }
        private BaseCommand _ShowKontrahenciCommand;
        public BaseCommand ShowKontrahenciCommand
        {
            get
            {
                if (_ShowKontrahenciCommand == null)
                {
                    _ShowKontrahenciCommand = new BaseCommand(() => showKontrahenci());
                }
                return _ShowKontrahenciCommand;
            }
        }
        private BaseCommand _ShowGrupyTowaruCommand;
        public BaseCommand ShowGrupyTowaruCommand
        {
            get
            {
                if (_ShowGrupyTowaruCommand == null)
                {
                    _ShowGrupyTowaruCommand = new BaseCommand(() => showGrupyTowaru());
                }
                return _ShowGrupyTowaruCommand;
            }
        }
        private BaseCommand _ShowKrajeCommand;
        public BaseCommand ShowKrajeCommand
        {
            get
            {
                if (_ShowKrajeCommand == null)
                {
                    _ShowKrajeCommand = new BaseCommand(() => showKraje());
                }
                return _ShowKrajeCommand;
            }
        }
        private BaseCommand _ShowStanowiskaCommand;
        public BaseCommand ShowStanowiskaCommand
        {
            get
            {
                if (_ShowStanowiskaCommand == null)
                {
                    _ShowStanowiskaCommand = new BaseCommand(() => showStanowiska());
                }
                return _ShowStanowiskaCommand;
            }
        }
        private BaseCommand _ShowTowaryCommand;
        public BaseCommand ShowTowaryCommand
        {
            get
            {
                if (_ShowTowaryCommand == null)
                {
                    _ShowTowaryCommand = new BaseCommand(() => showTowary());
                }
                return _ShowTowaryCommand;
            }
        }
        private BaseCommand _ShowJednostkiMiaryCommand;
        public BaseCommand ShowJednostkiMiaryCommand
        {
            get
            {
                if (_ShowJednostkiMiaryCommand == null)
                {
                    _ShowJednostkiMiaryCommand = new BaseCommand(() => showJednostkiMiary());
                }
                return _ShowJednostkiMiaryCommand;
            }
        }
        private BaseCommand _ShowAdresyCommand;
        public BaseCommand ShowAdresyCommand
        {
            get
            {
                if (_ShowAdresyCommand == null)
                {
                    _ShowAdresyCommand = new BaseCommand(() => showAdresy(false));
                }
                return _ShowAdresyCommand;
            }
        }
        private BaseCommand _ShowAdresyKorCommand;
        public BaseCommand ShowAdresyKorCommand
        {
            get
            {
                if (_ShowAdresyKorCommand == null)
                {
                    _ShowAdresyKorCommand = new BaseCommand(() => showAdresy(true));
                }
                return _ShowAdresyKorCommand;
            }
        }
        private BaseCommand _ShowVatSprzCommand;
        public BaseCommand ShowVatSprzCommand
        {
            get
            {
                if (_ShowVatSprzCommand == null)
                {
                    _ShowVatSprzCommand = new BaseCommand(() => showVat("Sprz"));
                }
                return _ShowVatSprzCommand;
            }
        }
        private BaseCommand _ShowVatZakCommand;
        public BaseCommand ShowVatZakCommand
        {
            get
            {
                if (_ShowVatZakCommand == null)
                {
                    _ShowVatZakCommand = new BaseCommand(() => showVat("Zak"));
                }
                return _ShowVatZakCommand;
            }
        }
        private BaseCommand _ShowWydaniaZewnetrzneCommand;
        public BaseCommand ShowWydaniaZewnetrzneCommand
        {
            get
            {
                if (_ShowWydaniaZewnetrzneCommand == null)
                {
                    _ShowWydaniaZewnetrzneCommand = new BaseCommand(() => showWydaniaZewnetrzne());
                }
                return _ShowWydaniaZewnetrzneCommand;
            }
        }
        private BaseCommand _ShowPrzyjeciaZewnetrzneCommand;
        public BaseCommand ShowPrzyjeciaZewnetrzneCommand
        {
            get
            {
                if (_ShowPrzyjeciaZewnetrzneCommand == null)
                {
                    _ShowPrzyjeciaZewnetrzneCommand = new BaseCommand(() => showPrzyjeciaZewnetrzne());
                }
                return _ShowPrzyjeciaZewnetrzneCommand;
            }
        }
        private BaseCommand _ShowKontaktyCommand;
        public BaseCommand ShowKontaktyCommand
        {
            get
            {
                if (_ShowKontaktyCommand == null)
                {
                    _ShowKontaktyCommand = new BaseCommand(() => showKontakty());
                }
                return _ShowKontaktyCommand;
            }
        }
        private BaseCommand _ShowPracownicyCommand;
        public BaseCommand ShowPracownicyCommand
        {
            get
            {
                if (_ShowPracownicyCommand == null)
                {
                    _ShowPracownicyCommand = new BaseCommand(() => showPracownicy());
                }
                return _ShowPracownicyCommand;
            }
        }
        private BaseCommand _ShowUmowyCommand;
        public BaseCommand ShowUmowyCommand
        {
            get
            {
                if (_ShowUmowyCommand == null)
                {
                    _ShowUmowyCommand = new BaseCommand(() => showUmowy());
                }
                return _ShowUmowyCommand;
            }
        }
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
        #region Fields
        public JJFirmaEntities Db { get; set; }
        public T Item { get; set; }
        protected bool isEditing;
        #endregion
        #region Konstruktor
        public JedenViewModel(string displayName)
        {
            base.DisplayName = displayName;
            Db = new JJFirmaEntities();
        }
        #endregion
        #region Properties
        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                if (value)
                {
                    _isEnabled = value;
                    base.OnPropertyChanged(() => IsEnabled);
                }
            }
        }
        #endregion
        #region Helpers
        protected virtual void add() { }
        protected virtual void delete() { }
        protected virtual void Load() { }
        private void showFaktury()
        {
            Messenger.Default.Send("FakturyAll;" + DisplayName);
        }
        private void showKontrahenci()
        {
            Messenger.Default.Send("KontrahenciAll;" + DisplayName);
        }
        private void showGrupyTowaru()
        {
            Messenger.Default.Send("GrupyTowaruAll;" + DisplayName);
        }
        private void showKraje()
        {
            Messenger.Default.Send("KrajeAll;" + DisplayName);
        }
        private void showStanowiska()
        {
            Messenger.Default.Send("StanowiskaAll;" + DisplayName);
        }
        private void showTowary()
        {
            Messenger.Default.Send("TowaryAll;" + DisplayName);
        }
        private void showJednostkiMiary()
        {
            Messenger.Default.Send("JednostkiMiaryAll;" + DisplayName);
        }
        private void showAdresy(bool isAdresKor)
        {
            Messenger.Default.Send((isAdresKor ? "AdresyKorAll;" : "AdresyAll;") + DisplayName);
        }
        private void showVat(string rodzajVat)
        {
            Messenger.Default.Send("Vat" + rodzajVat + "All;" + DisplayName);
        }
        private void showWydaniaZewnetrzne()
        {
            Messenger.Default.Send("WydaniaZewnetrzneAll;" + DisplayName);
        }
        private void showPrzyjeciaZewnetrzne()
        {
            Messenger.Default.Send("PrzyjeciaZewnetrzneAll;" + DisplayName);
        }
        private void showKontakty()
        {
            Messenger.Default.Send("KontaktyAll;" + DisplayName);
        }
        private void showPracownicy()
        {
            Messenger.Default.Send("PracownicyAll;" + DisplayName);
        }
        private void showUmowy()
        {
            Messenger.Default.Send("UmowyAll;" + DisplayName);
        }
        public abstract void Save();
        private void Edit()
        {
            Messenger.Default.Send(Item, Item);
        }
        private void saveAndClose()
        {
            if (isEditing)
                Edit();
            else
                Save();
            OnRequestClose();
        }
        #endregion
    }
}
