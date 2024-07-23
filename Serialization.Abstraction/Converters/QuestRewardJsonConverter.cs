using SharedDataModels.Abstractions.Quests;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class QuestRewardsJsonConverter : JsonConverter<QuestRewards>, IJsonConverter
{
  public override QuestRewards Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var zenny = root.GetProperty(nameof(QuestRewards.Zenny)).GetInt32();

    var itemsJson = root.GetProperty(nameof(QuestRewards.Items)).EnumerateArray();
    var itemIdPropertyName = nameof(ItemId);
    var stackPropertyName = nameof(LootDetails.Stack);
    var percentagePropertyName = nameof(LootDetails.Percentage);
    var items = new List<LootDetails>();
    foreach (var item in itemsJson)
    {
      var idStr = item.GetProperty(itemIdPropertyName).ToString();
      var itemId = ItemId.New(idStr);

      var stack = item.GetProperty(stackPropertyName).GetInt32();
      var percentage = item.GetProperty(percentagePropertyName).GetInt32();
      var lootDetails = new LootDetails(itemId, stack, percentage);
      items.Add( lootDetails);
    }

    var quest = new QuestRewards(zenny, items);
    return quest;
  }

  public override void Write(Utf8JsonWriter writer, QuestRewards quest, JsonSerializerOptions options)
  {
    writer.WriteStartObject("Rewards");

    writer.WriteNumber(nameof(QuestRewards.Zenny), quest.Zenny);

    writer.WriteStartArray(nameof(QuestRewards.Items));

    foreach (var item in quest.Items)
    {
      writer.WriteStartObject();
      writer.WriteString(nameof(ItemId), item.ItemId.Id);
      writer.WriteNumber(nameof(LootDetails.Stack), item.Stack);
      writer.WriteNumber(nameof(LootDetails.Percentage), item.Percentage);
      writer.WriteEndObject();
    }

    writer.WriteEndArray();

    writer.WriteEndObject();
  }
}