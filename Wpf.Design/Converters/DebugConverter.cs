using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace Wpf.Design.Converters;

public class DebugConverter : MarkupExtension, IValueConverter
{
  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return this;
  }

  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value;
  }

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    return value;
  }
}