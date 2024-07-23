namespace SharedDataModels.Abstractions.Gear.Weapons;

public class Sharpness
{
  private List<SharpnessSection> _sections = new List<SharpnessSection>();
  private readonly bool _isMaxed;

  //public Sharpness() { }
  public Sharpness(bool isMaxed, IEnumerable<SharpnessSection> sections)
  {
    _isMaxed = isMaxed;
    _sections = sections.ToList();
  }

  public bool IsMaxed => _isMaxed;

  public IEnumerable<SharpnessSection> Sections => _sections;

  public void AddSection(SharpnessSection section) => _sections.Add(section);
}