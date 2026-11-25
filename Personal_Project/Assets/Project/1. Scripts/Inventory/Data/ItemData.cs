using System.Collections.Generic;
using UnityEngine;


public class ItemData
{
    public ItemSO ItemSO;
    public ItemType ItemType;
    public Sprite ItemIcon;
    public GameObject ProjectilePrefab;
    public string ItemName;
    public float Speed;
    public float AttackCooltime;
    public int AttackDamage;

    public ItemData(ItemSO so)
    {
        ItemSO = so;
        ItemType = so.itemType;
        ItemIcon = so.itemIcon;
        ProjectilePrefab = so.projectilePrefab;
        ItemName =  so.itemName;
        Speed = so.speed;
        AttackCooltime = so.attackCooltime;
        AttackDamage = so.attackDamage;
    }
}
