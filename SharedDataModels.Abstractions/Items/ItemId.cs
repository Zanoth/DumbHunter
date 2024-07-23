﻿namespace SharedDataModels.Abstractions.Items;

public record ItemId(string Id) : IStrongId<string>
{
  public static ItemId Empty() => new(string.Empty);
  public static ItemId New(string idStr) => new(idStr);
}