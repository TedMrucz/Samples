using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Client
{
	public class BooleanToVisibilityConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, string language)
		{
			bool visibility = (bool)value;

			bool isInverse = (parameter == null) ? false : true;

			if (isInverse)
			{
				visibility = !visibility;
			}

			return visibility ? Visibility.Visible : Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
