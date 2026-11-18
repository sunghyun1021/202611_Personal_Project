using UnityEngine;

public class ItemData : MonoBehaviour
{
    [SerializeField] private ItemSO _itemSO;

    public string Name;
    public string Description;
    public Sprite Icon;
    public ItemType ItemType;
    public int WidthSize;
    public int HeightSize;
    
    // ---------- Event Method ----------

    private void Awake() => InitData();
    
    // ----------------------------------

    private void InitData()
    {
        Name = _itemSO.name;
        Description = _itemSO.description;
        Icon = _itemSO.icon;
        ItemType = _itemSO.itemtype;
        WidthSize = _itemSO.widthSize;
        HeightSize = _itemSO.heightSize;
    }
}
