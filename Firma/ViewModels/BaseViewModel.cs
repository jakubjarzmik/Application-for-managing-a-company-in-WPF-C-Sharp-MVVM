using Firma.Helpers;
using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Firma.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Propertychanged
        protected void OnPropertyChanged<T>(Expression<Func<T>> action)
        {
            var propertyName = GetPropertyName(action);
            OnPropertyChanged(propertyName);
        }
        private static string GetPropertyName<T>(Expression<Func<T>> action)
        {
            var expression = (MemberExpression)action.Body;
            var propertyName = expression.Member.Name;
            return propertyName;
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)

            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region  Command
        public delegate void CommandDelegate();
        protected BaseCommand GetCommand(BaseCommand baseCommand, CommandDelegate function)
        {
            if (baseCommand == null)
            {
                baseCommand = new BaseCommand(() => function());
            }
            return baseCommand;
        }
        #endregion

    }
}
