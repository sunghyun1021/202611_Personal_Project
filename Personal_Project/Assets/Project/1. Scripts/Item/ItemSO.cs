using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/Inventory/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    public ItemType itemtype;
    public string name;
    public string description;
    public Sprite icon;
    public int widthSize;
    public int heightSize;
}
