using Firma.Helpers;
using Firma.Models.Entities;
using GalaSoft.MvvmLight.Messaging;
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
        #region Commands
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
        #endregion
        #region Konstruktor
        public JedenViewModel(string displayName)
        {
            base.DisplayName = displayName;
            Db = new JJFirmaEntities();
        }
        #endregion

        #region Helpers
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
        public abstract void Save();
        private void saveAndClose()
        {
            Save();
            OnRequestClose();
        }
        #endregion
    }
}
