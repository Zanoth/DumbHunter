namespace SharedDataModels.Abstractions.Monsters;

public class TrapInteractions
{
  public TrapInteractions(bool pitfallTrap, bool shockTrap, bool vineTrap)
  {
    PitfallTrap = pitfallTrap;
    ShockTrap = shockTrap;
    VineTrap = vineTrap;
  }

  public bool PitfallTrap { get; init; }
  public bool ShockTrap { get; init; }
  public bool VineTrap { get; init; }
}