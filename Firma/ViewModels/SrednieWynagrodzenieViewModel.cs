using Firma.Helpers;
using Firma.Models.BusinessLogic;
using Firma.Models.Entities;
using Firma.Models.EntitiesForView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Firma.ViewModels
{
    public class SrednieWynagrodzenieViewModel : WorkspaceViewModel
    {
        #region Wlasciwosci
        public JJFirmaEntities JJFirmaEntities { get; set; }
        //dla każdego pola na widoku istotnego w obliczeniach tworzymy pole i właściwośc
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
        //dla każdego ComboBoxa w systemie należy utworzyć następujący properties
        public IQueryable<KeyAndValue> StanowiskaComboBoxItems
        {
            get
            {
                //w tym miejscu wywołujemy funckję logiki biznesowej która znajduje się w klasie TowarB
                return new StanowiskaB(JJFirmaEntities).GetAktywneStanowiska();
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
        #region Command
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
        #region Helpers
        private void obliczSrednieWynagrodzenieClick()
        {
            SrednieWynagrodzenie = new SrednieWynagrodzenieB(JJFirmaEntities).SrednieWynagrodzenie(Data, StanowiskoId);
        }
        #endregion
        #region Konstruktor
        public SrednieWynagrodzenieViewModel()
        {
            base.DisplayName = "Srednie wynagrodzenie";
            JJFirmaEntities=new JJFirmaEntities();  
            Data=DateTime.Now;
            SrednieWynagrodzenie = 0;
        }
        #endregion
    }
}
