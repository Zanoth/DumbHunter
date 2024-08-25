﻿using System.Text.Json;
using System.Text.Json.Serialization;
using Serialization.Abstraction.Converters.Utils;
using SharedDataModels.Abstractions.Gear.Weapons.Gunlances;

namespace Serialization.Abstraction.Converters.Weapons;

public class GunlanceConverter : JsonConverter<Gunlance>, IJsonConverter
{
  public override Gunlance? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var baseInfo = root.ReadBaseInfo();

    var sharpness = root.ReadSharpness();
    var shelling = root.ReadShelling();

    var gunlance = new Gunlance(baseInfo.id,
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
      sharpness,
      shelling);
    return gunlance;
  }

  public override void Write(Utf8JsonWriter writer, Gunlance gunlance, JsonSerializerOptions options)
  {
    writer.WriteBaseInfo(gunlance);
    writer.WriteSharpness(gunlance.Sharpness);
    writer.WriteShelling(gunlance.ShellingMechanic);
  }
}