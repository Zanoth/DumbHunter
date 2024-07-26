using SharedDataModels.Abstractions.Locations;
using SharedDataModels.Abstractions.Quests;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Serialization.Abstraction.Converters;

public class QuestJsonConverter : JsonConverter<Quest>, IJsonConverter
{
  public override Quest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
  {
    using JsonDocument doc = JsonDocument.ParseValue(ref reader);
    var root = doc.RootElement;

    var id = QuestId.New(root.GetProperty(nameof(Quest.QuestId)).GetString());
    var name = root.GetProperty(nameof(Quest.Name)).GetString();

    var category = (QuestCategory)Enum.Parse(typeof(QuestCategory), root.GetProperty(nameof(Quest.Category)).GetString());
    var rank = (QuestRank)Enum.Parse(typeof(QuestRank), root.GetProperty(nameof(Quest.Rank)).GetString());
    var stars = root.GetProperty(nameof(Quest.Stars)).GetInt32();

    var location = LocationId.New(root.GetProperty(nameof(Quest.LocationId)).GetString());

    var monsterJson = root.GetProperty(nameof(Quest.Monsters)).EnumerateArray();
    var monsters = GetTargetMonsters(monsterJson);

    var objectivesJson = root.GetProperty(nameof(Quest.Objectives)).EnumerateArray();
    var objectives = new List<QuestObjective?>();
    foreach (var objectiveJson in objectivesJson)
    {
      var objectiveText = objectiveJson.GetRawText();
      var questObjective = JsonSerializer.Deserialize<QuestObjective>(objectiveText, options);
      objectives.Add(questObjective);
    }

    var rewardsRawText = root.GetProperty(nameof(Quest.Rewards)).GetRawText();
    var rewards = JsonSerializer.Deserialize<QuestRewards>(rewardsRawText, options);

    var quest = new Quest(id, name, category, rank, stars, location, monsters, objectives, rewards);
    return quest;
  }


  // TODO: This method is repeated in #QuestObjectiveJsonConverter.cs, move it to a shared location
  private static List<EntityTracker> GetTargetMonsters(JsonElement.ArrayEnumerator monsterJsonArray)
  {
    var targetMonsters = new List<EntityTracker>();
    foreach (var monsterJson in monsterJsonArray)
    {
      var monsterId = MonsterId.New(monsterJson.GetProperty(nameof(Monster.MonsterId)).GetString());
      var count = monsterJson.GetProperty("Quantity").GetInt32();

      targetMonsters.Add(new EntityTracker(monsterId, count));
    }

    return targetMonsters;
  }


  public override void Write(Utf8JsonWriter writer, Quest quest, JsonSerializerOptions options)
  {
    //TODO: Implement write method

    //writer.WriteStartObject();

    //writer.WriteString(nameof(Quest.QuestId), quest.QuestId.Id);
    //writer.WriteString(nameof(Quest.Name), quest.Name);
    //writer.WriteString(nameof(Quest.Category), quest.Category.ToString());
    //writer.WriteString(nameof(Quest.Rank), quest.Rank.ToString());
    //writer.WriteNumber(nameof(Quest.Stars), quest.Stars);
    //writer.WriteString(nameof(Quest.LocationId), quest.LocationId.Id);

    //writer.WriteRawValue(JsonSerializer.Serialize(quest.Rewards, options));
    //writer.WriteRawValue(JsonSerializer.Serialize(quest.Objective, options));

    ////TODO: Implement serialization of the Monster object

    //writer.WriteEndObject();

    throw new NotImplementedException();
  }
}

//Refactor: This method is repeated in #QuestObjectiveJsonConverter.cs, move it to a shared location