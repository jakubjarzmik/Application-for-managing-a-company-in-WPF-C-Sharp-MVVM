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
        #region Command
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
        #region Konstruktor
        public RaportSprzedazyViewModel()
        {
            base.DisplayName = "Raport sprzedaży";
            JJFirmaEntities = new JJFirmaEntities();
            DataOd = DateTime.Now;
            DataDo = DateTime.Now;
            Utarg = 0;
        }
        #endregion
        #region Properties
        public JJFirmaEntities JJFirmaEntities { get; set; }
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
        private int _TowarId;
        public int TowarId
        {
            get
            {
                return _TowarId;
            }
            set
            {
                if (_TowarId != value)
                {
                    _TowarId = value;
                    OnPropertyChanged(() => TowarId);
                }
            }
        }
        public IQueryable<KeyAndValue> TowaryComboBoxItems
        {
            get
            {
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
        #region Helpers
        private void obliczUtargClick()
        {
            Utarg = new UtargB(JJFirmaEntities).UtargOkresTowar(TowarId, DataOd, DataDo);
        }
        #endregion
    }
}
