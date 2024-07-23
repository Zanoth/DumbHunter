using System.Drawing;

namespace SharedDataModels.Abstractions.Items;

public class Item
{
  public Item(ItemId itemId, string name, string category, string subcategory, int rarity, int buyPrice, int sellPrice, int carryLimit, int points, string iconName, Color iconColor)
  {
    ItemId = itemId;
    Name = name;
    Category = category;
    Subcategory = subcategory;
    Rarity = rarity;
    BuyPrice = buyPrice;
    SellPrice = sellPrice;
    CarryLimit = carryLimit;
    Points = points;
    IconName = iconName;
    IconColor = iconColor;
  }


  public ItemId ItemId { get; init; }
  public string Name { get; init; }
  public string Category { get; init; }
  public string Subcategory { get; init; }
  public int Rarity { get; init; }
  public int BuyPrice { get; init; }
  public int SellPrice { get; init; }
  public int CarryLimit { get; init; }
  public int Points { get; init; }

  public string IconName { get; init; }
  public Color IconColor { get; init; }
}