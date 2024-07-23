namespace SharedDataModels.Abstractions.Locations;

//Refactor: Should all data classes be records?
public class Location
{
  public Location(LocationId locationId, string name)
  {
    LocationId = locationId;
    Name = name;
  }

  public LocationId LocationId { get; init; }
  public string Name { get; init; }
}