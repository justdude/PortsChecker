using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace MVVM
{
	public interface IViewModelBase
	{
		event PropertyChangedEventHandler PropertyChanged;
	}
}
