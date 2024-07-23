using System.Text;
using SharedDataModels.Abstractions.Gear.Weapons.WeaponMechanics;

namespace Serialization.Abstraction.UnitTests.Converters.WeaponMechanics;

public class ChargeBladeMechanicStatsJsonConverterFixture
{
  [Fact(Skip ="A proper implementation of WeaponMechanics not yet implemented")]
  public void Read_GivenValidJson_ShouldReturnChargeBladeMechanicStats()
  {
    throw new NotImplementedException();

    //// Arrange
    //var builder = new ChargeBladeMechanicStatsTestBuilder();
    //var json = builder.BuildJson();
    //var reader = new Utf8JsonReader(Encoding.UTF8.GetBytes(json));
    //var converter = new ChargeBladeMechanicStatsJsonConverter();

    //// Act
    //var chargeBladeMechanicStats = converter.Read(ref reader, typeof(ChargeBladeMechanicStats), new JsonSerializerOptions());

    //// Assert
    //var expectedChargeBladeMechanicStats = builder.Build();
    //chargeBladeMechanicStats.Should().BeEquivalentTo(expectedChargeBladeMechanicStats);
  }

  [Fact(Skip ="A proper implementation of WeaponMechanics not yet implemented")]
  public void Write_GivenChargeBladeMechanicStats_ShouldWriteJson()
  {
    throw new NotImplementedException();

    //// Arrange
    //var builder = new ChargeBladeMechanicStatsTestBuilder();
    //var chargeBladeMechanicStats = builder.Build();
    //var options = new JsonSerializerOptions { Converters = { new ChargeBladeMechanicStatsJsonConverter() } };
    //var stream = new MemoryStream();
    //var writer = new Utf8JsonWriter(stream);

    //// Act
    //JsonSerializer.Serialize(writer, chargeBladeMechanicStats, options);
    //writer.Flush();
    //var json = Encoding.UTF8.GetString(stream.ToArray());

    //// Assert
    //var expectedJson = builder.BuildJson();
    //json.Should().Be(expectedJson);
  }

  #region Helper Class(es)

  private class ChargeBladeMechanicStatsTestBuilder
  {
    private PhialType _phialType = PhialType.Undefined;
    private int _phialPower = 100;

    public ChargeBladeMechanicStatsTestBuilder WithPhialType(PhialType phialType)
    {
      _phialType = phialType;
      return this;
    }

    public ChargeBladeMechanicStatsTestBuilder WithPhialPower(int phialPower)
    {
      _phialPower = phialPower;
      return this;
    }

    public PhialMechanics Build()
    {
      return new PhialMechanics(_phialType, _phialPower);
    }

    public string BuildJson()
    {
      //TODO: Replace with actual json
      var chargeBladeMechanicStats = Build();
      return JsonSerializer.Serialize(chargeBladeMechanicStats, new JsonSerializerOptions { Converters = { new PhialMechanicsJsonConverter() } });
    }
  }

  #endregion
}