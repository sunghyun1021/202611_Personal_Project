using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "ScriptableObjects/Inventory/ItemSO", order = 1)]
public class ItemSO : ScriptableObject
{
    public ItemType itemType;
    public Sprite itemIcon;
    public GameObject projectilePrefab;
    public string itemName;
    public int attackCooltime;
    public int attackDamage;
    public float speed;
}
