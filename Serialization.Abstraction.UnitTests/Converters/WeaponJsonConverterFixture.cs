//using System.Drawing;
//using System.Text;
//using SharedDataModels.Abstractions.Gear.Weapons.Stats;
//using SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;

//namespace Serialization.Abstraction.UnitTests.Converters;

//public class WeaponJsonConverterFixture
//{
//  [Fact]
//  public void Read_GivenValidJson_ShouldReturnWeapon()
//  {
//    // Arrange
//    var builder = new WeaponTestBuilder();

//    var expectedWeapon = builder.BuildWeapon();
//    var weaponJson = builder.BuildJson();

//    var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(weaponJson));

//    var weaponConverter = new WeaponJsonConverter();
//    var sharpnessConverter = new SharpnessJsonConverter();

//    var options = new JsonSerializerOptions { Converters = { weaponConverter, sharpnessConverter } };

//    // Act
//    var weapon = weaponConverter.Read(ref reader, typeof(WeaponBase), options);

//    // Assert
//    weapon.Should().BeEquivalentTo(expectedWeapon);
//  }

//  [Fact]
//  public void Write_GivenWeapon_ShouldWriteJson()
//  {
//    // Arrange
//    var builder = new WeaponTestBuilder();

//    var weapon = builder.BuildWeapon();
//    var expectedWeaponJson = builder.BuildJson();

//    var weaponConverter = new WeaponJsonConverter();
//    var sharpnessConverter = new SharpnessJsonConverter();

//    var options = new JsonSerializerOptions { Converters = { weaponConverter, sharpnessConverter} };

//    var stream = new MemoryStream();
//    var writer = new Utf8JsonWriter(stream);

//    // Act
//    JsonSerializer.Serialize(writer, weapon, options);
//    writer.Flush();
//    var weaponJson = Encoding.UTF8.GetString(stream.ToArray());

//    // Assert
//    weaponJson.Should().Be(expectedWeaponJson);
//  }
//}



//#region Helper Class(es)
//public class WeaponTestBuilder
//{
//  private WeaponId _weaponId = WeaponId.New("weapon-00123");
//  private string _name = "TestWeapon";
//  private int _rarity = 5;
//  private WeaponType _weaponType = WeaponType.Gunlance;
//  private int _attack = 100;
//  private int _affinity = 10;
//  private int _defense = 20;
//  private bool _elementHidden = false;
//  private Elderseal _elderseal = Elderseal.Low;


//  private List<ElementStat> _elementalStats = new() 
//  { 
//    new ElementStat(ElementType.Fire, 30) 
//  };

//  private List<GearSkill> _gearSkills = new() 
//  { 
//    new GearSkill
//    (
//      new Skill(SkillId.New("skill-123"),
//        "Dummy Skill",
//        Color.Red,
//        0,
//        SkillId.New("skill-321"),
//        new List<SkillLevel>
//        {
//          new SkillLevel(0, "level 0"),
//          new SkillLevel(1, "level 1")
//        }),
//      1) 
//  };

//  private List<DecorationSlot> _decoSlots = new() 
//  { 
//    new DecorationSlot(1, DecorationId.New("decoration-456")) 
//  };

//  private IWeaponMechanic _weaponMechanic = new ShellingMechanics(ShellingType.Normal, 1);
//  private Sharpness _sharpness = new(false,
//    new List<SharpnessSection>
//    {
//      new(Color.Red, 30),
//      new(Color.Orange, 60),
//      new(Color.Yellow, 20),
//      new(Color.Green,140),
//      new(Color.Blue,70),
//      new(Color.White,35),
//      new(Color.Purple,12)
//    }
//  );


//  public WeaponTestBuilder WithWeaponId(string weaponId)
//  {
//    _weaponId = WeaponId.New(weaponId);
//    return this;
//  }

//  public WeaponTestBuilder WithName(string name)
//  {
//    _name = name;
//    return this;
//  }

//  public WeaponTestBuilder WithRarity(int rarity)
//  {
//    _rarity = rarity;
//    return this;
//  }

//  public WeaponTestBuilder WithWeaponType(WeaponType weaponType)
//  {
//    _weaponType = weaponType;
//    return this;
//  }

//  public WeaponTestBuilder WithAttack(int attack)
//  {
//    _attack = attack;
//    return this;
//  }

//  public WeaponTestBuilder WithAffinity(int affinity)
//  {
//    _affinity = affinity;
//    return this;
//  }

//  public WeaponTestBuilder WithDefense(int defense)
//  {
//    _defense = defense;
//    return this;
//  }

//  public WeaponTestBuilder WithElementHidden(bool elementHidden)
//  {
//    _elementHidden = elementHidden;
//    return this;
//  }

//  public WeaponTestBuilder WithElementalStats(List<ElementStat> elementalStats)
//  {
//    _elementalStats = elementalStats;
//    return this;
//  }

//  public WeaponTestBuilder WithElderseal(Elderseal elderseal)
//  {
//    _elderseal = elderseal;
//    return this;
//  }

//  public WeaponTestBuilder WithDecoSlots(List<DecorationSlot> decoSlots)
//  {
//    _decoSlots = decoSlots;
//    return this;
//  }

