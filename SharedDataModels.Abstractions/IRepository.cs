namespace SharedDataModels.Abstractions;

public interface IRepository<TID, T>
{
  public T Get(TID id);
  public Task<T> GetAsync(TID id);
  public bool TryGet(TID id, out T value);
  public Task<bool> TryGetAsync(TID id, out T value);
  public IEnumerable<T> GetAll();
  public Task<IEnumerable<T>> GetAllAsync();
}