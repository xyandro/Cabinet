using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Cabinet
{
	public class LightGradientConverter : MarkupExtension, IValueConverter
	{
		public override object ProvideValue(IServiceProvider serviceProvider) => this;

		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => value is Color color ? Color.FromArgb(0, color.R, color.G, color.B) : default(Color);

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
	}
}
