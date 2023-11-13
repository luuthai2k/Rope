using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager ins;

    [SerializeField]
    private List<ShopItem> shopItems;

    [SerializeField]
    private List<string> names;

    public ShopSkinData skinData;

    [SerializeField]
    private string dataSkinName;

    [SerializeField]
    private SkinnedMeshRenderer player, shopCharacter, gunPlayer, gunCharacter;

    [SerializeField]
    private ShopItem shopItemNow;

    [SerializeField]
    private GameObject equip, unequip, buybutton, adsbutton, shop;

    [SerializeField]
    private TextMeshProUGUI textMoney, textNoMoney,name;

    [SerializeField]
    private Text money;

    [SerializeField]
    private Material defaultSkin, defaultGun;

    [SerializeField]
    private List<ShopItemUi> items;

    [SerializeField]
    private int index;

    public void Awake()
    {
        ins = this;
        skinData = new ShopSkinData();

        if (PlayerPrefs.HasKey(dataSkinName))
        {
            string data = PlayerPrefs.GetString(dataSkinName);
            skinData = JsonConvert.DeserializeObject<ShopSkinData>(data);
            Equip();

        }
        else
        {
            skinData.MaterialPlayer = 0;
            //skinData.Skin.Add(true);

        }

        InitData();
        ChooseSkin(index);
    }

    public void OnShop()
    {
        shop.SetActive(true);
        InitData();
    }
    public void BackShop()
    {
        shop.SetActive(false);
        InitData();
    }

    public void InitData()
    {
        money.text = ItemManager.ins.itemData.money + "";
        
        for (int i = 1; i < shopItems.Count; i++)
        {
            if (i < skinData.Skin.Count)
            {
                shopItems[i].isEquip = skinData.Skin[i];
            }
            else
            {
                skinData.Skin.Add(false);
            }
        }
        SaveDataSkin();

        if (skinData.MaterialPlayer != 0)
        {
            Material[] mats = shopCharacter.materials;

            mats[0] = shopItems[skinData.MaterialPlayer - 1].skin;
            shopCharacter.materials = mats;

            Material[] gunmats = gunCharacter.materials;

            gunmats[0] = shopItems[skinData.MaterialPlayer - 1].skinGun;
            gunCharacter.materials = gunmats;
        }


        if (skinData.MaterialPlayer != 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (i == skinData.MaterialPlayer - 1)
                {
                    items[i].OnItem();
                }
                else
                {
                    items[i].EndItem();
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {

                items[i].EndItem();
            }
        }


    }


    public void ChooseSkin(int _index)
    {

        index = _index;
        shopItemNow = shopItems[_index];
        skinData.MaterialPlayer = _index + 1;
        name.text = names[_index];
        if (shopItemNow.isEquip == false)
        {
            if (shopItemNow.itemShop == BuyItemShop.money)
            {
                buybutton.SetActive(true);
                equip.SetActive(false);
                unequip.SetActive(false);
                adsbutton.SetActive(false);
                textMoney.text = shopItemNow.buyIndex + "";
                textNoMoney.text = shopItemNow.buyIndex + "";
                if (ItemManager.ins.itemData.money >= shopItemNow.buyIndex)
                {
                    textMoney.enabled = true;
                    textNoMoney.enabled = false;
                }
                else
                {
                    textMoney.enabled = false;
                    textNoMoney.enabled = true;
                }
            }
            else
            {
                buybutton.SetActive(false);
                equip.SetActive(false);
                unequip.SetActive(false);
                adsbutton.SetActive(true);
            }
        }
        else
        {
            if (shopItemNow.skin == player.materials[0])
            {
                buybutton.SetActive(false);
                equip.SetActive(false);
                unequip.SetActive(true);
                adsbutton.SetActive(false);
            }
            else
            {
                buybutton.SetActive(false);
                equip.SetActive(true);
                unequip.SetActive(false);
                adsbutton.SetActive(false);
            }
        }

        InitData();
    }


    public void SaveDataSkin()
    {
        PlayerPrefs.SetString(dataSkinName, JsonConvert.SerializeObject(skinData));
    }


    public void Equip()
    {

        if (skinData.MaterialPlayer != 0)
        {
            Material[] mats = player.materials;

            mats[0] = shopItems[skinData.MaterialPlayer - 1].skin;
            player.materials = mats;

            Material[] gunmats = gunPlayer.materials;

            gunmats[0] = shopItems[skinData.MaterialPlayer - 1].skinGun;
            gunPlayer.materials = gunmats;
        }
        equip.SetActive(false);
        unequip.SetActive(true);
    }

    public void UnEquip()
    {
        if (skinData.MaterialPlayer != 0)
        {
            Material[] mats = player.materials;

            mats[0] = defaultSkin;
            player.materials = mats;

            Material[] gummats = player.materials;

            mats[0] = defaultGun;
            gunPlayer.materials = mats;
        }
        equip.SetActive(true);
        unequip.SetActive(false);
    }

    public void Ads()
    {
        skinData.Skin[skinData.MaterialPlayer - 1] = true;
        SaveDataSkin();
        InitData();
        ChooseSkin(index);
    }

    public void Buy()
    {
        if(ItemManager.ins.itemData.money >= shopItemNow.buyIndex)
        {
            ItemManager.ins.AddMoney(-shopItemNow.buyIndex);
            skinData.Skin[skinData.MaterialPlayer - 1] = true;
            SaveDataSkin();
            InitData();
            ChooseSkin(index);
        }
      
    }
}
public enum BuyItemShop
{
    money,
    ads
}

public class ShopSkinData
{
    [SerializeField]
    private List<bool> skin;
    [SerializeField]
    private int materialPlayer;

    public List<bool> Skin { get => skin; set => skin = value; }
    public int MaterialPlayer { get => materialPlayer; set => materialPlayer = value; }
    public ShopSkinData()
    {
        skin = new List<bool>();
    }
}