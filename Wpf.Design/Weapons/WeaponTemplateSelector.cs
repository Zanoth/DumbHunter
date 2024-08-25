using System;
using System.Windows;
using System.Windows.Controls;
using SharedDataModels.Abstractions.Gear.Weapons.Bows;
using SharedDataModels.Abstractions.Gear.Weapons.ChargeBlades;
using SharedDataModels.Abstractions.Gear.Weapons.DualBlades;
using SharedDataModels.Abstractions.Gear.Weapons.GreatSwords;
using SharedDataModels.Abstractions.Gear.Weapons.Gunlances;
using SharedDataModels.Abstractions.Gear.Weapons.Hammers;
using SharedDataModels.Abstractions.Gear.Weapons.HeavyBowGuns;
using SharedDataModels.Abstractions.Gear.Weapons.HuntingHorns;
using SharedDataModels.Abstractions.Gear.Weapons.insectGlaives;
using SharedDataModels.Abstractions.Gear.Weapons.Lances;
using SharedDataModels.Abstractions.Gear.Weapons.LightBowGuns;
using SharedDataModels.Abstractions.Gear.Weapons.LongSwords;
using SharedDataModels.Abstractions.Gear.Weapons.SwitchAxes;
using SharedDataModels.Abstractions.Gear.Weapons.SwordAndShields;

namespace Wpf.Design.Weapons;

public class WeaponTemplateSelector : DataTemplateSelector
{
  public DataTemplate BowTemplate {get; set; }
  public DataTemplate ChargeBladeTemplate {get; set; }
  public DataTemplate DualBladesTemplate {get; set; }
  public DataTemplate GreatSwordTemplate {get; set; }
  public DataTemplate GunlanceTemplate {get; set; }
  public DataTemplate HammerTemplate {get; set; }
  public DataTemplate HeavyBowgunTemplate {get; set; }
  public DataTemplate HuntingHornTemplate {get; set; }
  public DataTemplate InsectGlaiveTemplate {get; set; }
  public DataTemplate LanceTemplate {get; set; }
  public DataTemplate LightBowgunTemplate {get; set; }
  public DataTemplate LongSwordTemplate {get; set; }
  public DataTemplate SwitchAxeTemplate {get; set; }
  public DataTemplate SwordAndShieldTemplate {get; set; }


  public override DataTemplate SelectTemplate(object item, DependencyObject container)
  {
    if (item == null)
      return base.SelectTemplate(item, container);

    return item switch
    {
      IBow => BowTemplate,
      IChargeBlade => ChargeBladeTemplate,
      IDualBlades => DualBladesTemplate,
      IGreatSword => GreatSwordTemplate,
      IGunlance => GunlanceTemplate,
      IHammer => HammerTemplate,
      IHeavyBowGun => HeavyBowgunTemplate,
      IHuntingHorn => HuntingHornTemplate,
      IInsectGlaive => InsectGlaiveTemplate,
      ILance => LanceTemplate,
      ILightBowGun => LightBowgunTemplate,
      ILongsword => LongSwordTemplate,
      ISwitchAxe => SwitchAxeTemplate,
      ISwordAndShield => SwordAndShieldTemplate,

      _ => throw new NotImplementedException()
      
    };
  }
}