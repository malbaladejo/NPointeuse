using System;
using System.Windows.Input;

namespace NPointeuse.Infra.Client
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> execute;
        private readonly Func<T, bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute ?? new Func<T, bool>(v => true);
        }

        public void RaiseCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        bool ICommand.CanExecute(object parameter)
            => parameter is T value ? this.canExecute(value) : false;

        public bool CanExecute(T value) => this.canExecute(value);

        void ICommand.Execute(object parameter)
        {
            if (parameter is T value)
                this.execute(value);
        }

        public void Execute(T value) => this.execute(value);
    }
}
