using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class ItemJsonConverter : JsonConverter<Item>, IJsonConverter
{
  public override Item Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var id = ItemId.New(root.GetProperty(nameof(Item.ItemId)).GetString());
    var name = root.GetProperty(nameof(Item.Name)).GetString();
    var category = root.GetProperty(nameof(Item.Category)).GetString();
    var subcategory = root.GetProperty(nameof(Item.Subcategory)).GetString();
    var rarity = root.GetProperty(nameof(Item.Rarity)).GetInt32();
    var buyPrice = root.GetProperty(nameof(Item.BuyPrice)).GetInt32();
    var sellPrice = root.GetProperty(nameof(Item.SellPrice)).GetInt32();
    var carryLimit = root.GetProperty(nameof(Item.CarryLimit)).GetInt32();
    var points = root.GetProperty(nameof(Item.Points)).GetInt32();
    var iconName = root.GetProperty(nameof(Item.IconName)).GetString();
    var iconColor = Color.FromName(root.GetProperty(nameof(Item.IconColor)).GetString());


    var item = new Item(id, name, category, subcategory, rarity, buyPrice, sellPrice, carryLimit, points, iconName, iconColor);
    return item;
  }

  public override void Write(Utf8JsonWriter writer, Item item, JsonSerializerOptions options)
  {

    writer.WriteStartObject();
    writer.WriteString(nameof(Item.ItemId), item.ItemId.Id);
    writer.WriteString(nameof(Item.Name), item.Name);
    writer.WriteString(nameof(Item.Category), item.Category);
    writer.WriteString(nameof(Item.Subcategory), item.Subcategory);
    writer.WriteNumber(nameof(Item.Rarity), item.Rarity);
    writer.WriteNumber(nameof(Item.BuyPrice), item.BuyPrice);
    writer.WriteNumber(nameof(Item.SellPrice), item.SellPrice);
    writer.WriteNumber(nameof(Item.CarryLimit), item.CarryLimit);
    writer.WriteNumber(nameof(Item.Points), item.Points);
    writer.WriteString(nameof(Item.IconName), item.IconName);
    writer.WriteString(nameof(Item.IconColor), item.IconColor.Name);

    writer.WriteEndObject();

  }
}