namespace SharedDataModels.Abstractions;

public record DefenseStats(
  int BaseDef,
  int MaxDef,
  int AugmentedMaxDef,
  int FireDef,
  int WaterDef,
  int ThunderDef,
  int IceDef,
  int DragonDef);