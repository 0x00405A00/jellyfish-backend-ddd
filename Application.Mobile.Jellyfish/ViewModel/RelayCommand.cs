using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MobileApp.ViewModel
{
    public class RelayCommand<T> : ICommand
    {
        #region Private Members
        private Action<T> mAction;
        #endregion
        #region Public Events
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region Constructor
        public RelayCommand(Action<T> action)
        {
            mAction = action;
        }
        #endregion
        #region Command Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            mAction((T)parameter);
        }
        #endregion
    }
    public class RelayCommand<T1, T2> : ICommand
    {
        #region Private Members
        private Action<T1, T2> mAction;
        #endregion
        #region Public Events
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region Constructor
        public RelayCommand(Action<T1, T2> action)
        {
            mAction = action;
        }
        #endregion
        #region Command Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            object[] values = (object[])parameter;
            mAction((T1)values[0], (T2)values[1]);
        }
        #endregion
    }
    public class RelayCommand : ICommand
    {

        #region Private Members
        private Action mAction;
        #endregion
        #region Public Events
        public event EventHandler CanExecuteChanged = (sender, e) => { };
        #endregion

        #region Constructor
        public RelayCommand(Action action)
        {
            mAction = action;
        }
        #endregion
        #region Command Methods
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            mAction();
        }
        #endregion
    }
}
