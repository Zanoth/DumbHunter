using MHW.Wpf.ViewModels;
using System.Windows;

namespace MHW.Wpf.Views
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow(MainWindowViewModel viewModel)
    {
      DataContext = viewModel;
      InitializeComponent();
    }
  }
}