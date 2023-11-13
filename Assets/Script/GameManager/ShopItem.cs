using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shop", menuName = "Shop/Skin")]
public class ShopItem : ScriptableObject
{
    public string Name;
    public BuyItemShop itemShop;
    public bool isEquip;
    public int buyIndex;
    public Material skin;
    public Material skinGun;
}

