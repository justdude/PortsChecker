﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace MVVM
{
	/// <summary>
	/// Provides common functionality for ViewModel classes
	/// </summary>
	public abstract class ViewModelBase : INotifyPropertyChanged, IViewModelBase
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;

			if (handler != null) {
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}

	}
}
