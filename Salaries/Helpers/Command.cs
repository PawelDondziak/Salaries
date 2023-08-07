﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Salaries.Helpers
{
    public class Command : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public Command(Action execute)
            : this(execute, null)
        {
        }

        public Command(Action execute, Func<bool> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }

}
