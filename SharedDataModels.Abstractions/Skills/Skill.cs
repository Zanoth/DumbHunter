using System.Drawing;

namespace SharedDataModels.Abstractions.Skills;

public record Skill(SkillId SkillId, string Name, Color IconColor, int Secret, SkillId UnlocksId, IEnumerable<SkillLevel> Levels);