using System.Collections.Generic;
using UnityEngine;


public class ItemData
{
    public ItemSO ItemSO;
    public ItemType ItemType => ItemSO.itemType;
    public string ItemName => ItemSO.itemName;
    public Sprite ItemIcon => ItemSO.itemIcon;

    public ItemData(ItemSO so)
    {
        ItemSO = so;
    }
}
