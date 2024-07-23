using SharedDataModels.Abstractions.Items;

namespace SharedDataModels.Abstractions;

public record LootDetails(ItemId ItemId, int Stack, int Percentage);