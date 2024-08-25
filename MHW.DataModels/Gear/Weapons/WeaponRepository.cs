using Serialization.Abstraction;
using SharedDataModels.Abstractions.Gear.Weapons;

namespace MHW.DataModels.Gear.Weapons;

public class WeaponRepository : RepositoryBase<WeaponId, IWeapon>
{
  private static readonly string ResourceNamespace = $"{typeof(WeaponRepository).Namespace}";
  private static readonly string ResourceNameSuffix = $"Configuration.json";


  public WeaponRepository(ISerializor serializor)
  {
    Serializor = serializor;

    var configurationFileNames = Enum.GetValues<WeaponType>()
      .Select(weaponTypeEnum => weaponTypeEnum.ToString());

    foreach(var fileName in configurationFileNames)
      Initialize(ResourceNamespace + "." + fileName + ResourceNameSuffix);
  }

  protected override ISerializor Serializor { get; }
  protected override void AddEntitiesToDictionary(IDictionary<WeaponId, IWeapon> entityDictionary, List<IWeapon> entityList)
  {
    foreach (var entity in entityList)
      entityDictionary.Add(entity.WeaponId, entity);
  }
}