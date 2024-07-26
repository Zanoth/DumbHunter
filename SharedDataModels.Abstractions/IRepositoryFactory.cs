namespace SharedDataModels.Abstractions;

public interface IRepositoryFactory<TID, T>
{
  IRepository<TID, T> CreateRepository();
}