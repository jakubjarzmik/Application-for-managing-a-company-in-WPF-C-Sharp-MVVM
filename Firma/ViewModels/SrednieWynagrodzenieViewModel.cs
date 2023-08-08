using Firma.Helpers;
using Firma.Models.BusinessLogic;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using System;
using System.Linq;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class SrednieWynagrodzenieViewModel : WorkspaceViewModel
    {
        #region Commands
        private BaseCommand _ObliczCommand;
        public ICommand ObliczCommand
        {
            get
            {
                if (_ObliczCommand == null)
                    _ObliczCommand = new BaseCommand(obliczSrednieWynagrodzenieClick);
                return _ObliczCommand;
            }
        }
        #endregion
        #region Konstruktor
        public SrednieWynagrodzenieViewModel()
        {
            base.DisplayName = "Srednie wynagrodzenie";
            Db = new JJFirmaEntities();
            Data = DateTime.Now;
            SrednieWynagrodzenie = 0;
        }
        #endregion
        #region Properties
        public JJFirmaEntities Db { get; set; }
        private DateTime _Data;
        public DateTime Data
        {
            get
            {
                return _Data;
            }
            set
            {
                if (_Data != value)
                {
                    _Data = value;
                    OnPropertyChanged(() => Data);
                }
            }
        }
        private int? _StanowiskoId;
        public int? StanowiskoId
        {
            get
            {
                return _StanowiskoId;
            }
            set
            {
                if (_StanowiskoId != value)
                {
                    _StanowiskoId = value;
                    OnPropertyChanged(() => StanowiskoId);
                }
            }
        }
        public IQueryable<KeyAndValue> StanowiskaComboBoxItems
        {
            get
            {
                return new StanowiskaB(Db).GetAktywneStanowiska();
            }
        }
        private decimal? _SrednieWynagrodzenie;
        public decimal? SrednieWynagrodzenie
        {
            get
            {
                return _SrednieWynagrodzenie;
            }
            set
            {
                if (_SrednieWynagrodzenie != value)
                {
                    _SrednieWynagrodzenie = value;
                    OnPropertyChanged(() => SrednieWynagrodzenie);
                }
            }
        }
        #endregion
        #region Helpers
        private void obliczSrednieWynagrodzenieClick()
        {
            SrednieWynagrodzenie = new SrednieWynagrodzenieB(Db).SrednieWynagrodzenie(Data, StanowiskoId);
        }
        #endregion
    }
}
