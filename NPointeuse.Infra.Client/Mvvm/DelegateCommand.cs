using System;
using System.Windows.Input;

namespace NPointeuse.Infra.Client
{
    public class DelegateCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public event EventHandler CanExecuteChanged;

        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute ?? new Func<bool>(() => true);
        }

        public void RaiseCanExecuteChanged() => this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        bool ICommand.CanExecute(object parameter) => this.canExecute();

        public bool CanExecute() => this.canExecute();

        void ICommand.Execute(object parameter) => this.execute();

        public void Execute() => this.execute();
    }
}
