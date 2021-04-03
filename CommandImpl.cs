using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wpfLoginForm
{
    public class CommandImpl : ICommand
    {
        private Action executor;
        private Func<bool> canExecutorExecute;

        public CommandImpl(Action executor, Func<bool> canExecutorExecute)
        {
            this.executor = executor;
            this.canExecutorExecute = canExecutorExecute;
        }

        public CommandImpl(Action executor)
        {
            this.executor = executor;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (canExecutorExecute != null) return canExecutorExecute();
            if (executor != null) return true;
            return false;
        }

        public void Execute(object parameter)
        {
            executor?.Invoke();
        }
    }
}
