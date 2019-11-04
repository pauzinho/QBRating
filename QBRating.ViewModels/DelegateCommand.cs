﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QBRating.ViewModels {
	public class DelegateCommand : ICommand {
		private readonly Action<object> _execute;
		private readonly Func<object, bool> _canExecute;
		public event EventHandler CanExecuteChanged;

		public DelegateCommand(Action<object> execute, Func<object, bool> canExecute) {
			_execute = execute;
			_canExecute = canExecute;
		}

		public bool CanExecute(object parameter) {
			return _canExecute(parameter);
		}

		public void Execute(object parameter) => _execute(parameter);

		public void RaiseCanExecuteChanged() {
			CanExecuteChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
