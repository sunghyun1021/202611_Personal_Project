using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Inventory/ItemSO")]
public class ItemSO : ScriptableObject
{
    public ItemType itemtype;
    public string name;
    public string description;
    public Sprite icon;
    public int widthSize;
    public int heightSize;
}
