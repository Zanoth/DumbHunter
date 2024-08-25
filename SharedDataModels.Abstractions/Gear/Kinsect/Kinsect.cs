namespace SharedDataModels.Abstractions.Gear.Kinsect;

public class Kinsect
{
  public Kinsect(KinsectId kinsectId, string name, int rarity, KinsectAttackType attackType, KinsectDustEffect dustEffect, int power, int speed, int heal)
  {
    KinsectId = kinsectId;
    Name = name;
    Rarity = rarity;
    AttackType = attackType;
    DustEffect = dustEffect;
    Power = power;
    Speed = speed;
    Heal = heal;
  }

  public KinsectId KinsectId { get; }
  public string Name { get; }
  public int Rarity { get; }
  public KinsectAttackType AttackType { get; }
  public KinsectDustEffect DustEffect { get; }
  public int Power { get; }
  public int Speed { get; }
  public int Heal { get; }
}