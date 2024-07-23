using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace MHW.Wpf.Converters;

public class DebugConverter : MarkupExtension, IValueConverter
{
  public override object ProvideValue(IServiceProvider serviceProvider)
    => this;

  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    => value;

  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    => value;
}