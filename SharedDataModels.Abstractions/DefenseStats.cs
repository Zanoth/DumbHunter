namespace SharedDataModels.Abstractions;

public record DefenseStats(
  int BaseDef,
  int MaxDef,
  int ArgumentedMaxDef,
  int FireDef,
  int WaterDef,
  int ThunderDef,
  int IceDef,
  int DragonDef);