//  public WeaponTestBuilder WithWeaponMechanicStats(IWeaponMechanic weaponMechanic)
//  {
//    _weaponMechanic = weaponMechanic;
//    return this;
//  }

//  public WeaponTestBuilder WithSharpness(Sharpness sharpness)
//  {
//    _sharpness = sharpness;
//    return this;
//  }

//  public WeaponBase BuildWeapon()
//  {
//    return new WeaponBase(
//        _weaponId,
//        _name,
//        _rarity,
//        _weaponType,
//        _attack,
//        _affinity,
//        _defense,
//        _elementHidden,
//        _elementalStats,
//        _elderseal,
//        _decoSlots,
//        _gearSkills,
//        _weaponMechanic,
//        _sharpness
//    );
//  }

//  public string BuildJson()
//  {
//    var elementalStatsJson = string.Join(", ", _elementalStats.Select(elementalStat => $@"
//          {{
//            ""ElementType"": ""{elementalStat.ElementType}"",
//            ""ElementAttack"": {elementalStat.ElementAttack}
//          }}"));

//    var decoSlotsJson = string.Join(", ", _decoSlots.Select(decoSlot => $@"
//          {{
//            ""SlotLevel"": {decoSlot.SlotLevel},
//            ""AssignedDecorationId"": ""{decoSlot.AssignedDecorationId.Id}""
//          }}"));

//    var sharpnessSectionsJson = string.Join(", ", _sharpness.Sections.Select(section => $@"
//          {{
//            ""Color"": ""{section.Color.Name}"",
//            ""Value"": {section.Value}
//          }}"));

//    var skillsJson = string.Join(", ", _gearSkills.Select(gearSkill => $@"
//          {{
//            ""SkillId"": ""{gearSkill.Skill.SkillId.Id}"",
//            ""SkillLevel"": {gearSkill.SkillLevel}
//          }}"));

//    var kinsectBonus = "";
//    var phialType = "";
//    var phialPower = 0;
//    var shellingType = "";
//    var shellingLevel = 0;
//    var ammoConfig = "";

//    var weaponMechanicStr = "";

//    if (_weaponMechanic is ShellingMechanics gunlanceStats)
//    {
//      weaponMechanicStr =  $@"
//      {{
//        ""{nameof(ShellingMechanics.ShellingType)}"": ""{gunlanceStats.ShellingType.ToString()}"",
//        ""{nameof(ShellingMechanics.ShellingLevel)}"": {gunlanceStats.ShellingLevel}
//      }}";
//    }
//    else if (_weaponMechanic is PhialMechanics chargeBladeStats)
//    {
//      weaponMechanicStr =  $@"
//      {{
//        ""{nameof(PhialMechanics.PhialType)}"": ""{chargeBladeStats.PhialType.ToString()}"",
//        ""{nameof(PhialMechanics.PhialPower)}"": {chargeBladeStats.PhialPower}
//      }}";
//    }
//    else if (_weaponMechanic is KinsectMechanics kinsectMechanics)
//    {
//      weaponMechanicStr =  $@"
//      {{
//        ""{nameof(KinsectMechanics.KinsectBonusType)}"": ""{kinsectMechanics.KinsectBonusType.ToString()}""
//      }}";
//    }
//    //TODO: Add ammo config
//    //else if (_weaponMechanicStats is AmmoMechanics)
//    //{
//    //  ammoConfig = bowStats.AmmoConfig;
//    //}


//    var json = $@"
//      {{
//        ""{nameof(WeaponBase.WeaponId)}"": ""{_weaponId.Id}"",
//        ""{nameof(WeaponBase.Name)}"": ""{_name}"",
//        ""{nameof(WeaponBase.Rarity)}"": {_rarity},
//        ""{nameof(WeaponBase.WeaponType)}"": ""{_weaponType}"",
//        ""{nameof(WeaponBase.Attack)}"": {_attack},
//        ""{nameof(WeaponBase.Affinity)}"": {_affinity},
//        ""{nameof(WeaponBase.Defense)}"": {_defense},
//        ""{nameof(WeaponBase.ElementHidden)}"": {_elementHidden.ToString().ToLower()},
//        ""{nameof(WeaponBase.ElementalStats)}"": [{elementalStatsJson}],
//        ""{nameof(WeaponBase.Elderseal)}"": ""{_elderseal}"",
//        ""{nameof(WeaponBase.DecorationSlots)}"": [{decoSlotsJson}],
//        ""{nameof(WeaponBase.Sharpness)}"": {{
//          ""{nameof(Sharpness.IsMaxed)}"": {_sharpness.IsMaxed.ToString().ToLower()},
//          ""{nameof(Sharpness.Sections)}"": [{sharpnessSectionsJson}]
//        ""{nameof(WeaponBase.WeaponMechanics)}"": {weaponMechanicStr}
//        ""{nameof(WeaponBase.Skills)}"": [{skillsJson}]
//        }}
//      }}";

//    json = TestingUtilities.MinifyJson(json);

//    return json;
//  }
//}
//#endregion