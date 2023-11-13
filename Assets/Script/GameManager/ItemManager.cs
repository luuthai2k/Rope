using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    public static ItemManager ins;

    public ItemData itemData;

    [SerializeField]
    private string itemDataName = "Itemdata";

    [SerializeField]
    private Text hpKitText, armorText, spinText, moneytext;

    private void Awake()
    {
        ins = this;
        itemData = new ItemData();
        if (PlayerPrefs.HasKey(itemDataName))
        {
            string data = PlayerPrefs.GetString(itemDataName);
            itemData = JsonConvert.DeserializeObject<ItemData>(data);
        }

    }


    public void Start()
    {

        Init();

        SaveData();

        AddMoney(999999);
    }
    private void Init()
    {
        if (hpKitText != null) { hpKitText.text = itemData.hpkit + ""; }
        if (armorText != null) { armorText.text = itemData.armor + ""; }
       
        //spinText.text = itemData.spin + "";
        moneytext.text = itemData.money + "";
    }

    public void AddHPKit(int _index)
    {
        itemData.hpkit += _index;
        SaveData();
    }

    public void AddArmor(int _index)
    {
        itemData.armor += _index;
        SaveData();
    }

    public void AddSpin(int _index)
    {
        itemData.spin += _index;
        SaveData();
    }

    public void AddMoney(int _index)
    {
        itemData.money += _index;
        SaveData();
    }

    public void SaveData()
    {
        PlayerPrefs.SetString(itemDataName, JsonConvert.SerializeObject(itemData));
        Init();
    }
}


public enum Gift
{
    hpKit,
    armor,
    spin,
    money,
    skin
}

public class ItemData
{
    [SerializeField]
    private int Armor, Spin, Money, Hpkit;

    public int armor { get => Armor; set => Armor = value; }
    public int spin { get => Spin; set => Spin = value; }
    public int money { get => Money; set => Money = value; }
    public int hpkit { get => Hpkit; set => Hpkit = value; }

    public ItemData()
    {
        armor = 0;
        spin = 0;
        money = 0;
    }
}

