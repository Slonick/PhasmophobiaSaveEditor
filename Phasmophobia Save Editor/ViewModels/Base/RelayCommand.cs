using System;
using System.Windows.Input;

namespace PhasmophobiaSaveEditor.ViewModels.Base
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly Func<bool> canExecute;
        private readonly Action execute;
        private bool isExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }

        public bool CanExecute(object parameter) => this.canExecute == null || this.canExecute();

        public void Execute(object parameter)
        {
            if (this.isExecute)
            {
                return;
            }

            this.isExecute = true;
            this.execute?.Invoke();
            this.isExecute = false;
        }
    }

    public class RelayCommand<T> : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        private readonly Predicate<T> canExecute;
        private readonly Action<T> execute;
        private bool isExecute;

        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => this.canExecute == null || this.canExecute((T) parameter);

        public void Execute(object parameter)
        {
            if (this.isExecute)
            {
                return;
            }

            this.isExecute = true;
            this.execute?.Invoke((T) parameter);
            this.isExecute = false;
        }
    }
}