namespace MHW.Wpf.ViewModels;

public class MainWindowViewModel : BindableBase
{
  private readonly IRegionManager _regionManager;

  public MainWindowViewModel(IRegionManager regionManager)
  {
    _regionManager = regionManager;
    NavigateCommand = new DelegateCommand<string>(Navigate);
  }

  public DelegateCommand<string> NavigateCommand { get; }

  private void Navigate(string viewName)
  {
    _regionManager.RequestNavigate("ContentRegion", viewName);
  }
}