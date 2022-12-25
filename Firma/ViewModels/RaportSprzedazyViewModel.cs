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
    public class RaportSprzedazyViewModel : WorkspaceViewModel
    {
        #region Wlasciwosci
        public JJFirmaEntities JJFirmaEntities { get; set; }
        //dla każdego pola na widoku istotnego w obliczeniach tworzymy pole i właściwośc
        private DateTime _DataOd;
        public DateTime DataOd
        {
            get
            {
                return _DataOd;
            }
            set
            {
                if (_DataOd != value)
                {
                    _DataOd = value;
                    OnPropertyChanged(() => DataOd);
                }
            }
        }
        private DateTime _DataDo;
        public DateTime DataDo
        {
            get
            {
                return _DataDo;
            }
            set
            {
                if (_DataDo != value)
                {
                    _DataDo = value;
                    OnPropertyChanged(() => DataDo);
                }
            }
        }
        private int _IdTowaru;
        public int IdTowaru
        {
            get
            {
                return _IdTowaru;
            }
            set
            {
                if (_IdTowaru != value)
                {
                    _IdTowaru = value;
                    OnPropertyChanged(() => IdTowaru);
                }
            }
        }
        //dla każdego ComboBoxa w systemie należy utworzyć następujący properties
        public IQueryable<KeyAndValue> TowaryComboBoxItems
        {
            get
            {
                //w tym miejscu wywołujemy funckję logiki biznesowej która znajduje się w klasie TowarB
                return new TowarB(JJFirmaEntities).GetAktywneTowary();
            }
        }
        private decimal? _Utarg;
        public decimal? Utarg
        {
            get
            {
                return _Utarg;
            }
            set
            {
                if (_Utarg != value)
                {
                    _Utarg = value;
                    OnPropertyChanged(() => Utarg);
                }
            }
        }
        #endregion
        #region Command
        //tworymy komendę którą podłączymy pod przycisk oblicz, która wywoła funkcję obliczUtargClick
        private BaseCommand _ObliczCommand;
        public ICommand ObliczCommand
        {
            get
            {
                if (_ObliczCommand == null)
                    _ObliczCommand = new BaseCommand(obliczUtargClick);
                return _ObliczCommand;
            }
        }
        #endregion
        #region Helpers
        private void obliczUtargClick()
        {
            //wywołujemy funkcję logiki biznesowej z klasy UtargB
            Utarg = new UtargB(JJFirmaEntities).UtargOkresTowar(IdTowaru, DataOd, DataDo);
        }
        #endregion
        #region Konstruktor
        public RaportSprzedazyViewModel()
        {
            base.DisplayName = "Raport Sprzedazy";
            JJFirmaEntities =new JJFirmaEntities();  
            DataOd=DateTime.Now;
            DataDo = DateTime.Now;
            Utarg = 0;
        }
        #endregion
    }
}
