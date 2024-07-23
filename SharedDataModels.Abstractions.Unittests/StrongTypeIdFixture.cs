using FluentAssertions;

namespace SharedDataModels.Abstractions.Unittests;

public class StrongTypeIdFixture
{
  private record TestStrongTypeId : StrongTypeId<string>
  {
    public TestStrongTypeId(string itemId) => ItemId = itemId;
  }

  [Fact]
  public void ToString_ShouldReturnExpectedStringRepresentation()
  {
    // Arrange
    var expectedItemId = "testId";
    var strongTypeId = new TestStrongTypeId(expectedItemId);

    // Act
    var result = strongTypeId.ToString();

    // Assert
    result.Should().Be(expectedItemId);
  }
}