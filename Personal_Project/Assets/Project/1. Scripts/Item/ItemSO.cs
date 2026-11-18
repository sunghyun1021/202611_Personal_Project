using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Inventory/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string name;
    public string description;
    public Sprite icon;
    public ItemType itemtype;
    public int widthSize;
    public int heightSize;
}
