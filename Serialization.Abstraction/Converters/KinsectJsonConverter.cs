using System.Text.Json.Serialization;
using System.Text.Json;
using SharedDataModels.Abstractions.Gear.Kinsect;

namespace Serialization.Abstraction.Converters;

public class KinsectJsonConverter : JsonConverter<Kinsect>, IJsonConverter
{
  public override Kinsect Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var recipeId = GetRecipeId(root);
    var name = GetName(root);
    var craftingType = GetRarity(root);
    var items = GetAttackType(root);
    var dustEffect = GetDustEffect(root);
    var power = GetPower(root);
    var speed = GetSpeed(root);
    var heal = GetHeal(root);

    var kinsect = new Kinsect(recipeId, name, craftingType, items, dustEffect, power, speed, heal);
    return kinsect;
  }

  public override void Write(Utf8JsonWriter writer, Kinsect kinsect, JsonSerializerOptions options)
  {
    writer.WriteStartObject();
    writer.WriteString(nameof(Kinsect.KinsectId), kinsect.KinsectId.Id);
    writer.WriteString(nameof(Kinsect.Name), kinsect.Name);
    writer.WriteNumber(nameof(Kinsect.Rarity), kinsect.Rarity);
    writer.WriteString(nameof(Kinsect.AttackType), kinsect.AttackType.ToString());
    writer.WriteString(nameof(Kinsect.DustEffect), kinsect.DustEffect.ToString());
    writer.WriteNumber(nameof(Kinsect.Power), kinsect.Power);
    writer.WriteNumber(nameof(Kinsect.Speed), kinsect.Speed);
    writer.WriteNumber(nameof(Kinsect.Heal), kinsect.Heal);
    writer.WriteEndObject();
  }

  private static KinsectId GetRecipeId(JsonElement root)
  {
    var recipeId = KinsectId.New(root.GetProperty(nameof(Kinsect.KinsectId)).GetString());
    return recipeId;
  }

  private string GetName(JsonElement root)
  {
    var name = root.GetProperty(nameof(KinsectId.Id)).GetString();
    return name;
  }

  private int GetRarity(JsonElement root)
  {
    var rarity = root.GetProperty(nameof(Kinsect.Rarity)).GetInt32();
    return rarity;
  }

  private KinsectAttackType GetAttackType(JsonElement root)
  {
    var attackTypeStr = root.GetProperty(nameof(Kinsect.AttackType)).GetString();
    var attackType = Enum.Parse<KinsectAttackType>(attackTypeStr);
    return attackType;
  }

  private KinsectDustEffect GetDustEffect(JsonElement root)
  {
    var dustEffectStr = root.GetProperty(nameof(Kinsect.DustEffect)).GetString();
    var dustEffect = Enum.Parse<KinsectDustEffect>(dustEffectStr);
    return dustEffect;
  }

  private int GetPower(JsonElement root)
  {
    var power = root.GetProperty(nameof(Kinsect.Power)).GetInt32();
    return power;
  }

  private int GetSpeed(JsonElement root)
  {
    var speed = root.GetProperty(nameof(Kinsect.Speed)).GetInt32();
    return speed;
  }

  private int GetHeal(JsonElement root)
  {
    var heal = root.GetProperty(nameof(Kinsect.Heal)).GetInt32();
    return heal;
  }
}