//using System.Runtime.Serialization;
//using SharedDataModels.Abstractions.Gear.Weapons.Mechanics;
//using SharedDataModels.Abstractions.Gear.Weapons.Stats;

//namespace Serialization.Abstraction.UnitTests.Converters.WeaponMechanics;

//public class GunLanceMechanicStatsJsonConverterFixture
//{
//  [Fact(Skip ="A proper implementation of WeaponMechanics not yet implemented")]
//  public void Read_GivenValidJson_ShouldReturnGunLanceMechanicStats()
//  {
//    throw new NotImplementedException();
//    //// Arrange
//    //var builder = new GunLanceMechanicStatsTestBuilder();
//    //var json = builder.BuildJson();
//    //var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
//    //var converter = new GunLanceMechanicStatsJsonConverter();

//    //// Act
//    //var gunLanceMechanicStats = converter.Read(ref reader, typeof(GunLanceMechanicStats), new JsonSerializerOptions());

//    //// Assert
//    //var expectedGunLanceMechanicStats = builder.Build();
//    //gunLanceMechanicStats.Should().BeEquivalentTo(expectedGunLanceMechanicStats);
//  }

//  [Fact(Skip ="A proper implementation of WeaponMechanics not yet implemented")]
//  public void Write_GivenGunLanceMechanicStats_ShouldWriteJson()
//  {
//    throw new NotImplementedException();
//    //// Arrange
//    //var builder = new GunLanceMechanicStatsTestBuilder();
//    //var gunLanceMechanicStats = builder.Build();
//    //var options = new JsonSerializerOptions { Converters = { new GunLanceMechanicStatsJsonConverter() } };
//    //var stream = new MemoryStream();
//    //var writer = new Utf8JsonWriter(stream);

//    //// Act
//    //JsonSerializer.Serialize(writer, gunLanceMechanicStats, options);
//    //writer.Flush();
//    //var json = Encoding.UTF8.GetString(stream.ToArray());

//    //// Assert
//    //var expectedJson = builder.BuildJson();
//    //json.Should().Be(expectedJson);
//  }

//  #region Helper Class(es)

//  private class GunLanceMechanicStatsTestBuilder
//  {
//    private ShellingType _shellingType = ShellingType.Normal;
//    private int _shellingLevel = 3;

//    public GunLanceMechanicStatsTestBuilder WithShellingType(ShellingType shellingType)
//    {
//      _shellingType = shellingType;
//      return this;
//    }

//    public GunLanceMechanicStatsTestBuilder WithShellingLevel(int shellingLevel)
//    {
//      _shellingLevel = shellingLevel;
//      return this;
//    }

//    public ShellingMechanics Build()
//    {
//      return new ShellingMechanics(_shellingType, _shellingLevel);
//    }

//    public string BuildJson()
//    {
//      //TODO: Replace with actual json

//      var gunLanceMechanicStats = Build();
//      return JsonSerializer.Serialize(gunLanceMechanicStats, new JsonSerializerOptions { Converters = { new ShellingMechanicsJsonConverter() } });
//    }
//  }

//  #endregion
//}