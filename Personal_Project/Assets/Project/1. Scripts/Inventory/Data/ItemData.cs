using System.Collections.Generic;
using UnityEngine;


public class ItemData
{
    public ItemSO ItemSO;
    public ItemType ItemType;
    public Sprite ItemIcon;
    public GameObject ProjectilePrefab;
    public string ItemName;
    public int AttackCooltime;
    public int AttackDamage;
    public float Speed;

    public ItemData(ItemSO so)
    {
        ItemSO = so;
        ItemType = so.itemType;
        ItemIcon = so.itemIcon;
        ProjectilePrefab = so.projectilePrefab;
        ItemName =  so.itemName;
        AttackCooltime = so.attackCooltime;
        AttackDamage = so.attackDamage;
        Speed = so.speed;
    }
}
