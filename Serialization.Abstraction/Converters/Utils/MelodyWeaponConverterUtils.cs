using SharedDataModels.Abstractions.Gear.Weapons.Mechanics.Melodies;
using System.Text.Json;

namespace Serialization.Abstraction.Converters.Utils;

public static class MelodyWeaponConverterUtils
{
  public static MelodyMechanic ReadMelodies(this JsonElement root)
  {
    // TODO: Implement this method

    var melodyMechanic = new MelodyMechanic();
    return melodyMechanic;
  }
  public static void WriteMelodies(this Utf8JsonWriter writer, MelodyMechanic melodyMechanic)
  {
    // TODO: Implement this method
  }
}