using Prism.Commands;
using Prism.Regions;

namespace MHW.Wpf.ViewModels;

public class MainWindowViewModel
{
  private readonly IRegionManager _regionManager;

  public MainWindowViewModel(IRegionManager regionManager)
  {
    _regionManager = regionManager;

    NavigateToGearManagementCommand = new DelegateCommand(NavigateToGearManagement);
    NavigateToMonsterListCommand = new DelegateCommand(NavigateToMonsterList);

  }

  private void NavigateToGearManagement()
  {
    _regionManager.RequestNavigate("MainRegion", "GearManagementViewModel");
  }

  private void NavigateToMonsterList()
  {
    _regionManager.RequestNavigate("MainRegion", "MonsterListViewModel");
  }

  public DelegateCommand NavigateToGearManagementCommand { get; }
  public DelegateCommand NavigateToMonsterListCommand { get; }
}