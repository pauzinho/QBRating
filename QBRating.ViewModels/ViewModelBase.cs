using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace QBRating.ViewModels {
	public abstract class ViewModelBase : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;

		//https://intellitect.com/getting-started-model-view-viewmodel-mvvm-pattern-using-windows-presentation-framework-wpf/
		protected virtual bool SetProperty<T>(ref T currentValue, T newValue, [CallerMemberName]string propertyName = null) {
			if(!EqualityComparer<T>.Default.Equals(currentValue, newValue)) {
				currentValue = newValue;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
				return true;
			}
			return false;
		}

		protected virtual void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}
}
