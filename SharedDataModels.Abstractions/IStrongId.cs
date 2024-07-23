namespace SharedDataModels.Abstractions;

//Refactor: Seems like the generic parameter is not working with my approach - consider removing it
public interface IStrongId <TValueType>
{
  public TValueType Id { get; }
}