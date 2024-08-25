namespace SharedDataModels.Abstractions.Monsters;

public class FormWeakness
{
  public FormWeakness(bool isAltForm, string description, int fireVurnability, int waterVurnability, int thunderVurnability, int iceVurnability, int dragonVurnability, int poisonVurnability, int sleepVurnability, int paralysisVurnability, int blastVurnability, int stunVurnability)
  {
    IsAltForm = isAltForm;
    Description = description;
    FireVurnability = fireVurnability;
    WaterVurnability = waterVurnability;
    ThunderVurnability = thunderVurnability;
    IceVurnability = iceVurnability;
    DragonVurnability = dragonVurnability;
    PoisonVurnability = poisonVurnability;
    SleepVurnability = sleepVurnability;
    ParalysisVurnability = paralysisVurnability;
    BlastVurnability = blastVurnability;
    StunVurnability = stunVurnability;
  }

  public bool IsAltForm { get; }
  public string Description { get; }
  public int FireVurnability { get; }
  public int WaterVurnability { get; }
  public int ThunderVurnability { get; }
  public int IceVurnability { get; }
  public int DragonVurnability { get; }
  public int PoisonVurnability { get; }
  public int SleepVurnability { get; }
  public int ParalysisVurnability { get; }
  public int BlastVurnability { get; }
  public int StunVurnability { get; }
}