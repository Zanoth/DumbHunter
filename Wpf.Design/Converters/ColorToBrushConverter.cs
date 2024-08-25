using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;


namespace Wpf.Design.Converters
{
  public class ColorToBrushConverter : MarkupExtension, IValueConverter
  {
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return this;
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is System.Drawing.Color color)
        return new SolidColorBrush(Color.FromArgb(255, color.R, color.G, color.B));

      return Brushes.HotPink;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}