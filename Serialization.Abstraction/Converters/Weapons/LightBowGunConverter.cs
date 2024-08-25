using System.Text.Json;
using System.Text.Json.Serialization;
using Serialization.Abstraction.Converters.Utils;
using SharedDataModels.Abstractions.Gear.Weapons.LightBowGuns;

namespace Serialization.Abstraction.Converters.Weapons;

public class LightBowGunConverter : JsonConverter<LightBowGun>, IJsonConverter
{
  public override LightBowGun? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var baseInfo = root.ReadBaseInfo();
    var ammo = root.ReadAmmo();

    var lightBowGun = new LightBowGun(baseInfo.id,
      baseInfo.name,
      baseInfo.rarity,
      baseInfo.attack,
      baseInfo.affinity,
      baseInfo.defense,
      baseInfo.elementHidden,
      baseInfo.elementalStats,
      baseInfo.elderseal,
      baseInfo.decorationSlots,
      baseInfo.skills,
      ammo);
    return lightBowGun;
  }

  public override void Write(Utf8JsonWriter writer, LightBowGun lightBowGun, JsonSerializerOptions options)
  {
    writer.WriteBaseInfo(lightBowGun);
    writer.WriteAmmo(lightBowGun.BulletMechanic);
  }
}