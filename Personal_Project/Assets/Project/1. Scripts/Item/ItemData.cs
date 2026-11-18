using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField] private ItemSO _itemSO;

    public ItemType ItemType;
    public string Name;
    public string Description;
    public Sprite Icon;
    public int WidthSize;
    public int HeightSize;
    
    // ---------- Event Method ----------

    private void Awake() => InitData();
    
    // ----------------------------------

    private void InitData()
    {
        ItemType = _itemSO.itemtype;
        Name = _itemSO.name;
        Description = _itemSO.description;
        Icon = _itemSO.icon;
        WidthSize = _itemSO.widthSize;
        HeightSize = _itemSO.heightSize;
    }
}
