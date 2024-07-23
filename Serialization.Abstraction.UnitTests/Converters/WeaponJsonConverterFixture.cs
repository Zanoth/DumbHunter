using System.Drawing;
using System.Text;
using SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;

namespace Serialization.Abstraction.UnitTests.Converters;

public class WeaponJsonConverterFixture
{
  [Fact]
  public void Read_GivenValidJson_ShouldReturnWeapon()
  {
    // Arrange
    var builder = new WeaponTestBuilder();

    var expectedWeapon = builder.BuildWeapon();
    var weaponJson = builder.BuildJson();

    var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(weaponJson));

    var weaponConverter = new WeaponJsonConverter();
    var sharpnessConverter = new SharpnessJsonConverter();

    var options = new JsonSerializerOptions { Converters = { weaponConverter, sharpnessConverter } };

    // Act
    var weapon = weaponConverter.Read(ref reader, typeof(Weapon), options);

    // Assert
    weapon.Should().BeEquivalentTo(expectedWeapon);
  }

  [Fact]
  public void Write_GivenWeapon_ShouldWriteJson()
  {
    // Arrange
    var builder = new WeaponTestBuilder();

    var weapon = builder.BuildWeapon();
    var expectedWeaponJson = builder.BuildJson();

    var weaponConverter = new WeaponJsonConverter();
    var sharpnessConverter = new SharpnessJsonConverter();

    var options = new JsonSerializerOptions { Converters = { weaponConverter, sharpnessConverter} };

    var stream = new MemoryStream();
    var writer = new Utf8JsonWriter(stream);

    // Act
    JsonSerializer.Serialize(writer, weapon, options);
    writer.Flush();
    var weaponJson = Encoding.UTF8.GetString(stream.ToArray());

    // Assert
    weaponJson.Should().Be(expectedWeaponJson);
  }
}



#region Helper Class(es)
public class WeaponTestBuilder
{
  private WeaponId _weaponId = WeaponId.New("weapon-00123");
  private string _name = "TestWeapon";
  private int _rarity = 5;
  private WeaponType _weaponType = WeaponType.Gunlance;
  private int _attack = 100;
  private int _affinity = 10;
  private int _defense = 20;
  private bool _elementHidden = false;
  private List<ElementStat> _elementalStats = new() { new ElementStat(ElementType.Fire, 30) };
  private Elderseal _elderseal = Elderseal.Low;
  private List<DecoSlot> _decoSlots = new() { new DecoSlot(1, DecorationId.New("decoration-456")) };
  private IWeaponMechanic _weaponMechanic = new ShellingMechanics(ShellingType.Normal, 1);
  private Sharpness _sharpness = new(false,
    new List<SharpnessSection>
    {
      new(Color.Red, 30),
      new(Color.Orange, 60),
      new(Color.Yellow, 20),
      new(Color.Green,140),
      new(Color.Blue,70),
      new(Color.White,35),
      new(Color.Purple,12)
    }
  );


  public WeaponTestBuilder WithWeaponId(string weaponId)
  {
    _weaponId = WeaponId.New(weaponId);
    return this;
  }

  public WeaponTestBuilder WithName(string name)
  {
    _name = name;
    return this;
  }

  public WeaponTestBuilder WithRarity(int rarity)
  {
    _rarity = rarity;
    return this;
  }

  public WeaponTestBuilder WithWeaponType(WeaponType weaponType)
  {
    _weaponType = weaponType;
    return this;
  }

  public WeaponTestBuilder WithAttack(int attack)
  {
    _attack = attack;
    return this;
  }

  public WeaponTestBuilder WithAffinity(int affinity)
  {
    _affinity = affinity;
    return this;
  }

  public WeaponTestBuilder WithDefense(int defense)
  {
    _defense = defense;
    return this;
  }

  public WeaponTestBuilder WithElementHidden(bool elementHidden)
  {
    _elementHidden = elementHidden;
    return this;
  }

  public WeaponTestBuilder WithElementalStats(List<ElementStat> elementalStats)
  {
    _elementalStats = elementalStats;
    return this;
  }

  public WeaponTestBuilder WithElderseal(Elderseal elderseal)
  {
    _elderseal = elderseal;
    return this;
  }

  public WeaponTestBuilder WithDecoSlots(List<DecoSlot> decoSlots)
  {
    _decoSlots = decoSlots;
    return this;
  }

