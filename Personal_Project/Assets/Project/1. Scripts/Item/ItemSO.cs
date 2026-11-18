using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Inventory/ItemSO")]
public class ItemSO : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Icon;
    public int WidthSize;
    public int HeightSize;
    
}
