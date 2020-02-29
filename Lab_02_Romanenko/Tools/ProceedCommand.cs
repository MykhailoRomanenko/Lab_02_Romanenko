using System;
using System.Windows.Input;
using Lab_02_Romanenko.ViewModels;

namespace Lab_02_Romanenko.Tools{
    
    public class ProceedCommand : ICommand
    {
        private MainWindowViewModel _mwvm;
        public ProceedCommand(MainWindowViewModel mwvm)
        {
            _mwvm = mwvm;
        }

        public bool CanExecute(object parameter)
        {
            return _mwvm.CanProceed();
        }

        public void Execute(object parameter)
        {
            _mwvm.Proceed();
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}