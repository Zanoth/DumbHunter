using SharedDataModels.Abstractions.Quests;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class QuestObjectiveJsonConverter : JsonConverter<QuestObjective>, IJsonConverter
{
  public override QuestObjective Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var type = Enum.Parse<QuestType>(root.GetProperty(nameof(QuestObjective.QuestType)).ToString());
    var requirementsJson = root.GetProperty(nameof(QuestObjective.Requirements)).EnumerateArray();
    var questRequirements = new List<EntityTracker>();
    foreach (var requirementJson in requirementsJson)
    {

      IStrongId id = type switch
      {
        QuestType.Assignment or QuestType.Hunt or QuestType.Capture =>
          MonsterId.New(requirementJson.GetProperty(nameof(Monster.MonsterId)).GetString()),
          
        QuestType.Delivery => 
           ItemId.New(requirementJson.GetProperty(nameof(Item.ItemId)).GetString()),
          
        _ => throw new ArgumentException($"Could not convert {nameof(type)} to a class implementing {nameof(QuestObjective)}"),
      };


      var requiredCount = requirementJson.GetProperty("Quantity").GetInt32();
      var requirement = new EntityTracker(id, requiredCount);
      questRequirements.Add(requirement);
    }

    var questObjective = new QuestObjective(type, questRequirements);
    return questObjective;
  }



  public override void Write(Utf8JsonWriter writer, QuestObjective questObjective, JsonSerializerOptions options)
  {
    writer.WriteString(nameof(QuestObjective.QuestType), questObjective.QuestType.ToString());
    writer.WriteStartArray(nameof(QuestObjective.Requirements));
    foreach (var requirement in questObjective.Requirements)
    {
      writer.WriteStartObject();
      switch (questObjective.QuestType)
      {
        case QuestType.Hunt or QuestType.Capture:
          writer.WriteString(nameof(Monster.MonsterId), requirement.Id.Id);
          break;
        case QuestType.Delivery:
          writer.WriteString(nameof(Item.ItemId), requirement.Id.Id);
          break;
        default:
          throw new ArgumentException($"Could not convert {nameof(questObjective)} to a class implementing {nameof(QuestObjective)}");
      }

      writer.WriteNumber("Quantity", requirement.Count);
      writer.WriteEndObject();
    }
    writer.WriteEndArray();
  }
}