using SharedDataModels.Abstractions.Skills;
using System.Drawing;

namespace SharedDataModels.Abstractions.Decorations;

public record Decoration(DecorationId DecorationId, string Name, int RequiredSlotSize, int Rarity, Color IconColor, IEnumerable<GearSkill> Skills);