  public WeaponTestBuilder WithWeaponMechanicStats(IWeaponMechanic weaponMechanic)
  {
    _weaponMechanic = weaponMechanic;
    return this;
  }

  public WeaponTestBuilder WithSharpness(Sharpness sharpness)
  {
    _sharpness = sharpness;
    return this;
  }

  public Weapon BuildWeapon()
  {
    return new Weapon(
        _weaponId,
        _name,
        _rarity,
        _weaponType,
        _attack,
        _affinity,
        _defense,
        _elementHidden,
        _elementalStats,
        _elderseal,
        _decoSlots,
        null,   //TODO: Fix this - left out for now for simplicity
        _weaponMechanic,
        _sharpness
    );
  }

  public string BuildJson()
  {
    var elementalStatsJson = string.Join(", ", _elementalStats.Select(elementalStat => $@"
          {{
            ""ElementType"": ""{elementalStat.ElementType}"",
            ""ElementAttack"": {elementalStat.ElementAttack}
          }}"));

    var decoSlotsJson = string.Join(", ", _decoSlots.Select(decoSlot => $@"
          {{
            ""SlotLevel"": {decoSlot.SlotLevel},
            ""AssignedDecorationId"": ""{decoSlot.AssignedDecorationId.Id}""
          }}"));

    var sharpnessSectionsJson = string.Join(", ", _sharpness.Sections.Select(section => $@"
          {{
            ""Color"": ""{section.Color.Name}"",
            ""Value"": {section.Value}
          }}"));

    var kinsectBonus = "";
    var phialType = "";
    var phialPower = 0;
    var shellingType = "";
    var shellingLevel = 0;
    var ammoConfig = "";

    if (_weaponMechanic is ShellingMechanics gunlanceStats)
    {
      shellingType = gunlanceStats.ShellingType.ToString();
      shellingLevel = gunlanceStats.ShellingLevel;
    }
    else if (_weaponMechanic is PhialMechanics chargeBladeStats)
    {
      phialType = chargeBladeStats.PhialType.ToString();
      phialPower = chargeBladeStats.PhialPower;
    }
    //TODO: Add other weapon types
    //else if (_weaponMechanicStats is InsectGlaiveMechanicStats insectGlaiveStats)
    //{
    //  kinsectBonus = insectGlaiveStats.KinsectBonus;
    //}
    //else if (_weaponMechanicStats is BowMechanicStats bowStats)
    //{
    //  ammoConfig = bowStats.AmmoConfig;
    //}

    //TODO: weaponMechanicStats removed from json for now
    //""WeaponMechanicStats"": {{
    //  ""KinsectBonus"": ""{kinsectBonus}"",
    //  ""Phial"": ""{phialType}"",
    //  ""PhialPower"": {phialPower},
    //  ""Shelling"": ""{shellingType}"",
    //  ""ShellingLevel"": {shellingLevel},
    //  ""AmmoConfig"": ""{ammoConfig}""
    //}},

    //TODO: Add skill
    var json = $@"
      {{
        ""{nameof(Weapon.WeaponId)}"": ""{_weaponId.Id}"",
        ""{nameof(Weapon.Name)}"": ""{_name}"",
        ""{nameof(Weapon.Rarity)}"": {_rarity},
        ""{nameof(Weapon.WeaponType)}"": ""{_weaponType}"",
        ""{nameof(Weapon.Attack)}"": {_attack},
        ""{nameof(Weapon.Affinity)}"": {_affinity},
        ""{nameof(Weapon.Defense)}"": {_defense},
        ""{nameof(Weapon.ElementHidden)}"": {_elementHidden.ToString().ToLower()},
        ""{nameof(Weapon.ElementalStats)}"": [{elementalStatsJson}],
        ""{nameof(Weapon.Elderseal)}"": ""{_elderseal}"",
        ""{nameof(Weapon.DecoSlots)}"": [{decoSlotsJson}],
        ""{nameof(Weapon.Sharpness)}"": {{
          ""{nameof(Sharpness.IsMaxed)}"": {_sharpness.IsMaxed.ToString().ToLower()},
          ""{nameof(Sharpness.Sections)}"": [{sharpnessSectionsJson}]
        }}
      }}";

    json = TestingUtilities.MinifyJson(json);

    return json;
  }
}
#endregion