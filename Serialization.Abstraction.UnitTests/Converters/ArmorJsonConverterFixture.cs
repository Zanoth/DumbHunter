//using System.Text;

//namespace Serialization.Abstraction.UnitTests.Converters;

//public class ArmorJsonConverterFixture
//{
//  [Fact]
//  public void Read_GivenValidJson_ShouldReturnArmor()
//  {
//    // Arrange
//    var builder = new ArmorTestBuilder();
//    var json = builder.BuildJson();
//    var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));

//    var armorConverter = new ArmorJsonConverter();
//    var skillConverter = new GearSkillJsonConverter();
//    var options = new JsonSerializerOptions { Converters = { armorConverter, skillConverter} };

//    // Act
//    var armor = armorConverter.Read(ref reader, typeof(Armor), options);

//    // Assert
//    var expectedArmor = builder.BuildArmor();
//    armor.Should().BeEquivalentTo(expectedArmor);
//  }

//  [Fact]
//  public void Write_GivenArmor_ShouldWriteJson()
//  {
//    // Arrange
//    var builder = new ArmorTestBuilder();
//    var armor = builder.BuildArmor();

//    var armorConverter = new ArmorJsonConverter();
//    var skillConverter = new GearSkillJsonConverter();
//    var options = new JsonSerializerOptions { Converters = { armorConverter, skillConverter} };

//    var stream = new MemoryStream();
//    var writer = new Utf8JsonWriter(stream);

//    // Act
//    JsonSerializer.Serialize(writer, armor, options);
//    writer.Flush();
//    var json = Encoding.UTF8.GetString(stream.ToArray());

//    // Assert
//    var expectedJson = builder.BuildJson();
//    json.Should().Be(expectedJson);
//  }

//  #region Helper Class(es)

//  private class ArmorTestBuilder
//  {
//    private string _id = "armor-0123";
//    private string _name = "Dragon Armor";
//    private int _rarity = 5;
//    private ArmorType _armorType = ArmorType.Undefined;
//    private List<GearSkill> _skills = new List<GearSkill>
//    {
//      new GearSkill(SkillId.New("skill-001"), 1),
//      new GearSkill(SkillId.New("skill-002"), 2)
//    };
//    private List<DecoSlot> _decoSlots = new List<DecoSlot>
//    {
//      new DecoSlot(1, DecorationId.New("deco-0001")),
//      new DecoSlot(2, DecorationId.New("deco-0002"))
//    };
//    private DefenseStats _defenseStats = new DefenseStats(100, 200, 250, 10, 20, 30, 40, 50);

//    public ArmorTestBuilder WithId(string id)
//    {
//      _id = id;
//      return this;
//    }

//    public ArmorTestBuilder WithName(string name)
//    {
//      _name = name;
//      return this;
//    }

//    public ArmorTestBuilder WithRarity(int rarity)
//    {
//      _rarity = rarity;
//      return this;
//    }

//    public ArmorTestBuilder WithArmorType(ArmorType armorType)
//    {
//      _armorType = armorType;
//      return this;
//    }

//    public ArmorTestBuilder WithSkills(List<GearSkill> skills)
//    {
//      _skills = skills;
//      return this;
//    }

//    public ArmorTestBuilder WithDecoSlots(List<DecoSlot> decoSlots)
//    {
//      _decoSlots = decoSlots;
//      return this;
//    }

//    public ArmorTestBuilder WithDefenseStats(DefenseStats defenseStats)
//    {
//      _defenseStats = defenseStats;
//      return this;
//    }

//    public Armor BuildArmor()
//    {
//      return new Armor(ArmorId.New(_id), _name, _rarity, _armorType, _skills, _decoSlots, _defenseStats);
//    }

//    public string BuildJson()
//    {
//      var skills = string.Join(", ", _skills.Select(skill => $@"
//        {{
//          ""{nameof(GearSkill.SkillId)}"": ""{skill.SkillId.Id}"",
//          ""{nameof(GearSkill.SkillLevel)}"": {skill.SkillLevel}
//        }}"));

//      var decos = string.Join(", ", _decoSlots.Select(deco => $@"
//        {{
//          ""{nameof(DecoSlot.SlotLevel)}"": {deco.SlotLevel},
//          ""{nameof(DecoSlot.AssignedDecorationId)}"": ""{deco.AssignedDecorationId.Id}""
//        }}"));


//      var json = $@"
//      {{
//        ""{nameof(Armor.ArmorId)}"": ""{_id}"",        
//        ""{nameof(Armor.Name)}"": ""{_name}"",
//        ""{nameof(Armor.Rarity)}"": {_rarity},
//        ""{nameof(Armor.ArmorType)}"": ""{_armorType}"",
//        ""{nameof(Armor.Skills)}"": [{skills}],
//        ""{nameof(Armor.DecoSlots)}"": [{decos}],
//        ""{nameof(Armor.DefenseStats)}"": {{
//          ""{nameof(DefenseStats.BaseDef)}"": {_defenseStats.BaseDef},
//          ""{nameof(DefenseStats.MaxDef)}"": {_defenseStats.MaxDef},
//          ""{nameof(DefenseStats.ArgumentedMaxDef)}"": {_defenseStats.ArgumentedMaxDef},
//          ""{nameof(DefenseStats.FireDef)}"": {_defenseStats.FireDef},
//          ""{nameof(DefenseStats.WaterDef)}"": {_defenseStats.WaterDef},
//          ""{nameof(DefenseStats.ThunderDef)}"": {_defenseStats.ThunderDef},
//          ""{nameof(DefenseStats.IceDef)}"": {_defenseStats.IceDef},
//          ""{nameof(DefenseStats.DragonDef)}"": {_defenseStats.DragonDef}}}
//      }}";

//      json = TestingUtilities.MinifyJson(json);
//      return json;
//    }
//  }

//  #endregion
//}