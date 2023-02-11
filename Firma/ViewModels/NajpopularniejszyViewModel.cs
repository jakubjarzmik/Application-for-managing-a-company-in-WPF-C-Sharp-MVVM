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
    public class NajpopularniejszyViewModel : WorkspaceViewModel
    {
        #region Command
        private BaseCommand _ObliczCommand;
        public ICommand ObliczCommand
        {
            get
            {
                if (_ObliczCommand == null)
                    _ObliczCommand = new BaseCommand(sprawdzNajpopularniejszy);
                return _ObliczCommand;
            }
        }
        #endregion
        #region Konstruktor
        public NajpopularniejszyViewModel()
        {
            base.DisplayName = "Najpopularniejszy";
            Db = new JJFirmaEntities();
            DataOd = DateTime.Now;
            DataDo = DateTime.Now;
        }
        #endregion
        #region Properties
        public JJFirmaEntities Db { get; set; }
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
        private int _TheBestId;
        public int TheBestId
        {
            get
            {
                return _TheBestId;
            }
            set
            {
                if (_TheBestId != value)
                {
                    _TheBestId = value;
                    OnPropertyChanged(() => TheBestId);
                }
            }
        }
        public IQueryable<KeyAndValue> TheBestComboBoxItems
        {
            get
            {
                return new ListOfTheBestB(Db).GetListOfTheBest();
            }
        }
        private string _Najpopularniejszy;
        public string Najpopularniejszy
        {
            get
            {
                return _Najpopularniejszy;
            }
            set
            {
                if (_Najpopularniejszy != value)
                {
                    _Najpopularniejszy = value;
                    OnPropertyChanged(() => Najpopularniejszy);
                }
            }
        }
        #endregion
        #region Helpers
        private void sprawdzNajpopularniejszy()
        {
            Najpopularniejszy = new TheBestB(Db).TheBestOf(TheBestId, DataOd, DataDo);
        }
        #endregion
    }
}
