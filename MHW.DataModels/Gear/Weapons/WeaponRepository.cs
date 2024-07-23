using Serialization.Abstraction;

namespace MHW.DataModels.Gear.Weapons;

public class WeaponRepository : RepositoryBase<WeaponId, Weapon>
{
  public WeaponRepository(ISerializor serializor)
  {
    Serializor = serializor;
    Initialize();
  }

  protected override string ResourceName { get; } = $"{typeof(WeaponRepository).Namespace}.WeaponConfiguration.json";
  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<WeaponId, Weapon> entityDictionary, List<Weapon> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.WeaponId, entity);
  }
}