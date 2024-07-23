using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

//TODO: Consider using this for IEnumerable<Drop> instead of Drop
public class DropJsonConverter : JsonConverter<Drop>, IJsonConverter
{
  public override Drop Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;


    var id = ItemId.New(root.GetProperty(nameof(Item.ItemId)).ToString());
    var rank = Enum.Parse<QuestRank>(root.GetProperty(nameof(Drop.QuestRank)).ToString(), true);
    var dropCondition = DropCondition.Undefined /*Enum.Parse<DropCondition>(root.GetProperty(nameof(Drop.DropCondition)).ToString(), true)*/;

    var stack = root.GetProperty(nameof(Drop.LootDetails.Stack)).GetInt32();
    var percentage = root.GetProperty(nameof(Drop.LootDetails.Percentage)).GetInt32();

    var drop = new Drop(rank, dropCondition, new LootDetails(id, stack, percentage));
    return drop;
  }


  public override void Write(Utf8JsonWriter writer, Drop drop, JsonSerializerOptions options)
  {
    writer.WriteStartObject();
    writer.WriteString(nameof(Drop.LootDetails.ItemId), drop.LootDetails.ItemId.Id);
    writer.WriteString(nameof(Drop.DropCondition), drop.DropCondition.ToString());
    writer.WriteString(nameof(Drop.QuestRank), drop.QuestRank.ToString());
    writer.WriteNumber(nameof(Drop.LootDetails.Stack), drop.LootDetails.Stack);
    writer.WriteNumber(nameof(Drop.LootDetails.Percentage), drop.LootDetails.Percentage);
    writer.WriteEndObject();
  }
}