/*
 * Created by SharpDevelop.
 * User: Albantov
 * Date: 28.10.2014
 */
using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Markup;
using Data;
using Client;

namespace Converters
{
	/// <summary>
	/// Description of StateToColorConverter.
	/// </summary>
	public class StateToColorConverter: MarkupExtension, IValueConverter
	{
		private static StateToColorConverter modConverter = null;
		
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			if (modConverter == null)
				modConverter = new StateToColorConverter();
			return modConverter;
		}
		
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			var states = (ConnectionState) value;
			switch (states)
			{
				case ConnectionState.Connected: 
					return new SolidColorBrush(Colors.Green);
				case ConnectionState.Disconnected: 
					return new SolidColorBrush(Colors.Red);
				case ConnectionState.Receive: 
					return new SolidColorBrush(Colors.Orange);
				case ConnectionState.Send: 
					return new SolidColorBrush(Colors.Blue);
			}
			return new SolidColorBrush(Colors.Red);
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
