using Serialization.Abstraction;
using SharedDataModels.Abstractions;
using System.Reflection;

namespace MHW.DataModels;

public abstract class RepositoryBase<Tid, T> : IRepository<Tid, T>
{
  protected abstract string ResourceName { get; }
  protected abstract ISerializor Serializor { get; }
  private readonly IDictionary<Tid, T> _entities = new Dictionary<Tid, T>();

  protected void Initialize()
  {
    var assembly = Assembly.GetExecutingAssembly();
    var entities = Serializor.Deserialize<List<T>>(assembly, ResourceName);
    AddEntitiesToDictionary(_entities, entities);
  }

  protected abstract void AddEntitiesToDictionary(IDictionary<Tid, T> entityDictionary, List<T> entityList);

  public T Get(Tid id) => _entities[id];
  public Task<T> GetAsync(Tid id) => Task.FromResult(Get(id));
  public bool TryGet(Tid id, out T value) => _entities.TryGetValue(id, out value);

  public Task<bool> TryGetAsync(Tid id, out T value) => Task.FromResult(TryGet(id, out value));

  public IEnumerable<T> GetAll() => _entities.Values.AsEnumerable();
  public Task<IEnumerable<T>> GetAllAsync() => Task.FromResult(GetAll());
}