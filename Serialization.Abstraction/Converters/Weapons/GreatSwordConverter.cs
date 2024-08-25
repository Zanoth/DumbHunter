﻿using System.Text.Json;
using System.Text.Json.Serialization;
using Serialization.Abstraction.Converters.Utils;
using SharedDataModels.Abstractions.Gear.Weapons.GreatSwords;

namespace Serialization.Abstraction.Converters.Weapons;

public class GreatSwordConverter : JsonConverter<GreatSword>, IJsonConverter
{
  public override GreatSword? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var baseInfo = root.ReadBaseInfo();

    var sharpness = root.ReadSharpness();

    var greatSword = new GreatSword(baseInfo.id,
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
      sharpness);
    return greatSword;
  }

  public override void Write(Utf8JsonWriter writer, GreatSword greatSword, JsonSerializerOptions options)
  {
    writer.WriteBaseInfo(greatSword);
    writer.WriteSharpness(greatSword.Sharpness);
  }
}