using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;

namespace Wpf.Design.Controls;

/// <summary>
/// Interaction logic for SharpnessSectionsIndicator.xaml
/// </summary>
public partial class SharpnessSectionIndicator : UserControl
{
  public SharpnessSectionIndicator()
  {
    InitializeComponent();
  }


  #region Color

  public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
    "Color", typeof(Color), typeof(SharpnessSectionIndicator), new PropertyMetadata(Color.HotPink));

  public Color Color
  {
    get => (Color)GetValue(ColorProperty);
    set => SetValue(ColorProperty, value);
  }

  #endregion

  #region Value

  public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(
    "Value", typeof(int), typeof(SharpnessIndicator), new PropertyMetadata(0));

  public int Value
  {
    get => (int)GetValue(ValueProperty);
    set => SetValue(ValueProperty, value);
  }

  #endregion
}