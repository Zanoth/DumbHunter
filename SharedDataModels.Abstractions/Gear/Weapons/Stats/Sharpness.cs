namespace SharedDataModels.Abstractions.Gear.Weapons.Stats;

public class Sharpness
{
  private List<SharpnessSection> _sections = new List<SharpnessSection>();
  private readonly bool _isMaxed;

  public Sharpness(bool isMaxed, IEnumerable<SharpnessSection> sections)
  {
    _isMaxed = isMaxed;
    _sections = sections.ToList();
  }

  public static Sharpness Empty() => new Sharpness(false, new List<SharpnessSection>());

  public bool IsMaxed => _isMaxed;

  public IEnumerable<SharpnessSection> Sections => _sections;

  public void AddSection(SharpnessSection section) => _sections.Add(section);
}