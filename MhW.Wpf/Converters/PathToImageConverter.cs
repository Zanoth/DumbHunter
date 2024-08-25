using MHW.Wpf.Providers;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace MHW.Wpf.Converters;

public class PathToImageConverter : MarkupExtension, IValueConverter
{
  private readonly IIconDirectoryPathProvider _iconDirectoryPathProvider;

  public PathToImageConverter()
  {
    _iconDirectoryPathProvider = (IIconDirectoryPathProvider)App.Container.Resolve(typeof(IIconDirectoryPathProvider));
  }
    
  public override object ProvideValue(IServiceProvider serviceProvider)
  {
    return this;
  }

  public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
  {
    if (value is not string imageName) return null;

    var iconDirectory = _iconDirectoryPathProvider.GetIconDirectoryPath();
    var fullPath = Path.Combine(iconDirectory, imageName);

    var uri = new Uri(fullPath, UriKind.Absolute);
    var bitmapImage = new BitmapImage(uri);
    return bitmapImage;

  }


  public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
  {
    throw new NotImplementedException();
  }
}