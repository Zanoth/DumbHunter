using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using SharedDataModels.Abstractions.Gear.Weapons;

namespace Wpf.Design.Controls;

/// <summary>
/// Interaction logic for SharpnessIndicator.xaml
/// </summary>
public partial class SharpnessIndicator : UserControl
{
  public SharpnessIndicator()
  {
    InitializeComponent();
  }

  #region Sharpness

  public static readonly DependencyProperty SharpnessSectionsProperty = DependencyProperty.Register(
    "SharpnessSections", typeof(IEnumerable<SharpnessSection>), typeof(SharpnessIndicator), new PropertyMetadata(new List<SharpnessSection>()));

  public IEnumerable<SharpnessSection> SharpnessSections
  {
    get => (IEnumerable<SharpnessSection>)GetValue(SharpnessSectionsProperty);
    set => SetValue(SharpnessSectionsProperty, value);
  }

  #endregion
}