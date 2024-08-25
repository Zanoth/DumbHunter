using Microsoft.Extensions.Configuration;

namespace MHW.Wpf.Providers;

public class IconDirectoryPathProvider : IIconDirectoryPathProvider
{
  private readonly IConfiguration _configuration;

  public IconDirectoryPathProvider(IConfiguration configuration)
  {
    _configuration = configuration;
  }

  public string GetIconDirectoryPath() => _configuration["IconDirectory"];
}