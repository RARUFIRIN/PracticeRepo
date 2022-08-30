using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Weapon,
        armor,
        Food,
        Ingredient,

    }
    public ItemType itemType;
    public string itemName;
    public int itemidx;
    public Sprite itemImage;
    public GameObject itemPrefab;

    public string WeaponType;
}

